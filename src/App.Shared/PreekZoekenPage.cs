using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.Shared
{
    public class PreekZoekenPage : ContentPage
    {
        private readonly IPrekenwebApiWrapper _api;
        private readonly PrekenListView _listView;
        private readonly PreekZoekenPageViewModel _viewModel;
        private bool IsUpdating;

        public PreekZoekenPage(IPrekenwebApiWrapper api)
        {
            _api = api;
            _viewModel = new PreekZoekenPageViewModel();
            BindingContext = _viewModel;

            Title = "Preken";
            Icon = "check.png";

            _listView = new PrekenListView();


            var textbox = new Entry
            {
                BindingContext = _viewModel,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            textbox.SetBinding<PreekZoekenPageViewModel>(Entry.TextProperty, x => x.ZoekTerm);

            var button = new Button
            {
                Text = "Zoek",

            };
            button.Clicked += async (sender, args) =>
            {
                await UpdateResults();
            };

            var textAndSearchButton = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { textbox, button },
                Padding = new Thickness(5)
            };
            var layout = new StackLayout();
            layout.Children.Add(textAndSearchButton);
            layout.Children.Add(_listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.Padding = new Thickness(10, 50, 10, 10);

            Content = layout;
        }

        private async Task UpdateResults()
        {
            if (string.IsNullOrEmpty(_viewModel.ZoekTerm)) return;

            while (IsUpdating)
            {
            }

            IsUpdating = true;

            var items = await _api.PreekZoeken(_viewModel.ZoekTerm);
            _listView.RenderItems(items);

            IsUpdating = false;

        }
    }
}