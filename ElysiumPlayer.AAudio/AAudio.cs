using System;
using System.Runtime.InteropServices;
namespace ElysiumPlayer.AAudio
{
    public struct AAudioStreamBuilder
    {
        internal IntPtr value;
    }

    public struct AAudioStream
    {
        internal IntPtr value;
        internal AAudioStream(IntPtr ptr)
        {
            value = ptr;
        }

        public static AAudioStream Create(IntPtr ptr) => new AAudioStream(ptr);

        public bool Initialized => value != IntPtr.Zero;

        public static readonly AAudioStream Empty = new AAudioStream(IntPtr.Zero);

    }

    public static class AAudio
    {
        private const string AAudioLib = "aaudio";
        public const int AAUDIO_UNSPECIFIED = 0;

        #region Typed wrappers

        public static aaudio_result_t AAudio_createStreamBuilder(ref AAudioStreamBuilder builder)
        {
            return AAudio_createStreamBuilder(ref builder.value);
        }

        public static aaudio_result_t AAudioStreamBuilder_delete(AAudioStreamBuilder builder)
        {
            return AAudioStreamBuilder_delete(builder.value);
        }

        public static void AAudioStreamBuilder_setDeviceId(AAudioStreamBuilder builder, Int32 deviceId)
        {
            AAudioStreamBuilder_setDeviceId(builder.value, deviceId);
        }

        public static void AAudioStreamBuilder_setDirection(AAudioStreamBuilder builder, aaudio_direction_t direction)
        {
            AAudioStreamBuilder_setDirection(builder.value, direction);
        }

        public static void AAudioStreamBuilder_setSharingMode(AAudioStreamBuilder builder, aaudio_sharing_mode_t sharingMode)
        {
            AAudioStreamBuilder_setSharingMode(builder.value, sharingMode);
        }

        public static void AAudioStreamBuilder_setSampleRate(AAudioStreamBuilder builder, Int32 sampleRate)
        {
            AAudioStreamBuilder_setSampleRate(builder.value, sampleRate);
        }

        public static void AAudioStreamBuilder_setChannelCount(AAudioStreamBuilder builder, Int32 channelCount)
        {
            AAudioStreamBuilder_setChannelCount(builder.value, channelCount);
        }

        public static void AAudioStreamBuilder_setFormat(AAudioStreamBuilder builder, aaudio_format_t format)
        {
            AAudioStreamBuilder_setFormat(builder.value, format);
        }

        public static void AAudioStreamBuilder_setBufferCapacityInFrames(AAudioStreamBuilder builder, Int32 numFrames)
        {
            AAudioStreamBuilder_setBufferCapacityInFrames(builder.value, numFrames);
        }

        public static void AAudioStreamBuilder_setPerformanceMode(AAudioStreamBuilder builder, aaudio_performance_mode_t mode)
        {
            AAudioStreamBuilder_setPerformanceMode(builder.value, mode);
        }

        public static aaudio_result_t AAudioStreamBuilder_openStream(AAudioStreamBuilder builder, ref AAudioStream stream)
        {
            return AAudioStreamBuilder_openStream(builder.value, ref stream.value);
        }

        public static aaudio_result_t AAudioStream_close(AAudioStream stream)
        {
            return AAudioStream_close(stream.value);
        }

        public static void AAudioStreamBuilder_setDataCallback(AAudioStreamBuilder builder, AAudioStream_dataCallback callback, IntPtr userData)
        {
            AAudioStreamBuilder_setDataCallback(builder.value, callback, userData);
        }

        public static void AAudioStreamBuilder_setFramesPerDataCallback(AAudioStreamBuilder builder, Int32 numFrames)
        {
            AAudioStreamBuilder_setFramesPerDataCallback(builder.value, numFrames);
        }

        public static void AAudioStreamBuilder_setErrorCallback(AAudioStreamBuilder builder, AAudioStream_errorCallback callback, IntPtr userData)
        {
            AAudioStreamBuilder_setErrorCallback(builder.value, callback, userData);
        }

        public static aaudio_result_t AAudioStream_write(AAudioStream stream, IntPtr buffer, Int32 numFrames, Int64 timeoutNanoseconds)
        {
            return AAudioStream_write(stream.value, buffer, numFrames, timeoutNanoseconds);
        }

        public static aaudio_result_t AAudioStream_read(AAudioStream stream, IntPtr buffer, Int32 numFrames, Int64 timeoutNanoseconds)
        {
            return AAudioStream_read(stream.value, buffer, numFrames, timeoutNanoseconds);
        }

        public static aaudio_result_t AAudioStream_requestStart(AAudioStream stream)
        {
            return AAudioStream_requestStart(stream.value);
        }

        public static aaudio_result_t AAudioStream_requestPause(AAudioStream stream)
        {
            return AAudioStream_requestPause(stream.value);
        }
        public static aaudio_result_t AAudioStream_requestFlush(AAudioStream stream)
        {
            return AAudioStream_requestFlush(stream.value);
        }
        
        public static aaudio_result_t AAudioStream_requestStop(AAudioStream stream)
        {
            return AAudioStream_requestStop(stream.value);
        }
        
        public static aaudio_stream_state_t AAudioStream_getState(AAudioStream stream)
        {
            return AAudioStream_getState(stream.value);
        }
        
        public static aaudio_result_t AAudioStream_waitForStateChange(AAudioStream stream,
                aaudio_stream_state_t inputState, ref aaudio_stream_state_t nextState,
                Int64 timeoutNanoseconds)
        {
            return AAudioStream_waitForStateChange(stream.value, inputState, ref nextState, timeoutNanoseconds);
        }

        public static Int32 AAudioStream_getBufferSizeInFrames(AAudioStream stream)
        {
            return AAudioStream_getBufferSizeInFrames(stream.value);
        }

        public static Int32 AAudioStream_getFramesPerBurst(AAudioStream stream)
        {
            return AAudioStream_getFramesPerBurst(stream.value);
        }

        public static Int32 AAudioStream_getFramesPerDataCallback(AAudioStream stream)
        {
            return AAudioStream_getFramesPerDataCallback(stream.value);
        }

        public static Int32 AAudioStream_getBufferCapacityInFrames(AAudioStream stream)
        {
            return AAudioStream_getBufferCapacityInFrames(stream.value);
        }

        public static Int32 AAudioStream_getXRunCount(AAudioStream stream)
        {
            return AAudioStream_getXRunCount(stream.value);
        }

        public static aaudio_result_t AAudioStream_setBufferSizeInFrames(AAudioStream stream, Int32 numFrames)
        {
            return AAudioStream_setBufferSizeInFrames(stream.value, numFrames);
        }
        
        public static aaudio_sharing_mode_t AAudioStream_getSharingMode(AAudioStream stream)
        {
            return AAudioStream_getSharingMode(stream.value);
        }

        public static aaudio_performance_mode_t AAudioStream_getPerformanceMode(AAudioStream stream)
        {
            return AAudioStream_getPerformanceMode(stream.value);
        }

        public static aaudio_format_t AAudioStream_getFormat(AAudioStream stream)
        {
            return AAudioStream_getFormat(stream.value);
        }

        public static int AAudioStream_getChannelCount(AAudioStream stream)
        {
            return AAudioStream_getChannelCount(stream.value);
        }

        public static int AAudioStream_getSampleRate(AAudioStream stream)
        {
            return AAudioStream_getSampleRate(stream.value);
        }

        #endregion

        // ============================================================
        // Audio System
        // ============================================================

        /**
         * The text is the ASCII symbol corresponding to the returnCode,
         * or an English message saying the returnCode is unrecognized.
         * This is intended for developers to use when debugging.
         * It is not for display to users.
         *
         * Available since API level 26.
         *
         * @return pointer to a text representation of an AAudio result code.
         */
        //TODO:
        //[DllImport(AAudioLib)] private static extern const char* AAudio_convertResultToText(aaudio_result_t returnCode);

        /**
         * The text is the ASCII symbol corresponding to the stream state,
         * or an English message saying the state is unrecognized.
         * This is intended for developers to use when debugging.
         * It is not for display to users.
         *
         * Available since API level 26.
         *
         * @return pointer to a text representation of an AAudio state.
         */
        //TODO:
        //[DllImport(AAudioLib)] private static extern const char* AAudio_convertStreamStateToText(aaudio_stream_state_t state);

        // ============================================================
        // StreamBuilder
        // ============================================================

        /**
         * Create a StreamBuilder that can be used to open a Stream.
         *
         * The deviceId is initially unspecified, meaning that the current default device will be used.
         *
         * The default direction is {@link #AAUDIO_DIRECTION_OUTPUT}.
         * The default sharing mode is {@link #AAUDIO_SHARING_MODE_SHARED}.
         * The data format, samplesPerFrames and sampleRate are unspecified and will be
         * chosen by the device when it is opened.
         *
         * AAudioStreamBuilder_delete() must be called when you are done using the builder.
         *
         * Available since API level 26.
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_result_t AAudio_createStreamBuilder(ref IntPtr builder)
               ;

        /**
         * Request an audio device identified device using an ID.
         * On Android, for example, the ID could be obtained from the Java AudioManager.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_UNSPECIFIED},
         * in which case the primary device will be used.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param deviceId device identifier or {@link #AAUDIO_UNSPECIFIED}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setDeviceId(IntPtr builder,
                                                        Int32 deviceId);

        /**
         * Request a sample rate in Hertz.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_UNSPECIFIED}.
         * An optimal value will then be chosen when the stream is opened.
         * After opening a stream with an unspecified value, the application must
         * query for the actual value, which may vary by device.
         *
         * If an exact value is specified then an opened stream will use that value.
         * If a stream cannot be opened with the specified value then the open will fail.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param sampleRate frames per second. Common rates include 44100 and 48000 Hz.
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setSampleRate(IntPtr builder,
                                                          Int32 sampleRate);

        /**
         * Request a number of channels for the stream.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_UNSPECIFIED}.
         * An optimal value will then be chosen when the stream is opened.
         * After opening a stream with an unspecified value, the application must
         * query for the actual value, which may vary by device.
         *
         * If an exact value is specified then an opened stream will use that value.
         * If a stream cannot be opened with the specified value then the open will fail.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param channelCount Number of channels desired.
         */
        [DllImport(AAudioLib)] private static extern void AAudioStreamBuilder_setChannelCount(IntPtr builder, Int32 channelCount);

        /**
         * Identical to AAudioStreamBuilder_setChannelCount().
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param samplesPerFrame Number of samples in a frame.
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setSamplesPerFrame(IntPtr builder,
                                                               Int32 samplesPerFrame);

        /**
         * Request a sample data format, for example {@link #AAUDIO_FORMAT_PCM_I16}.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_UNSPECIFIED}.
         * An optimal value will then be chosen when the stream is opened.
         * After opening a stream with an unspecified value, the application must
         * query for the actual value, which may vary by device.
         *
         * If an exact value is specified then an opened stream will use that value.
         * If a stream cannot be opened with the specified value then the open will fail.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param format common formats are {@link #AAUDIO_FORMAT_PCM_FLOAT} and
         *               {@link #AAUDIO_FORMAT_PCM_I16}.
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setFormat(IntPtr builder,
                                                      aaudio_format_t format);

        /**
         * Request a mode for sharing the device.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_SHARING_MODE_SHARED}.
         *
         * The requested sharing mode may not be available.
         * The application can query for the actual mode after the stream is opened.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param sharingMode {@link #AAUDIO_SHARING_MODE_SHARED} or {@link #AAUDIO_SHARING_MODE_EXCLUSIVE}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setSharingMode(IntPtr builder,
                aaudio_sharing_mode_t sharingMode);

        /**
         * Request the direction for a stream.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_DIRECTION_OUTPUT}.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param direction {@link #AAUDIO_DIRECTION_OUTPUT} or {@link #AAUDIO_DIRECTION_INPUT}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setDirection(IntPtr builder,
                aaudio_direction_t direction);

        /**
         * Set the requested buffer capacity in frames.
         * The final AAudioStream capacity may differ, but will probably be at least this big.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_UNSPECIFIED}.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param numFrames the desired buffer capacity in frames or {@link #AAUDIO_UNSPECIFIED}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setBufferCapacityInFrames(IntPtr builder,
                Int32 numFrames);

        /**
         * Set the requested performance mode.
         *
         * Supported modes are {@link #AAUDIO_PERFORMANCE_MODE_NONE},
         * {@link #AAUDIO_PERFORMANCE_MODE_POWER_SAVING} * and {@link #AAUDIO_PERFORMANCE_MODE_LOW_LATENCY}.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_PERFORMANCE_MODE_NONE}.
         *
         * You may not get the mode you requested.
         * You can call AAudioStream_getPerformanceMode()
         * to find out the final mode for the stream.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param mode the desired performance mode, eg. {@link #AAUDIO_PERFORMANCE_MODE_LOW_LATENCY}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setPerformanceMode(IntPtr builder,
                aaudio_performance_mode_t mode);

        /**
         * Set the intended use case for the stream.
         *
         * The AAudio system will use this information to optimize the
         * behavior of the stream.
         * This could, for example, affect how volume and focus is handled for the stream.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_USAGE_MEDIA}.
         *
         * Available since API level 28.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param usage the desired usage, eg. {@link #AAUDIO_USAGE_GAME}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setUsage(IntPtr builder,
                aaudio_usage_t usage);

        /**
         * Set the type of audio data that the stream will carry.
         *
         * The AAudio system will use this information to optimize the
         * behavior of the stream.
         * This could, for example, affect whether a stream is paused when a notification occurs.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_CONTENT_TYPE_MUSIC}.
         *
         * Available since API level 28.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param contentType the type of audio data, eg. {@link #AAUDIO_CONTENT_TYPE_SPEECH}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setContentType(IntPtr builder,
                aaudio_content_type_t contentType);

        /**
         * Set the input (capture) preset for the stream.
         *
         * The AAudio system will use this information to optimize the
         * behavior of the stream.
         * This could, for example, affect which microphones are used and how the
         * recorded data is processed.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_INPUT_PRESET_VOICE_RECOGNITION}.
         * That is because VOICE_RECOGNITION is the preset with the lowest latency
         * on many platforms.
         *
         * Available since API level 28.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param inputPreset the desired configuration for recording
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setInputPreset(IntPtr builder,
                aaudio_input_preset_t inputPreset);

        /**
         * Specify whether this stream audio may or may not be captured by other apps or the system.
         *
         * The default is {@link #AAUDIO_ALLOW_CAPTURE_BY_ALL}.
         *
         * Note that an application can also set its global policy, in which case the most restrictive
         * policy is always applied. See {@link android.media.AudioAttributes#setAllowedCapturePolicy(int)}
         *
         * Available since API level 29.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param capturePolicy the desired level of opt-out from being captured.
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setAllowedCapturePolicy(IntPtr builder,
                aaudio_allowed_capture_policy_t capturePolicy);

        /** Set the requested session ID.
         *
         * The session ID can be used to associate a stream with effects processors.
         * The effects are controlled using the Android AudioEffect Java API.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_SESSION_ID_NONE}.
         *
         * If set to {@link #AAUDIO_SESSION_ID_ALLOCATE} then a session ID will be allocated
         * when the stream is opened.
         *
         * The allocated session ID can be obtained by calling AAudioStream_getSessionId()
         * and then used with this function when opening another stream.
         * This allows effects to be shared between streams.
         *
         * Session IDs from AAudio can be used with the Android Java APIs and vice versa.
         * So a session ID from an AAudio stream can be passed to Java
         * and effects applied using the Java AudioEffect API.
         *
         * Note that allocating or setting a session ID may result in a stream with higher latency.
         *
         * Allocated session IDs will always be positive and nonzero.
         *
         * Available since API level 28.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param sessionId an allocated sessionID or {@link #AAUDIO_SESSION_ID_ALLOCATE}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setSessionId(IntPtr builder,
                aaudio_session_id_t sessionId);


        /** Indicates whether this input stream must be marked as privacy sensitive or not.
         *
         * When true, this input stream is privacy sensitive and any concurrent capture
         * is not permitted.
         *
         * This is off (false) by default except when the input preset is {@link #AAUDIO_INPUT_PRESET_VOICE_COMMUNICATION}
         * or {@link #AAUDIO_INPUT_PRESET_CAMCORDER}.
         *
         * Always takes precedence over default from input preset when set explicitly.
         *
         * Only relevant if the stream direction is {@link #AAUDIO_DIRECTION_INPUT}.
         *
         * Added in API level 30.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param privacySensitive true if capture from this stream must be marked as privacy sensitive,
         * false otherwise.
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setPrivacySensitive(IntPtr builder,
                bool privacySensitive);


        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setDataCallback(IntPtr builder, AAudioStream_dataCallback callback, IntPtr userData);

        /**
         * Delete the resources associated with the StreamBuilder.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)] private static extern aaudio_result_t AAudioStreamBuilder_delete(IntPtr builder);


        /**
         * Delete the internal data structures associated with the stream created
         * by AAudioStreamBuilder_openStream().
         *
         * If AAudioStream_release() has not been called then it will be called automatically.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)] private static extern aaudio_result_t AAudioStream_close(IntPtr stream);

        /**
         * Open a stream based on the options in the StreamBuilder.
         *
         * AAudioStream_close() must be called when finished with the stream to recover
         * the memory and to free the associated resources.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param stream pointer to a variable to receive the new stream reference
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_result_t AAudioStreamBuilder_openStream(IntPtr builder, ref IntPtr stream);

        /**
         * Write data to the stream.
         *
         * The call will wait until the write is complete or until it runs out of time.
         * If timeoutNanos is zero then this call will not wait.
         *
         * Note that timeoutNanoseconds is a relative duration in wall clock time.
         * Time will not stop if the thread is asleep.
         * So it will be implemented using CLOCK_BOOTTIME.
         *
         * This call is "strong non-blocking" unless it has to wait for room in the buffer.
         *
         * If the call times out then zero or a partial frame count will be returned.
         *
         * Available since API level 26.
         *
         * @param stream A stream created using AAudioStreamBuilder_openStream().
         * @param buffer The address of the first sample.
         * @param numFrames Number of frames to write. Only complete frames will be written.
         * @param timeoutNanoseconds Maximum number of nanoseconds to wait for completion.
         * @return The number of frames actually written or a negative error.
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_result_t AAudioStream_write(IntPtr stream, IntPtr buffer, Int32 numFrames, Int64 timeoutNanoseconds);

        /**
         * Read data from the stream.
         *
         * The call will wait until the read is complete or until it runs out of time.
         * If timeoutNanos is zero then this call will not wait.
         *
         * Note that timeoutNanoseconds is a relative duration in wall clock time.
         * Time will not stop if the thread is asleep.
         * So it will be implemented using CLOCK_BOOTTIME.
         *
         * This call is "strong non-blocking" unless it has to wait for data.
         *
         * If the call times out then zero or a partial frame count will be returned.
         *
         * Available since API level 26.
         *
         * @param stream A stream created using AAudioStreamBuilder_openStream().
         * @param buffer The address of the first sample.
         * @param numFrames Number of frames to read. Only complete frames will be written.
         * @param timeoutNanoseconds Maximum number of nanoseconds to wait for completion.
         * @return The number of frames actually read or a negative error.
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_result_t AAudioStream_read(IntPtr stream, IntPtr buffer, Int32 numFrames, Int64 timeoutNanoseconds);


        /**
         * Set the requested data callback buffer size in frames.
         * See {@link #AAudioStream_dataCallback}.
         *
         * The default, if you do not call this function, is {@link #AAUDIO_UNSPECIFIED}.
         *
         * For the lowest possible latency, do not call this function. AAudio will then
         * call the dataProc callback function with whatever size is optimal.
         * That size may vary from one callback to another.
         *
         * Only use this function if the application requires a specific number of frames for processing.
         * The application might, for example, be using an FFT that requires
         * a specific power-of-two sized buffer.
         *
         * AAudio may need to add additional buffering in order to adapt between the internal
         * buffer size and the requested buffer size.
         *
         * If you do call this function then the requested size should be less than
         * half the buffer capacity, to allow double buffering.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param numFrames the desired buffer size in frames or {@link #AAUDIO_UNSPECIFIED}
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setFramesPerDataCallback(IntPtr builder, Int32 numFrames);


        /**
         * Request that AAudio call this function if any error occurs or the stream is disconnected.
         *
         * It will be called, for example, if a headset or a USB device is unplugged causing the stream's
         * device to be unavailable or "disconnected".
         * Another possible cause of error would be a timeout or an unanticipated internal error.
         *
         * In response, this function should signal or create another thread to stop
         * and close this stream. The other thread could then reopen a stream on another device.
         * Do not stop or close the stream, or reopen the new stream, directly from this callback.
         *
         * This callback will not be called because of actions by the application, such as stopping
         * or closing a stream.
         *
         * Note that the AAudio callbacks will never be called simultaneously from multiple threads.
         *
         * Available since API level 26.
         *
         * @param builder reference provided by AAudio_createStreamBuilder()
         * @param callback pointer to a function that will be called if an error occurs.
         * @param userData pointer to an application data structure that will be passed
         *          to the callback functions.
         */
        [DllImport(AAudioLib)]
        private static extern void AAudioStreamBuilder_setErrorCallback(IntPtr builder, AAudioStream_errorCallback callback, IntPtr userData);


        /**
         * Asynchronously request to start playing the stream. For output streams, one should
         * write to the stream to fill the buffer before starting.
         * Otherwise it will underflow.
         * After this call the state will be in {@link #AAUDIO_STREAM_STATE_STARTING} or
         * {@link #AAUDIO_STREAM_STATE_STARTED}.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)] private static extern aaudio_result_t AAudioStream_requestStart(IntPtr stream);


        /**
         * Asynchronous request for the stream to pause.
         * Pausing a stream will freeze the data flow but not flush any buffers.
         * Use AAudioStream_requestStart() to resume playback after a pause.
         * After this call the state will be in {@link #AAUDIO_STREAM_STATE_PAUSING} or
         * {@link #AAUDIO_STREAM_STATE_PAUSED}.
         *
         * This will return {@link #AAUDIO_ERROR_UNIMPLEMENTED} for input streams.
         * For input streams use AAudioStream_requestStop().
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)] private static extern aaudio_result_t AAudioStream_requestPause(IntPtr stream);

        /**
         * Asynchronous request for the stream to flush.
         * Flushing will discard any pending data.
         * This call only works if the stream is pausing or paused. TODO review
         * Frame counters are not reset by a flush. They may be advanced.
         * After this call the state will be in {@link #AAUDIO_STREAM_STATE_FLUSHING} or
         * {@link #AAUDIO_STREAM_STATE_FLUSHED}.
         *
         * This will return {@link #AAUDIO_ERROR_UNIMPLEMENTED} for input streams.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)] private static extern aaudio_result_t AAudioStream_requestFlush(IntPtr stream);

        /**
         * Asynchronous request for the stream to stop.
         * The stream will stop after all of the data currently buffered has been played.
         * After this call the state will be in {@link #AAUDIO_STREAM_STATE_STOPPING} or
         * {@link #AAUDIO_STREAM_STATE_STOPPED}.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)] private static extern aaudio_result_t AAudioStream_requestStop(IntPtr stream);

        /**
         * Query the current state of the client, eg. {@link #AAUDIO_STREAM_STATE_PAUSING}
         *
         * This function will immediately return the state without updating the state.
         * If you want to update the client state based on the server state then
         * call AAudioStream_waitForStateChange() with currentState
         * set to {@link #AAUDIO_STREAM_STATE_UNKNOWN} and a zero timeout.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         */
        [DllImport(AAudioLib)] private static extern aaudio_stream_state_t AAudioStream_getState(IntPtr stream);

        /**
         * Wait until the current state no longer matches the input state.
         *
         * This will update the current client state.
         *
         * <pre><code>
         * aaudio_result_t result = AAUDIO_OK;
         * aaudio_stream_state_t currentState = AAudioStream_getState(stream);
         * aaudio_stream_state_t inputState = currentState;
         * while (result == AAUDIO_OK && currentState != AAUDIO_STREAM_STATE_PAUSED) {
         *     result = AAudioStream_waitForStateChange(
         *                                   stream, inputState, &currentState, MY_TIMEOUT_NANOS);
         *     inputState = currentState;
         * }
         * </code></pre>
         *
         * Available since API level 26.
         *
         * @param stream A reference provided by AAudioStreamBuilder_openStream()
         * @param inputState The state we want to avoid.
         * @param nextState Pointer to a variable that will be set to the new state.
         * @param timeoutNanoseconds Maximum number of nanoseconds to wait for completion.
         * @return {@link #AAUDIO_OK} or a negative error.
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_result_t AAudioStream_waitForStateChange(IntPtr stream,
                aaudio_stream_state_t inputState, ref aaudio_stream_state_t nextState,
                Int64 timeoutNanoseconds);

        /**
         * Query the maximum number of frames that can be filled without blocking.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return buffer size in frames.
         */
        [DllImport(AAudioLib)] private static extern Int32 AAudioStream_getBufferSizeInFrames(IntPtr stream);

        /**
         * Query the number of frames that the application should read or write at
         * one time for optimal performance. It is OK if an application writes
         * a different number of frames. But the buffer size may need to be larger
         * in order to avoid underruns or overruns.
         *
         * Note that this may or may not match the actual device burst size.
         * For some endpoints, the burst size can vary dynamically.
         * But these tend to be devices with high latency.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return burst size
         */
        [DllImport(AAudioLib)] private static extern Int32 AAudioStream_getFramesPerBurst(IntPtr stream);

        /**
         * Query the size of the buffer that will be passed to the dataProc callback
         * in the numFrames parameter.
         *
         * This call can be used if the application needs to know the value of numFrames before
         * the stream is started. This is not normally necessary.
         *
         * If a specific size was requested by calling
         * AAudioStreamBuilder_setFramesPerDataCallback() then this will be the same size.
         *
         * If AAudioStreamBuilder_setFramesPerDataCallback() was not called then this will
         * return the size chosen by AAudio, or {@link #AAUDIO_UNSPECIFIED}.
         *
         * {@link #AAUDIO_UNSPECIFIED} indicates that the callback buffer size for this stream
         * may vary from one dataProc callback to the next.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return callback buffer size in frames or {@link #AAUDIO_UNSPECIFIED}
         */
        [DllImport(AAudioLib)]
        private static extern Int32 AAudioStream_getFramesPerDataCallback(IntPtr stream);

        /**
         * Query maximum buffer capacity in frames.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return  buffer capacity in frames
         */
        [DllImport(AAudioLib)] private static extern Int32 AAudioStream_getBufferCapacityInFrames(IntPtr stream);

        /**
         * An XRun is an Underrun or an Overrun.
         * During playing, an underrun will occur if the stream is not written in time
         * and the system runs out of valid data.
         * During recording, an overrun will occur if the stream is not read in time
         * and there is no place to put the incoming data so it is discarded.
         *
         * An underrun or overrun can cause an audible "pop" or "glitch".
         *
         * Note that some INPUT devices may not support this function.
         * In that case a 0 will always be returned.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return the underrun or overrun count
         */
        [DllImport(AAudioLib)] private static extern Int32 AAudioStream_getXRunCount(IntPtr stream);

        /**
         * This can be used to adjust the latency of the buffer by changing
         * the threshold where blocking will occur.
         * By combining this with AAudioStream_getXRunCount(), the latency can be tuned
         * at run-time for each device.
         *
         * This cannot be set higher than AAudioStream_getBufferCapacityInFrames().
         *
         * Note that you will probably not get the exact size you request.
         * You can check the return value or call AAudioStream_getBufferSizeInFrames()
         * to see what the actual final size is.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @param numFrames requested number of frames that can be filled without blocking
         * @return actual buffer size in frames or a negative error
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_result_t AAudioStream_setBufferSizeInFrames(IntPtr stream, Int32 numFrames);

        /**
         * Provide actual sharing mode.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return  actual sharing mode
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_sharing_mode_t AAudioStream_getSharingMode(IntPtr stream);

        /**
         * Get the performance mode used by the stream.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_performance_mode_t AAudioStream_getPerformanceMode(IntPtr stream);

        /**
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return actual data format
         */
        [DllImport(AAudioLib)]
        private static extern aaudio_format_t AAudioStream_getFormat(IntPtr stream);

        /**
         * A stream has one or more channels of data.
         * A frame will contain one sample for each channel.
         *
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return actual number of channels
         */
        [DllImport(AAudioLib)] 
        private static extern int AAudioStream_getChannelCount(IntPtr stream);

        /**
         * Available since API level 26.
         *
         * @param stream reference provided by AAudioStreamBuilder_openStream()
         * @return actual sample rate
         */
        [DllImport(AAudioLib)]
        private static extern int AAudioStream_getSampleRate(IntPtr stream);


        /**
         * Control whether AAudioStreamBuilder_openStream() will use the new MMAP data path
         * or the older "Legacy" data path.
         *
         * This will only affect the current process.
         *
         * If unspecified then the policy will be based on system properties or configuration.
         *
         * @note This is only for testing. Do not use this in an application.
         * It may change or be removed at any time.
         *
         * @param policy AAUDIO_UNSPECIFIED, AAUDIO_POLICY_NEVER, AAUDIO_POLICY_AUTO, or AAUDIO_POLICY_ALWAYS
         * @return AAUDIO_OK or a negative error
         */
        [DllImport(AAudioLib)]
        public static extern aaudio_result_t AAudio_setMMapPolicy(aaudio_policy_t policy);
        /**
         * Get the current MMAP policy set by AAudio_setMMapPolicy().
         *
         * @note This is only for testing. Do not use this in an application.
         * It may change or be removed at any time.
         *
         * @return current policy
         */
        [DllImport(AAudioLib)]
        public static extern aaudio_policy_t AAudio_getMMapPolicy();
        /**
         * Return true if the stream uses the MMAP data path versus the legacy path.
         *
         * @note This is only for testing. Do not use this in an application.
         * It may change or be removed at any time.
         *
         * @return true if the stream uses ther MMAP data path
         */
        public static bool AAudioStream_isMMapUsed(AAudioStream stream)
        {
            return AAudioStream_isMMapUsed(stream.value);
        }

        [DllImport(AAudioLib)]
        private static extern bool AAudioStream_isMMapUsed(IntPtr stream);


    }
}

