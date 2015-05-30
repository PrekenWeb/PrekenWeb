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

	public class PreekTableSource : UITableViewSource {

		private static readonly string preekCellId = "PreekCell";
		private readonly List<Preek> _preken; 

		public PreekTableSource (List<Preek> preken)
		{
			_preken = preken; 
		} 

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _preken.Count;  
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{ 
			var preek = _preken[indexPath.Row];

			new UIAlertView ("Preek", preek.PreekTitel, null, "OK", null).Show ();

			tableView.DeselectRow (indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (preekCellId);

			var preek = _preken[indexPath.Row];

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, preekCellId);	

			cell.TextLabel.Text = preek.PreekTitel;
			cell.DetailTextLabel.Text = preek.DatumPreek + " - " + preek.BijbeltekstOmschrijving;

			return cell;
		}   
	}
}
