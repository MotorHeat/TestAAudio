namespace ElysiumPlayer.AAudio
{
    /**
     * Return one of these values from the data callback function.
     */
    public enum aaudio_data_callback_result_t
    {
        /**
         * Continue calling the callback.
         */
        AAUDIO_CALLBACK_RESULT_CONTINUE = 0,

        /**
         * Stop calling the callback.
         *
         * The application will still need to call AAudioStream_requestPause()
         * or AAudioStream_requestStop().
         */
        AAUDIO_CALLBACK_RESULT_STOP,

    }
}