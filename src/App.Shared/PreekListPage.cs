using System.Linq;
using Prekenweb.Models.Dtos;
using Xamarin.Forms;

namespace App.Shared
{
	public class PreekListPage : ContentPage
	{ 
		readonly ListView _listView;

		private readonly IPrekenwebApiWrapper _api;

		public PreekListPage(IPrekenwebApiWrapper api)
		{
			_api = api;

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
			Icon = "check.png";
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing(); 

			 var result = await _api.NieuwePreken(); 
			_listView.ItemsSource = result.ToList(); 
		}
	} 

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