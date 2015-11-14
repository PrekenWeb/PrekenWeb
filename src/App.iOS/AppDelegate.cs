using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using App.Shared;

namespace App.iOS
{ 
	[Register ("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
    { 
  
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            LoadApplication(new App.Shared.App());
            return base.FinishedLaunching(app, options);
        }
    }
 
}


