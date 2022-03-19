namespace ElysiumPlayer.AAudio
{
    /**
     * Specifying if audio may or may not be captured by other apps or the system.
     *
     * Note that these match the equivalent values in {@link android.media.AudioAttributes}
     * in the Android Java API.
     *
     * Added in API level 29.
     */
    public enum aaudio_allowed_capture_policy_t
    {
        /**
         * Indicates that the audio may be captured by any app.
         *
         * For privacy, the following usages can not be recorded: AAUDIO_VOICE_COMMUNICATION*,
         * AAUDIO_USAGE_NOTIFICATION*, AAUDIO_USAGE_ASSISTANCE* and {@link #AAUDIO_USAGE_ASSISTANT}.
         *
         * On {@link android.os.Build.VERSION_CODES#Q}, this means only {@link #AAUDIO_USAGE_MEDIA}
         * and {@link #AAUDIO_USAGE_GAME} may be captured.
         *
         * See {@link android.media.AudioAttributes#ALLOW_CAPTURE_BY_ALL}.
         */
        AAUDIO_ALLOW_CAPTURE_BY_ALL = 1,
        /**
         * Indicates that the audio may only be captured by system apps.
         *
         * System apps can capture for many purposes like accessibility, user guidance...
         * but have strong restriction. See
         * {@link android.media.AudioAttributes#ALLOW_CAPTURE_BY_SYSTEM} for what the system apps
         * can do with the capture audio.
         */
        AAUDIO_ALLOW_CAPTURE_BY_SYSTEM = 2,
        /**
         * Indicates that the audio may not be recorded by any app, even if it is a system app.
         *
         * It is encouraged to use {@link #AAUDIO_ALLOW_CAPTURE_BY_SYSTEM} instead of this value as system apps
         * provide significant and useful features for the user (eg. accessibility).
         * See {@link android.media.AudioAttributes#ALLOW_CAPTURE_BY_NONE}.
         */
        AAUDIO_ALLOW_CAPTURE_BY_NONE = 3,
    }
}