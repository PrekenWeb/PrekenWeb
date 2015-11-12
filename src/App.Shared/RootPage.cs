using System.Linq;
using Prekenweb.Models.Dtos;
using Xamarin.Forms;

namespace App.Shared
{

	public class RootPage : TabbedPage
	{  

		public RootPage()
		{
			Title = "PW";
//			this.ItemsSource = new [] {
//				"Nieuw",
//				"Zoek",
//				"Gedwonload"
//			};

			this.Children.Add (new PreekListPage (new PrekenwebApiWrapper()));
 
		}
		 
	}
}