namespace ElysiumPlayer.AAudio
{
    public enum aaudio_stream_state_t
    {
        AAUDIO_STREAM_STATE_UNINITIALIZED = 0,
        AAUDIO_STREAM_STATE_UNKNOWN,
        AAUDIO_STREAM_STATE_OPEN,
        AAUDIO_STREAM_STATE_STARTING,
        AAUDIO_STREAM_STATE_STARTED,
        AAUDIO_STREAM_STATE_PAUSING,
        AAUDIO_STREAM_STATE_PAUSED,
        AAUDIO_STREAM_STATE_FLUSHING,
        AAUDIO_STREAM_STATE_FLUSHED,
        AAUDIO_STREAM_STATE_STOPPING,
        AAUDIO_STREAM_STATE_STOPPED,
        AAUDIO_STREAM_STATE_CLOSING,
        AAUDIO_STREAM_STATE_CLOSED,
        AAUDIO_STREAM_STATE_DISCONNECTED
    }
}