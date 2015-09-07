using System.Linq;
using Prekenweb.Models.Dtos;
using Xamarin.Forms;

namespace App.Shared
{
    public class PreekListPage : ContentPage
    { 
        readonly ListView _listView;

        public PreekListPage()
        {
            Title = "Preken";

            _listView = new ListView();
            _listView.ItemTemplate = new DataTemplate  (typeof(PreekCell));
            _listView.ItemSelected += (sender, e) =>
            {
                var preek = (Preek)e.SelectedItem;
                var todoPage = new PreekPage();
                todoPage.BindingContext = preek; 

                Navigation.PushAsync(todoPage);
            };

            var layout = new StackLayout();
            if (Device.OS == TargetPlatform.WinPhone)
            { // WinPhone doesn't have the title showing
                layout.Children.Add(new Label
                {
                    Text = "Preken",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                });
            }
            layout.Children.Add(_listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing(); 

            var test = new TestClass();
            var result = await test.NieuwePreken(); 
            _listView.ItemsSource = result.ToList(); 
        }
    }
}