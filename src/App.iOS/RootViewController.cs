using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using App.Shared;
using System.Linq;
using System.Collections.Generic;
using Prekenweb.Models.Dtos;

namespace App.iOS
{
	partial class RootViewController : UIViewController
	{ 

		public RootViewController (IntPtr handle) : base (handle)
		{

		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var test = new TestClass();
			var result = await test.NieuwePreken(); 
			//var alert = new UIAlertView("Title", result.First().PreekTitel,null,"ok");
			//alert.Show(); 

			//TableView.ContentInset = new UIEdgeInsets (this.TopLayoutGuide.Length, 0, 0, 0);
			TableView.Bounds = this.View.Bounds;
			TableView.Source = new PreekTableSource(result.ToList()); 
		}
	}

}
