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
		//public override UIWindow Window {
		//	get;
		//	set;
		//}

		//public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		//{ 
		//	//Window = new UIWindow (UIScreen.MainScreen.Bounds);
		//	//Window.MakeKeyAndVisible ();

		//	//Window.RootViewController = new MainViewController ();

		//	return true;
		//}

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            LoadApplication(new App.Shared.App());
            return base.FinishedLaunching(app, options);
        }
    }
 
}


