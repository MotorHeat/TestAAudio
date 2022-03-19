namespace ElysiumPlayer.AAudio
{
    public enum aaudio_performance_mode_t
    {
        /**
         * No particular performance needs. Default.
         */
        AAUDIO_PERFORMANCE_MODE_NONE = 10,

        /**
         * Extending battery life is more important than low latency.
         *
         * This mode is not supported in input streams.
         * For input, mode NONE will be used if this is requested.
         */
        AAUDIO_PERFORMANCE_MODE_POWER_SAVING,

        /**
         * Reducing latency is more important than battery life.
         */
        AAUDIO_PERFORMANCE_MODE_LOW_LATENCY

    }
}