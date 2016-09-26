using System;
using System.IO;
using System.Net.Http;
using App.Shared.Db;
using App.Shared.Services;
using Prekenweb.Models.Dtos;
using Xamarin.Forms;

namespace App.Shared.Pages
{
    public class PreekPage : ContentPage
    {
        public PreekPage(IPreekService preekService)
        {
            this.SetBinding(ContentPage.TitleProperty, "PreekTitel");

            NavigationPage.SetHasNavigationBar(this, true);
            var nameLabel = new Label { Text = "PreekTitel" };

            var downloadButton = new Button { Text = "Download & Save" };
            downloadButton.Clicked += async (sender, e) =>
            {
                var preek = (PreekInLocalDb)BindingContext;
                preek.LocalFilePath = await DependencyService.Get<IPreekStorage>().DownloadPreek(preek.Id, preek.Filename).ConfigureAwait(false);
                await preekService.SetLocalPreekFilename(preek.Id, preek.LocalFilePath);
            };

            var playButton = new Button { Text = "Play" };
            playButton.Clicked += (sender, e) =>
            {
                var preek = (PreekInLocalDb)BindingContext;
                if (string.IsNullOrWhiteSpace(preek.LocalFilePath))
                {
                    //todo message 'download first'
                    return;
                }
                DependencyService.Get<IAudio>().PlayMp3File(preek.LocalFilePath);
            };

            var pauseButton = new Button { Text = "Pause" };
            pauseButton.Clicked += (sender, e) =>
            {
                var preek = (PreekInLocalDb)BindingContext;
                if (string.IsNullOrWhiteSpace(preek.LocalFilePath))
                {
                    //todo message 'download first'
                    return;
                }
                DependencyService.Get<IAudio>().Pause();
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children =
                {
                    nameLabel,
                    downloadButton,
                    playButton,
                    pauseButton
                }
            };
        }
    }
}