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
	public partial class ItemsPage : ContentPage
	{
        private Common.Model model;
        private Common.Category category;

		public ItemsPage()
		{
			InitializeComponent();
		}

        public ItemsPage(Common.Model model, Common.Category category)
        {
            InitializeComponent();
            this.model = model;
            this.category = category;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToolBar.Make(this);
            ListViewItems.ItemsSource = await Api.Instance.GetItems(model.Id, category.Id);
        }

        private void ListViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ListViewItems.SelectedItem = null;

            Item item = (Item)e.SelectedItem;
            Navigation.PushAsync(new ItemDetailsPage(item));
        }
    }
}
