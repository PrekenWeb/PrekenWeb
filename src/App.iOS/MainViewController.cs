using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using App.Shared;

namespace App.iOS
{
	partial class MainViewController : UIViewController
	{
		public MainViewController (IntPtr handle) : base (handle)
		{
			testButton.TouchUpInside += async (object sender, EventArgs e) => {
				var kees = new TestClass();
				var result = await kees.NieuwePreken();
			};
		}
	}
}
