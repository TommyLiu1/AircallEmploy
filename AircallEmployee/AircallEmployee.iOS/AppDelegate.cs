using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace AircallEmployee.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            XamForms.Controls.iOS.Calendar.Init();
            CachedImageRenderer.Init();
            LoadApplication(new App());
            var success = base.FinishedLaunching(app, options);

            if (success)
            {
                SessionManager.Instance.SessionDuration = TimeSpan.FromSeconds(20);
                SessionManager.Instance.OnSessionExpired += HandleSessionExpired;

                var allGesturesRecognizer = new AllGesturesRecognizer(delegate
                {
                    SessionManager.Instance.ExtendSession();
                });

                this.Window.AddGestureRecognizer(allGesturesRecognizer);
            }
            return success;
        }

        async void HandleSessionExpired(object sender, EventArgs e)
        {
            
        }

        class AllGesturesRecognizer : UIGestureRecognizer
        {
            public delegate void OnTouchesEnded();

            private OnTouchesEnded touchesEndedDelegate;

            public AllGesturesRecognizer(OnTouchesEnded touchesEnded)
            {
                this.touchesEndedDelegate = touchesEnded;
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                this.State = UIGestureRecognizerState.Failed;

                this.touchesEndedDelegate();

                base.TouchesEnded(touches, evt);
            }
        }
    }
}
