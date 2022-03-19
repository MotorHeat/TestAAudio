using System;

namespace ElysiumPlayer.AAudio
{
    public delegate aaudio_data_callback_result_t AAudioStream_dataCallback(
           IntPtr stream,
           IntPtr userData,
           IntPtr audioData,
           Int32 numFrames);
}