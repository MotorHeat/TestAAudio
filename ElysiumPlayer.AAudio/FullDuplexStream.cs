using System;
using System.Runtime.InteropServices;
using static ElysiumPlayer.AAudio.AAudio;

namespace ElysiumPlayer.AAudio
{

    public delegate aaudio_data_callback_result_t OnBothStreamsReady(
        AAudioStream inputStream,
        float[] inputData,
        int numInputFrames,
        AAudioStream outputStream,
        IntPtr outputData,
        int numOutputFrames);

    public class FullDuplexStream
    {
        public FullDuplexStream(OnBothStreamsReady streamsReady)
        {
            _streamsReady = streamsReady;
        }

        ~FullDuplexStream()
        {
            if (mInputBufferHandle.IsAllocated)
            {
                mInputBufferHandle.Free();
            }
        }

        public void setInputStream(AAudioStream stream)
        {
            mInputStream = stream;
        }

        public void setOutputStream(AAudioStream stream)
        {
            mOutputStream = stream;
        }

        public aaudio_result_t start()
        {
            mCountCallbacksToDrain = kNumCallbacksToDrain;
            mCountInputBurstsCushion = mNumInputBurstsCushion;
            mCountCallbacksToDiscard = kNumCallbacksToDiscard;

            // Determine maximum size that could possibly be called.
            int bufferSize = AAudioStream_getBufferCapacityInFrames(mOutputStream)
                    * AAudioStream_getChannelCount(mOutputStream);
            if (bufferSize > mBufferSize)
            {
                mInputBuffer = new float[bufferSize];
                mBufferSize = bufferSize;
                if (mInputBufferHandle.IsAllocated)
                {
                    mInputBufferHandle.Free();
                }
                mInputBufferHandle = GCHandle.Alloc(mInputBuffer, GCHandleType.Pinned);
            }
            var result = AAudioStream_requestStart(mInputStream);
            if (result != aaudio_result_t.AAUDIO_OK)
            {
                return result;
            }
            return AAudioStream_requestStart(mOutputStream);
        }

        public aaudio_result_t stop()
        {
            var outputResult = aaudio_result_t.AAUDIO_OK;
            var inputResult = aaudio_result_t.AAUDIO_OK;
            if (mOutputStream.Initialized)
            {
                outputResult = AAudioStream_requestStop(mOutputStream);
            }
            if (mInputStream.Initialized)
            {
                inputResult = AAudioStream_requestStop(mInputStream);
            }
            if (outputResult != aaudio_result_t.AAUDIO_OK)
            {
                return outputResult;
            }
            else
            {
                return inputResult;
            }
        }

        /**
         * Called by Oboe when the stream is ready to process audio.
         * This implements the stream synchronization. App should NOT override this method.
         */
        public aaudio_data_callback_result_t onAudioReady(
                AAudioStream outputStream,
                IntPtr audioData,
                int numFrames)
        {
            aaudio_data_callback_result_t callbackResult = aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_CONTINUE;
            int actualFramesRead = 0;

            // Silence the output.
            //int numBytes = numFrames * AAudioStream_getChannelCount(outputStream) * sizeof(float);
            //memset(audioData, 0 /* value */, numBytes);

            Array.Fill(mInputBuffer, 0);
            Marshal.Copy(mInputBuffer, 0, audioData, numFrames * AAudioStream_getChannelCount(outputStream));

            if (mCountCallbacksToDrain > 0)
            {
                // Drain the input.
                int totalFramesRead = 0;
                do
                {
                    int result = (int)AAudioStream_read(mInputStream, mInputBufferHandle.AddrOfPinnedObject(), numFrames, 0 /* timeout */);
                    if (result < 0)
                    {
                        // Ignore errors because input stream may not be started yet.
                        break;
                    }
                    actualFramesRead = result;
                    totalFramesRead += actualFramesRead;
                } while (actualFramesRead > 0);
                // Only counts if we actually got some data.
                if (totalFramesRead > 0)
                {
                    mCountCallbacksToDrain--;
                }

            }
            else if (mCountInputBurstsCushion > 0)
            {
                // Let the input fill up a bit so we are not so close to the write pointer.
                mCountInputBurstsCushion--;

            }
            else if (mCountCallbacksToDiscard > 0)
            {
                // Ignore. Allow the input to reach to equilibrium with the output.
                int result = (int)AAudioStream_read(mInputStream, mInputBufferHandle.AddrOfPinnedObject(), numFrames, 0 /* timeout */);
                if (result < 0)
                {
                    callbackResult = aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP;
                }
                mCountCallbacksToDiscard--;

            }
            else
            {
                // Read data into input buffer.
                int result = (int)AAudioStream_read(mInputStream, mInputBufferHandle.AddrOfPinnedObject(), numFrames, 0 /* timeout */);
                if (result < 0)
                {
                    callbackResult = aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP;
                }
                else
                {
                    int framesRead = result;

                    callbackResult = _streamsReady(
                            mInputStream, 
                            mInputBuffer, 
                            framesRead,
                            mOutputStream, 
                            audioData, 
                            numFrames
                    );
                }
            }

            if (callbackResult == aaudio_data_callback_result_t.AAUDIO_CALLBACK_RESULT_STOP)
            {
                AAudioStream_requestStop(mInputStream);
            }

            return callbackResult;
        }

        public int getNumInputBurstsCushion() => mNumInputBurstsCushion;

        /**
         * Number of bursts to leave in the input buffer as a cushion.
         * Typically 0 for latency measurements
         * or 1 for glitch tests.
         *
         * @param mNumInputBurstsCushion
         */
        public void setNumInputBurstsCushion(int numBursts)
        {
            mNumInputBurstsCushion = numBursts;
        }


        // TODO add getters and setters
        private const int kNumCallbacksToDrain = 20;
        private const int kNumCallbacksToDiscard = 30;
        private readonly OnBothStreamsReady _streamsReady;

        // let input fill back up, usually 0 or 1
        private int mNumInputBurstsCushion = 1; //see also mCountInputBurstsCushion

        // We want to reach a state where the input buffer is empty and
        // the output buffer is full.
        // These are used in order.
        // Drain several callback so that input is empty.
        private int mCountCallbacksToDrain = kNumCallbacksToDrain;
        // Let the input fill back up slightly so we don't run dry.
        private int mCountInputBurstsCushion = 1;//this should be initialized same way as mNumInputBurstsCushion;
        // Discard some callbacks so the input and output reach equilibrium.
        private int mCountCallbacksToDiscard = kNumCallbacksToDiscard;

        private AAudioStream mInputStream;
        private AAudioStream mOutputStream;

        private int mBufferSize = 0;
        private float[] mInputBuffer;
        private GCHandle mInputBufferHandle;
    }

}