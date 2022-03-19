namespace ElysiumPlayer.AAudio
{
    /**
     * The CONTENT_TYPE attribute describes "what" you are playing.
     * It expresses the general category of the content. This information is optional.
     * But in case it is known (for instance AAUDIO_CONTENT_TYPE_MOVIE for a
     * movie streaming service or AAUDIO_CONTENT_TYPE_SPEECH for
     * an audio book application) this information might be used by the audio framework to
     * enforce audio focus.
     *
     * Note that these match the equivalent values in {@link android.media.AudioAttributes}
     * in the Android Java API.
     *
     * Added in API level 28.
     */
    public enum aaudio_content_type_t
    {

        /**
         * Use this for spoken voice, audio books, etcetera.
         */
        AAUDIO_CONTENT_TYPE_SPEECH = 1,

        /**
         * Use this for pre-recorded or live music.
         */
        AAUDIO_CONTENT_TYPE_MUSIC = 2,

        /**
         * Use this for a movie or video soundtrack.
         */
        AAUDIO_CONTENT_TYPE_MOVIE = 3,

        /**
         * Use this for sound is designed to accompany a user action,
         * such as a click or beep sound made when the user presses a button.
         */
        AAUDIO_CONTENT_TYPE_SONIFICATION = 4

    }
}