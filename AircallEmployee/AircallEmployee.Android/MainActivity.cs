
using Android.App;
using Android.Content.PM;
using Android.OS;
using Acr.UserDialogs;
using FFImageLoading.Forms.Droid;
using AircallEmployee.Utils;
using System;

namespace AircallEmployee.Droid
{
    [Activity(Label = "AircallEmployee", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MainTheme);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            XamForms.Controls.Droid.Calendar.Init();
            CachedImageRenderer.Init(true);
            LoadApplication(new App());

            SessionManager.Instance.SessionDuration = TimeSpan.FromMinutes(5);
            SessionManager.Instance.OnSessionExpired = HandleSessionExpired;
        }

        async void HandleSessionExpired(object sender, EventArgs e)
        {
            
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();

            SessionManager.Instance.ExtendSession();
        }
    }
}