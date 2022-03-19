namespace ElysiumPlayer.AAudio
{
    /**
     * Defines the audio source.
     * An audio source defines both a default physical source of audio signal, and a recording
     * configuration.
     *
     * Note that these match the equivalent values in MediaRecorder.AudioSource in the Android Java API.
     *
     * Added in API level 28.
     */
    public enum aaudio_input_preset_t
    {
        /**
         * Use this preset when other presets do not apply.
         */
        AAUDIO_INPUT_PRESET_GENERIC = 1,

        /**
         * Use this preset when recording video.
         */
        AAUDIO_INPUT_PRESET_CAMCORDER = 5,

        /**
         * Use this preset when doing speech recognition.
         */
        AAUDIO_INPUT_PRESET_VOICE_RECOGNITION = 6,

        /**
         * Use this preset when doing telephony or voice messaging.
         */
        AAUDIO_INPUT_PRESET_VOICE_COMMUNICATION = 7,

        /**
         * Use this preset to obtain an input with no effects.
         * Note that this input will not have automatic gain control
         * so the recorded volume may be very low.
         */
        AAUDIO_INPUT_PRESET_UNPROCESSED = 9,

        /**
         * Use this preset for capturing audio meant to be processed in real time
         * and played back for live performance (e.g karaoke).
         * The capture path will minimize latency and coupling with playback path.
         * Available since API level 29.
         */
        AAUDIO_INPUT_PRESET_VOICE_PERFORMANCE = 10,

    }
}