using System;
using System.IO;
using System.Net.Http;
using App.Shared.Db;
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
            var nameLabel = new Label { Text = "PreekTitel" };

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var preek = (PreekInLocalDb)BindingContext;
                var localFilename = await DependencyService.Get<IPreekStorage>().DownloadPreek(preek.Id, preek.Filename).ConfigureAwait(false);
                DependencyService.Get<IAudio>().PlayMp3File(localFilename);
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children =
                {
                    nameLabel,
                    saveButton
                }
            };
        }
    }
}