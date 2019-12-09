using MotoWebShop.MobileApp.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MotoWebShop.MobileApp
{
    public partial class App : Application
    {
        public App(IToast toast = null)
        {
            Toast.Handler = toast;

            InitializeComponent();

            MainPage = new NavigationPage(new ManufacturersPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
