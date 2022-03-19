namespace ElysiumPlayer.AAudio
{
    public static class Constants
    {
        public const int AAUDIO_SYSTEM_USAGE_OFFSET = 1000;

    }

    /**
     * The USAGE attribute expresses "why" you are playing a sound, what is this sound used for.
     * This information is used by certain platforms or routing policies
     * to make more refined volume or routing decisions.
     *
     * Note that these match the equivalent values in {@link android.media.AudioAttributes}
     * in the Android Java API.
     *
     * Added in API level 28.
     */

    public enum aaudio_usage_t
    {
        /**
 * Use this for streaming media, music performance, video, podcasts, etcetera.
 */
        AAUDIO_USAGE_MEDIA = 1,

        /**
         * Use this for voice over IP, telephony, etcetera.
         */
        AAUDIO_USAGE_VOICE_COMMUNICATION = 2,

        /**
         * Use this for sounds associated with telephony such as busy tones, DTMF, etcetera.
         */
        AAUDIO_USAGE_VOICE_COMMUNICATION_SIGNALLING = 3,

        /**
         * Use this to demand the users attention.
         */
        AAUDIO_USAGE_ALARM = 4,

        /**
         * Use this for notifying the user when a message has arrived or some
         * other background event has occured.
         */
        AAUDIO_USAGE_NOTIFICATION = 5,

        /**
         * Use this when the phone rings.
         */
        AAUDIO_USAGE_NOTIFICATION_RINGTONE = 6,

        /**
         * Use this to attract the users attention when, for example, the battery is low.
         */
        AAUDIO_USAGE_NOTIFICATION_EVENT = 10,

        /**
         * Use this for screen readers, etcetera.
         */
        AAUDIO_USAGE_ASSISTANCE_ACCESSIBILITY = 11,

        /**
         * Use this for driving or navigation directions.
         */
        AAUDIO_USAGE_ASSISTANCE_NAVIGATION_GUIDANCE = 12,

        /**
         * Use this for user interface sounds, beeps, etcetera.
         */
        AAUDIO_USAGE_ASSISTANCE_SONIFICATION = 13,

        /**
         * Use this for game audio and sound effects.
         */
        AAUDIO_USAGE_GAME = 14,

        /**
         * Use this for audio responses to user queries, audio instructions or help utterances.
         */
        AAUDIO_USAGE_ASSISTANT = 16,

        /**
         * Use this in case of playing sounds in an emergency.
         * Privileged MODIFY_AUDIO_ROUTING permission required.
         */
        AAUDIO_SYSTEM_USAGE_EMERGENCY = Constants.AAUDIO_SYSTEM_USAGE_OFFSET,

        /**
         * Use this for safety sounds and alerts, for example backup camera obstacle detection.
         * Privileged MODIFY_AUDIO_ROUTING permission required.
         */
        AAUDIO_SYSTEM_USAGE_SAFETY = Constants.AAUDIO_SYSTEM_USAGE_OFFSET + 1,

        /**
         * Use this for vehicle status alerts and information, for example the check engine light.
         * Privileged MODIFY_AUDIO_ROUTING permission required.
         */
        AAUDIO_SYSTEM_USAGE_VEHICLE_STATUS = Constants.AAUDIO_SYSTEM_USAGE_OFFSET + 2,

        /**
         * Use this for traffic announcements, etc.
         * Privileged MODIFY_AUDIO_ROUTING permission required.
         */
        AAUDIO_SYSTEM_USAGE_ANNOUNCEMENT = Constants.AAUDIO_SYSTEM_USAGE_OFFSET + 3,

    }
}