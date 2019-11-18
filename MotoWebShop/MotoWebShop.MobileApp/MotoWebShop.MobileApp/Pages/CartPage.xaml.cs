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
	public partial class CartPage : ContentPage
	{
		public CartPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToolBar.Make(this);
            ListViewCartItems.ItemsSource = Cart.Instance.Items;
        }

        private async void ButtonRemove_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonDetails_Clicked(object sender, EventArgs e)
        {
            var item = ((KeyValuePair<Item, int>)ListViewCartItems.SelectedItem);
            Navigation.PushAsync(new ItemDetailsPage(item.Key));
        }

        private async void ListViewCartItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            Item item = ((KeyValuePair<Item, int>)e.SelectedItem).Key;
            ListViewCartItems.SelectedItem = null;

            List<string> buttons = new List<string>();
            buttons.Add("Details");
            buttons.Add("Remove all");

            for (int i = 1; i < Cart.Instance.GetAmount(item); i++)
            {
                buttons.Add($"Remove {i}");
            }

            string result = await DisplayActionSheet(item.Name, "Cancel", "X", buttons.ToArray());

            switch(result)
            {
                case "Details":
                    await Navigation.PushAsync(new ItemDetailsPage(item));
                    return;
                case "Remove all":
                    Cart.Instance.RemoveCartItem(item);
                    Navigation.PopAsync();
                    Navigation.PushAsync(new CartPage());
                    return;
            }

            try
            {
                int amount = int.Parse(result.Split(' ')[1]);
                if (amount > 0)
                {
                    int newAmount = Cart.Instance.GetAmount(item) - amount;
                    if (newAmount > 0)
                    {
                        Cart.Instance.SetCartItemAmount(item, newAmount);
                        Navigation.PopAsync();
                        Navigation.PushAsync(new CartPage());
                    }
                }
            }
            catch
            {

            }

        }
    }
}