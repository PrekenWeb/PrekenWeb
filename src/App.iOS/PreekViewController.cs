using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using App.Shared;
using System.Linq;
using System.Drawing;
using Prekenweb.Models.Dtos;

namespace App.iOS
{
	partial class PreekViewController : UITableViewController
	{

		public PreekViewController ()
		{

		}

		public PreekViewController (Zoekopdracht zoekopdracht)
		{

		}

		protected LoadingOverlay _loadPop = null;
 
		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			 
			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);
			this.View.Add ( this._loadPop );
			 
			//var test = new TestClass();
			//var result = await test.NieuwePreken();  

			 //TableView.Source = new PreekTableSource(result.ToList()); 

			this._loadPop.Hide ();
		}
	} 
}
