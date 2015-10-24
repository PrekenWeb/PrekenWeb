using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace App.iOS
{
	partial class MainMenuViewController : UITableViewController
	{
		public MainMenuViewController ()
		{
			this.Title = "PrekenWeb";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var menuItems = new List<MenuItem> {
				new MenuItem {
					Label = "Nieuwe preken",
					Action = () => this.NavigationController.PushViewController (new PreekViewController (), true)
				}
			};

			TableView.Source = new MainMenuTableSource (menuItems);
		}
	}

	public class MenuItem
	{
		public string Label { get; set; }

		public Action Action { get; set; }
	}

	public class MainMenuTableSource : UITableViewSource
	{

		private static readonly string _cellId = "MainMenuCell";
		private readonly IList<MenuItem> _menuItems;

		public MainMenuTableSource (IList<MenuItem> menuItems)
		{
			_menuItems = menuItems;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _menuItems.Count;  
		}


		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{ 
			var item = _menuItems [indexPath.Row];
			item.Action (); 

			tableView.DeselectRow (indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (_cellId);

			var preek = _menuItems [indexPath.Row];

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _cellId);	

			cell.TextLabel.Text = preek.Label;
			//cell.DetailTextLabel.Text = preek.DatumPreek + " - " + preek.BijbeltekstOmschrijving;

			return cell;
		}
	}
}
