using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ElysiumPlayer.AAudio.AAudio;
namespace ElysiumPlayer.AAudio
{
    public static class AAudioStreamExtensions
    {
        /**
        * Close the stream. AudioStream::close() is a blocking call so
        * the application does not need to add synchronization between
        * onAudioReady() function and the thread calling close().
        * [the closing thread is the UI thread in this sample].
        * @param stream the stream to close
        */
        public static void CloseStream(this ref AAudioStream stream)
        {
            if (stream.Initialized)
            {
                var result = AAudioStream_requestStop(stream);
                if (result != aaudio_result_t.AAUDIO_OK)
                {
                    //LOGW("Error stopping stream: %s", oboe::convertToText(result));
                }
                result = AAudioStream_close(stream);
                if (result != aaudio_result_t.AAUDIO_OK)
                {
                    //LOGE("Error closing stream: %s", oboe::convertToText(result));
                }
                else
                {
                    //LOGW("Successfully closed streams");
                }
                stream = AAudioStream.Empty;
            }
        }

        public static IntPtr AsIntPtr(this ref AAudioStream stream)
        {
            return stream.value;
        }
    }
}