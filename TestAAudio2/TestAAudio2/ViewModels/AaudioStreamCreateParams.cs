using TestAAudio.Models;

namespace TestAAudio.ViewModels
{
    public class AaudioStreamCreateParamsViewModel : BaseViewModel
    {
        private bool exclusiveMode;
        private bool lowLatency;
        private bool alwaysUseMmap;

        public AaudioStreamCreateParamsViewModel()
        {
            ExclusiveMode = true;
            LowLatency = true;
            AlwaysUseMmap = true;
        }

        public bool ExclusiveMode
        {
            get { return exclusiveMode; }
            set { SetProperty(ref exclusiveMode, value); }
        }

        public bool LowLatency
        {
            get { return lowLatency; }
            set
            {
                SetProperty(
                    ref lowLatency, 
                    value,
                    onChanged: () =>
                      {
                          if (!value)
                          {
                              AlwaysUseMmap = false;
                          }
                      });
            }
        }

        public bool AlwaysUseMmap
        {
            get { return alwaysUseMmap; }
            set { SetProperty(ref alwaysUseMmap, value); }
        }

        public AaudioStreamCreateParams ToParams()
        {
            return new AaudioStreamCreateParams
            {
                AlwaysUseMmap = AlwaysUseMmap,
                ExclusiveMode = ExclusiveMode,
                LowLatency = LowLatency,
            };
        }

    }
}
