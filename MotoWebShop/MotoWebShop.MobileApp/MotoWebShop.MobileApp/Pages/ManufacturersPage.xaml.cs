﻿using MotoWebShop.Common;
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
	public partial class ManufacturersPage : ContentPage
	{
		public ManufacturersPage ()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToolBar.Make(this);
            Api.Instance.GetManufacturers(GetManufacturersResultHandler);
        }

        private void GetManufacturersResultHandler(IEnumerable<Manufacturer> manufacturers)
        {
            ListViewManufacturers.ItemsSource = manufacturers;
        }

        private void ListViewManufacturers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ListViewManufacturers.SelectedItem = null;

            Manufacturer manufacturer = (Manufacturer)e.SelectedItem;
            Navigation.PushAsync(new ModelsPage(manufacturer));
        }
    }
}