using Prekenweb.Models.Dtos;
using Xamarin.Forms;

namespace App.Shared.Pages
{
    public class PreekPage : ContentPage
    {
        public PreekPage()
        {
            this.SetBinding(ContentPage.TitleProperty, "PreekTitel");

            NavigationPage.SetHasNavigationBar(this, true);
            var nameLabel = new Label {Text = "PreekTitel" };

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += (sender, e) => {
                var todoItem = (Preek)BindingContext;

                //var webClient = new WebClient();

                //webClient.DownloadStringCompleted += (s, e) => {
                //    var text = e.Result; // get the downloaded text
                //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //    string localFilename = "downloaded.txt";
                //    string localPath = Path.Combine(documentsPath, localFilename);
                //    File.WriteAllText(localpath, text); // writes to local storage
                //};
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children =
                {
                    nameLabel
                }
            };
        }
    }
}