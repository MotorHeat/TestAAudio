using TestAAudio.Models;

namespace TestAAudio.ViewModels
{
    public class AaudioStreamStatusViewModel : BaseViewModel
    {
        private bool exclusiveMode;
        private bool lowLatency;
        private bool isMmapUsed;
        private bool created;

        public bool Created
        {
            get { return created; }
            set { SetProperty(ref created, value); }
        }
        public bool ExclusiveMode
        {
            get { return exclusiveMode; }
            set { SetProperty(ref exclusiveMode, value); }
        }

        public bool LowLatency
        {
            get { return lowLatency; }
            set { SetProperty(ref lowLatency, value); }
        }

        public bool IsMmapUsed
        {
            get { return isMmapUsed; }
            set { SetProperty(ref isMmapUsed, value); }
        }

        public void Reset()
        {
            Created = false;
            ExclusiveMode = false;
            LowLatency = false;
            IsMmapUsed = false;
        }

        public void FromStatus(AaudioStreamStatus status)
        {
            Created = status.Created;
            ExclusiveMode = status.ExclusiveMode;
            LowLatency = status.LowLatency;
            IsMmapUsed = status.IsMmapUsed;
        }

    }
}
