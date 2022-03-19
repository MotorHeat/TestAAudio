using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using TestAAudio.Droid;
using System.Collections.Generic;
using Android;

namespace TestAAudio2.Droid
{
    [Activity(Label = "TestAAudio2", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var permissionsToRequest = new List<string>();
            if (CheckCallingOrSelfPermission(Manifest.Permission.RecordAudio) != Permission.Granted) permissionsToRequest.Add(Manifest.Permission.RecordAudio);
            if (CheckCallingOrSelfPermission(Manifest.Permission.ModifyAudioSettings) != Permission.Granted) permissionsToRequest.Add(Manifest.Permission.ModifyAudioSettings);

            if (permissionsToRequest.Count > 0) RequestPermissions(permissionsToRequest.ToArray(), 101);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            DependencyConfig.RegisterServices();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}