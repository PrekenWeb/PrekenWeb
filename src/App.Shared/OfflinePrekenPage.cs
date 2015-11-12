using Xamarin.Forms;

namespace App.Shared
{
    public class OfflinePrekenPage : ContentPage
    { 
        private readonly PrekenListView _listView;

        public OfflinePrekenPage(object localStorage)
        { 

            Title = "Gedownload";
            Icon = "check.png"; 

            var layout = new StackLayout();
            //layout.Children.Add(_listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //var items = await _api.NieuwePreken();
            //_listView.RenderItems(items);
        }
    }
}