namespace ElysiumPlayer.AAudio
{
    public enum aaudio_sharing_mode_t
    {
        /// <summary>
        /// This will be the only stream using a particular source or sink.
        /// This mode will provide the lowest possible latency.
        /// You should close EXCLUSIVE streams immediately when you are not using them.
        /// </summary>
        AAUDIO_SHARING_MODE_EXCLUSIVE,

        /// <summary>
        /// Multiple applications will be mixed by the AAudio Server.
        /// This will have higher latency than the EXCLUSIVE mode.
        /// </summary>
        AAUDIO_SHARING_MODE_SHARED

    }
}