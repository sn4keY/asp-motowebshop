using MotoWebShop.MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MotoWebShop.MobileApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent();
		}

        public LoginPage(string defaultUsername)
        {
            InitializeComponent();
            EntryUsername.Text = defaultUsername;
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text;
            string password = EntryPassword.Text;

            if (username.Length > 0 && password.Length > 0)
            {
                Api api = Api.Instance;
                SetInputEnabled(false);
                bool result = await api.Login(username, password);

                if (result)
                {
                    Toast.Show("Logged in");
                    Navigation.PopAsync();
                }
                else
                {
                    SetInputEnabled(true);
                    Toast.Show("Bad password or username");
                }
            }
            else
            {
                Toast.Show("Enter your username and password!");
            }
        }

        private void SetInputEnabled(bool enabled)
        {
            ButtonLogin.IsEnabled = enabled;
            EntryUsername.IsEnabled = enabled;
            EntryPassword.IsEnabled = enabled;
        }
    }
}