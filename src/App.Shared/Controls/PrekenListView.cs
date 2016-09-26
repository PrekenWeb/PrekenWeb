using System;
using System.Collections.Generic;
using App.Shared.Db;
using App.Shared.Pages;
using App.Shared.Services;
using Prekenweb.Models.Dtos;
using Xamarin.Forms;

namespace App.Shared.Controls
{
    public class PrekenListView : ListView
    {
        public PrekenListView(Action<object> onRefresh, IPreekService preekService)
        {
            ItemTemplate = new DataTemplate(typeof(PreekCell));

            IsPullToRefreshEnabled = true;
            RefreshCommand = new Command(onRefresh);

            ItemSelected += (sender, e) =>
            {
                var preek = (PreekInLocalDb)e.SelectedItem;
                var todoPage = new PreekPage(preekService);
                todoPage.BindingContext = preek;

                Navigation.PushAsync(todoPage);
            };
        }

        public void RenderItems(IEnumerable<PreekInLocalDb> preken)
        {
            ItemsSource = preken;
        }
    }
}