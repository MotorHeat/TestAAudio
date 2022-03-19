namespace ElysiumPlayer.AAudio
{
    /**
     * These may be used with AAudioStreamBuilder_setSessionId().
     *
     * Added in API level 28.
     */
    public enum aaudio_session_id_t
    {
        /**
         * Do not allocate a session ID.
         * Effects cannot be used with this stream.
         * Default.
         *
         * Added in API level 28.
         */
        AAUDIO_SESSION_ID_NONE = -1,

        /**
         * Allocate a session ID that can be used to attach and control
         * effects using the Java AudioEffects API.
         * Note that using this may result in higher latency.
         *
         * Note that this matches the value of AudioManager.AUDIO_SESSION_ID_GENERATE.
         *
         * Added in API level 28.
         */
        AAUDIO_SESSION_ID_ALLOCATE = 0,
    }
}