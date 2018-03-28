using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Robotics.Mobile.Core.Bluetooth.LE;
using UIKit;

namespace indoor.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            App.SetAdapter(Adapter.Current);
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
