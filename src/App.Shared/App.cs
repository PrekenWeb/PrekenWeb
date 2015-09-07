using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace App.Shared
{
    public class App : Application
    {

        public App()
        {
            MainPage = new NavigationPage(new PreekListPage());
        }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }
    }
}
