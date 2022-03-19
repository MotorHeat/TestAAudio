using System;
using System.Collections.Generic;
using System.Text;
using TestAAudio.Models;

namespace TestAAudio.Services
{
    public enum StreamDirection
    {
        Input,
        Output,
    }

    public class StreamsData
    {
        public IDisposable StreamCalbacks { get; set; }
        public AaudioStreamStatus InputStreamStatus { get; set; }
        public AaudioStreamStatus OutputStreamStatus { get; set; }

    }
    public interface IAudioService
    {
        IntPtr CreateStream(StreamDirection streamDirection, AaudioStreamCreateParams streamParams);
        bool CloseStream(IntPtr stream);
        AaudioStreamStatus GetStreamStatus(IntPtr stream);

        StreamsData CreateStreams(AaudioStreamCreateParams inputStreamParams, AaudioStreamCreateParams outputStreamParams);
    }
}
