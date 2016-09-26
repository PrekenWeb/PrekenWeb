using App.Shared.Controls;
using App.Shared.Services;
using Xamarin.Forms;

namespace App.Shared.Pages
{
    public class NieuwePrekenPage : ContentPage
    {
        private readonly IPreekService _preekService; 
        private readonly PrekenListView _listView;

        public NieuwePrekenPage(IPreekService preekService)
        {
            _preekService = preekService; 

            Title = "Preken";
            Icon = "check.png";

            _listView = new PrekenListView(async x =>
            {
                await _preekService.UpdatePreken();

                var items = _preekService.GetNieuwePreken();
                _listView.RenderItems(items);
                _listView.IsRefreshing = false;
            }, _preekService);

            var layout = new StackLayout();
            layout.Children.Add(_listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.Padding = new Thickness(0, 50, 0, 0);
            Content = layout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _preekService.UpdatePreken();

            var items = _preekService.GetNieuwePreken();
            _listView.RenderItems(items);
        }
    }
}