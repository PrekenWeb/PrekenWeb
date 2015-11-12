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

            this.Children.Add(new NieuwePrekenPage(new PrekenwebApiWrapper()));
            this.Children.Add(new OfflinePrekenPage(null));
            this.Children.Add(new PreekZoekenPage(new PrekenwebApiWrapper()));

        }

    }
}