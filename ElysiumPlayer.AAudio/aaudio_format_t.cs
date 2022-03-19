namespace ElysiumPlayer.AAudio
{
    public enum aaudio_format_t
    {
        AAUDIO_FORMAT_INVALID = -1,
        AAUDIO_FORMAT_UNSPECIFIED = 0,

        /**
         * This format uses the int16_t data type.
         * The maximum range of the data is -32768 to 32767.
         */
        AAUDIO_FORMAT_PCM_I16,

        /**
         * This format uses the float data type.
         * The nominal range of the data is [-1.0f, 1.0f).
         * Values outside that range may be clipped.
         *
         * See also 'floatData' at
         * https://developer.android.com/reference/android/media/AudioTrack#write(float[],%20int,%20int,%20int)
         */
        AAUDIO_FORMAT_PCM_FLOAT
    }
}