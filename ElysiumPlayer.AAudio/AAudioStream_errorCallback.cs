using System;

namespace ElysiumPlayer.AAudio
{
    public delegate void AAudioStream_errorCallback(
        IntPtr stream,
        IntPtr userData,
        aaudio_result_t error);
}