using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Net;

namespace MotoWebShop.MobileApp.Droid
{
    [Activity(Label = "MotoWebShop.MobileApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        class ToastMaker : IToast
        {
            private Context context;

            public ToastMaker(Context context)
            {
                this.context = context;
            }

            public void Show(string text, Toast.Time time)
            {
                Android.Widget.Toast.MakeText(context, text, time == Toast.Time.Short ? ToastLength.Short : ToastLength.Long).Show();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new ToastMaker(this.BaseContext)));
        }
    }
}