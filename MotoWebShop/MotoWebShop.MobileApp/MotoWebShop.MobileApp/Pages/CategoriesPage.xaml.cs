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
	public partial class CategoriesPage : ContentPage
	{
        private Common.Model model;

		public CategoriesPage()
		{
			InitializeComponent();
		}

        public CategoriesPage(Common.Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToolBar.Make(this);
            ListViewCategories.ItemsSource = await Api.Instance.GetCategories();
        }

        private void GetCategoriesResultHandler(IEnumerable<Category> categories)
        {
            ListViewCategories.ItemsSource = categories;
        }

        private void ListViewCategories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ListViewCategories.SelectedItem = null;

            Category category = (Category)e.SelectedItem;
            Navigation.PushAsync(new ItemsPage(model, category));
        }
    }
}