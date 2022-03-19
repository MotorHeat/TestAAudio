namespace TestAAudio.Models
{
    public class AaudioStreamStatus
    {
        public bool Created { get; set; }
        public bool ExclusiveMode { get; set; }
        public bool LowLatency { get; set; }
        public bool IsMmapUsed { get; set; }
    }
}
