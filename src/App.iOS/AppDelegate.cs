using App.iOS;
using App.Shared;
using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace App.iOS
{ 
	[Register ("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
    { 
  
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            LoadApplication(new Shared.App()); 
            return base.FinishedLaunching(app, options);
        } 
    }
}


