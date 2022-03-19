namespace TestAAudio.Models
{
    public class AaudioStreamCreateParams
    {
        public bool AlwaysUseMmap { get; set; }
        public bool ExclusiveMode { get; set; }
        public bool LowLatency { get; set; }
    }
}
