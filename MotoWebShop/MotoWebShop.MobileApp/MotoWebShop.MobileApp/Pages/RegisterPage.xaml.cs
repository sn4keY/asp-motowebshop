﻿using MotoWebShop.MobileApp.Model;
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
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text;
            string password = EntryPassword.Text;

            if (username.Length > 0 && password.Length > 0)
            {
                SetInputEnabled(false);
                bool result = await Api.Instance.Register(username, password);

                if (result)
                {
                    Toast.Show("Registered successfully");
                    Navigation.PopAsync();
                    Navigation.PushAsync(new LoginPage(EntryUsername.Text));
                }
                else
                {
                    Toast.Show("An error occured!");
                    SetInputEnabled(true);
                }
            }
            else
            {
                Toast.Show("Enter your username and password!");
            }
        }

        private void SetInputEnabled(bool enabled)
        {
            EntryUsername.IsEnabled = enabled;
            EntryPassword.IsEnabled = enabled;
            ButtonRegister.IsEnabled = enabled;
        }
    }
}