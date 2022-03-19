using ElysiumPlayer.AAudio;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using TestAAudio.Models;
using TestAAudio.Services;
using static ElysiumPlayer.AAudio.AAudio;

namespace TestAAudio.Droid.Services
{

    public class AAudioStreamsCallbacks : IDisposable
    {
        private AAudioStream _outputStream;
        private AAudioStream _inputMic;
        private float[] _audioBuffer;
        private IntPtr _audioBufferPtr;
        private GCHandle _audioBufferHandle;
        private bool _firstExecution;
        public bool StopAudioProcessing { get; set; }

        public int InputFramesPerBurst { get; set; }
        public int OutputFramesPerBurst { get; set; }

        public AAudioStreamsCallbacks(AAudioStream inputMic, AAudioStream outputStream)
        {
            _outputStream = outputStream;
            _inputMic = inputMic;
            _firstExecution = true;
            _audioBuffer = new float[4*1024*1024];
            _audioBufferHandle = GCHandle.Alloc(_audioBuffer, GCHandleType.Pinned);
            _audioBufferPtr = _audioBufferHandle.AddrOfPinnedObject();
            StopAudioProcessing = false;
        }

        public aaudio_data_callback_result_t InputStreamCallback(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames)
        {
            if (StopAudioProcessing)
            {
                return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP;
            }

            var result = AAudioStream_write(_outputStream, audioData, numFrames, 0);
            int nmberOfFramesWritten = (int)result;
            if (nmberOfFramesWritten < 0 || nmberOfFramesWritten != numFrames)
            {
                //TODO: issue, what to do here?
                return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
            }

            return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
        }

        public aaudio_data_callback_result_t OutputStreamCallback(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames)
        {
            if (StopAudioProcessing)
            {
                return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP;
            }

            AAudioStream_read(_inputMic, audioData, numFrames, 1);
            return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
        }

        public aaudio_data_callback_result_t OutputStreamCallback_SkipInput(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames)
        {
            if (StopAudioProcessing)
            {
                return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP;
            }

            //we want to read all the data we have
            var inputBuffer = _audioBufferPtr;
            int readFrames;
            int totalRead = 0;
            do
            {
                readFrames = (int)AAudioStream_read(_inputMic, inputBuffer, InputFramesPerBurst, 0);
                if (readFrames > 0)
                {
                    inputBuffer += readFrames;
                    totalRead += readFrames;
                }
            }
            while (readFrames == InputFramesPerBurst && _firstExecution);

            if (numFrames < totalRead)
            {
                int startIndex = totalRead - numFrames;
                Marshal.Copy(_audioBuffer, startIndex, audioData, numFrames);
            }
            else
            {
                Marshal.Copy(_audioBuffer, 0, audioData, Math.Min(numFrames, totalRead));
            }
            _firstExecution = false;
            return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
        }

        public aaudio_data_callback_result_t OutputStreamCallback_AmplifyMic(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames)
        {
            if (StopAudioProcessing)
            {
                return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP;
            }

            var readFrames = (int)AAudioStream_read(_inputMic, _audioBufferPtr, numFrames, 10);
            var samples = readFrames * 2;
            var micVolume = 2.5f; //mic volume

            unsafe
            {
                float* dst = (float*)audioData;
                float* src = (float*)_audioBufferPtr;
                while (samples >= 0)
                {
                    *dst = *src * micVolume;
                    dst++;
                    src++;
                    samples--;
                }
            }

            return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
        }

        public void OutputErrorCallback(
            IntPtr stream,
            IntPtr userData,
            aaudio_result_t error)
        {
            //todo: do nothing?
        }

        public void Dispose()
        {
            StopAudioProcessing = true;
            Thread.Sleep(100);
            _audioBufferHandle.Free();
            _audioBufferPtr = IntPtr.Zero;
            _audioBuffer = null;
        }
    }

    public class AAudioStreamsData: IDisposable
    {
        private AAudioStream input;
        private AAudioStream output;
        private AAudioStreamsCallbacks callbacks;

        public AAudioStreamsData(AAudioStream input, AAudioStream output, AAudioStreamsCallbacks callbacks)
        {
            this.input = input;
            this.output = output;
            this.callbacks = callbacks;
        }

        public void Dispose()
        {
            if (callbacks != null)
            {
                callbacks.Dispose();
                callbacks = null;
            }

            AAudioStream_requestStop(input).Check();
            AAudioStream_requestStop(output).Check();

            input.CloseStream();
            output.CloseStream();
        }
    }
    public class AudioService : IAudioService
    {
        public bool CloseStream(IntPtr stream)
        {
            if (stream == IntPtr.Zero)
            {
                return false;
            }
            var s = AAudioStream.Create(stream);
            s.CloseStream();
            return true;
        }

        public IntPtr CreateStream(StreamDirection streamDirection, AaudioStreamCreateParams streamParams)
        {
            var stream = streamDirection == StreamDirection.Input
                ? CreateInputAAudioStream(InputStreamCallback, streamParams)
                : CreateOutputAAudioStream(OutputStreamCallback, streamParams);
            return stream.AsIntPtr();
        }

        private static aaudio_data_callback_result_t InputStreamCallback(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames)
        {
            //do nothing for now
            return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
        }

        private static aaudio_data_callback_result_t OutputStreamCallback(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames)
        {
            //do nothing for now
            return aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
        }

        private static void OutputErrorCallback(
        IntPtr stream,
        IntPtr userData,
        aaudio_result_t error)
        {
            //todo: do nothing?
        }
        public StreamsData CreateStreams(AaudioStreamCreateParams inputStreamParams, AaudioStreamCreateParams outputStreamParams)
        {
            //return CreateStreams_InputPushToOutput(inputStreamParams, outputStreamParams);
            return CreateStreams_OutputReadsFromInput(inputStreamParams, outputStreamParams);
        }

        private StreamsData CreateStreams_InputPushToOutput(AaudioStreamCreateParams inputStreamParams, AaudioStreamCreateParams outputStreamParams)
        {
            var outputStream = CreateOutputAAudioStream(null, outputStreamParams);
            var callbacks = new AAudioStreamsCallbacks(AAudioStream.Empty, outputStream);
            var inputStream = CreateInputAAudioStream(callbacks.InputStreamCallback, inputStreamParams);


            #region start playing streams
            AAudioStream_requestStart(outputStream).Check();
            AAudioStream_requestStart(inputStream).Check();
            #endregion

            return new StreamsData
            {
                StreamCalbacks = new AAudioStreamsData(inputStream, outputStream, callbacks),
                InputStreamStatus = GetStreamStatus(inputStream.AsIntPtr()),
                OutputStreamStatus = GetStreamStatus(outputStream.AsIntPtr()),
            };
        }

        private StreamsData CreateStreams_OutputReadsFromInput(AaudioStreamCreateParams inputStreamParams, AaudioStreamCreateParams outputStreamParams)
        {
            var inputStream = CreateInputAAudioStream(null, inputStreamParams);
            var callbacks = new AAudioStreamsCallbacks(inputStream, AAudioStream.Empty);
            var outputStream = CreateOutputAAudioStream(callbacks.OutputStreamCallback, outputStreamParams);

            callbacks.InputFramesPerBurst = AAudioStream_getFramesPerBurst(inputStream);
            callbacks.OutputFramesPerBurst = AAudioStream_getFramesPerBurst(outputStream);
            
            #region start playing streams
            AAudioStream_requestStart(inputStream).Check();
            AAudioStream_requestStart(outputStream).Check();
            #endregion

            return new StreamsData
            {
                StreamCalbacks = new AAudioStreamsData(inputStream, outputStream, callbacks),
                InputStreamStatus = GetStreamStatus(inputStream.AsIntPtr()),
                OutputStreamStatus = GetStreamStatus(outputStream.AsIntPtr()),
            };
        }

        public AaudioStreamStatus GetStreamStatus(IntPtr stream)
        {
            if (stream == IntPtr.Zero)
            {
                return new AaudioStreamStatus
                {
                    Created = false,
                    ExclusiveMode = false,
                    IsMmapUsed = false,
                    LowLatency = false,
                };
            }

            var s = AAudioStream.Create(stream);

            return new AaudioStreamStatus
            {
                Created = true,
                ExclusiveMode = AAudioStream_getSharingMode(s) == aaudio_sharing_mode_t.AAUDIO_SHARING_MODE_EXCLUSIVE,
                IsMmapUsed = AAudioStream_isMMapUsed(s),
                LowLatency = AAudioStream_getPerformanceMode(s) == aaudio_performance_mode_t.AAUDIO_PERFORMANCE_MODE_LOW_LATENCY,
            };
        }

        private static AAudioStream CreateInputAAudioStream(AAudioStream_dataCallback dataCallback, AaudioStreamCreateParams createParams)
        {
            AAudioStreamBuilder builder = new AAudioStreamBuilder();
            AAudio_createStreamBuilder(ref builder);
            try
            {
                var requestedSharingMode = createParams.ExclusiveMode
                    ? aaudio_sharing_mode_t.AAUDIO_SHARING_MODE_EXCLUSIVE
                    : aaudio_sharing_mode_t.AAUDIO_SHARING_MODE_SHARED;

                var requestedPerformanceMode = createParams.LowLatency
                    ? aaudio_performance_mode_t.AAUDIO_PERFORMANCE_MODE_LOW_LATENCY
                    : aaudio_performance_mode_t.AAUDIO_PERFORMANCE_MODE_NONE;

                aaudio_policy_t mmapPolicy = createParams.AlwaysUseMmap
                    ? aaudio_policy_t.AAUDIO_POLICY_ALWAYS
                    : aaudio_policy_t.AAUDIO_POLICY_AUTO;

                AAudio_setMMapPolicy(mmapPolicy);

                //AAudioStreamBuilder_setDeviceId(builder, 13);
                AAudioStreamBuilder_setDirection(builder, aaudio_direction_t.AAUDIO_DIRECTION_INPUT);
                AAudioStreamBuilder_setPerformanceMode(builder, requestedPerformanceMode);
                AAudioStreamBuilder_setSharingMode(builder, requestedSharingMode);
                //AAudioStreamBuilder_setSampleRate(builder, sampleRate);
                AAudioStreamBuilder_setChannelCount(builder, 2);
                AAudioStreamBuilder_setFormat(builder, aaudio_format_t.AAUDIO_FORMAT_PCM_FLOAT);
                if (dataCallback != null)
                {
                    AAudioStreamBuilder_setDataCallback(builder, dataCallback, IntPtr.Zero);
                }

                AAudioStream stream = new AAudioStream();
                AAudioStreamBuilder_openStream(builder, ref stream).Check();

                return stream;
            }
            finally
            {
                AAudioStreamBuilder_delete(builder).Check();
            }
        }

        private static AAudioStream CreateOutputAAudioStream(AAudioStream_dataCallback dataCallback, AaudioStreamCreateParams createParams)
        {
            AAudioStreamBuilder builder = new AAudioStreamBuilder();
            AAudio_createStreamBuilder(ref builder);
            try
            {
                var requestedSharingMode = createParams.ExclusiveMode
                    ? aaudio_sharing_mode_t.AAUDIO_SHARING_MODE_EXCLUSIVE
                    : aaudio_sharing_mode_t.AAUDIO_SHARING_MODE_SHARED;

                var requestedPerformanceMode = createParams.LowLatency
                    ? aaudio_performance_mode_t.AAUDIO_PERFORMANCE_MODE_LOW_LATENCY
                    : aaudio_performance_mode_t.AAUDIO_PERFORMANCE_MODE_NONE;

                if(dataCallback != null)
                {
                    AAudioStreamBuilder_setDataCallback(builder, dataCallback, IntPtr.Zero);
                }

                AAudioStreamBuilder_setErrorCallback(builder, OutputErrorCallback, IntPtr.Zero);
                //AAudioStreamBuilder_setDeviceId(builder, 15); //22 - HDMI
                AAudioStreamBuilder_setDirection(builder, aaudio_direction_t.AAUDIO_DIRECTION_OUTPUT);
                AAudioStreamBuilder_setChannelCount(builder, 2);
                AAudioStreamBuilder_setFormat(builder, aaudio_format_t.AAUDIO_FORMAT_PCM_FLOAT);
                AAudioStreamBuilder_setSharingMode(builder, requestedSharingMode);
                AAudioStreamBuilder_setPerformanceMode(builder, requestedPerformanceMode);
                //AAudioStreamBuilder_setSampleRate(builder, sampleRate);

                AAudioStream stream = new AAudioStream();
                AAudioStreamBuilder_openStream(builder, ref stream).Check();

                return stream;
            }
            finally
            {
                AAudioStreamBuilder_delete(builder).Check();
            }
        }

    }
}