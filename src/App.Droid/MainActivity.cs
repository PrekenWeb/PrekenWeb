using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace App.Droid
{
    [Activity(Label = "App.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity :  FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new Shared.App());
        }
    }
}

