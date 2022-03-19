using TestAAudio.Droid.Services;
using TestAAudio.Services;
using Xamarin.Forms;

namespace TestAAudio.Droid
{
    public static class DependencyConfig
    {
        public static void RegisterServices()
        {
            DependencyService.Register<IAudioService, AudioService>();
        }
    }
}