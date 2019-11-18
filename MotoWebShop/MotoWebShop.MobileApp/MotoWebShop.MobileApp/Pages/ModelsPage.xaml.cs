using MotoWebShop.Common;
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
	public partial class ModelsPage : ContentPage
	{
        private Manufacturer manufacturer;

		public ModelsPage()
		{
			InitializeComponent();
		}

        public ModelsPage(Manufacturer manufacturer)
        {
            InitializeComponent();
            this.manufacturer = manufacturer;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToolBar.Make(this);
            ListViewModels.ItemsSource = await Api.Instance.GetModels(manufacturer.Id);
        }

        private void ListViewModels_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ListViewModels.SelectedItem = null;

            Common.Model model = (Common.Model)e.SelectedItem;
            Navigation.PushAsync(new CategoriesPage(model));
        }
    }
}