using MotoWebShop.Common;
using MotoWebShop.MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MotoWebShop.MobileApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailsPage : ContentPage
	{
        private Item item;

		public ItemDetailsPage()
		{
			InitializeComponent();
		}

        public ItemDetailsPage(Item item)
        {
            InitializeComponent();
            this.item = item;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToolBar.Make(this);

            labelName.Text = $"Name: {item.Name}";
            labelPrice.Text = $"Price: {item.Price}";
            labelDescription.Text = item.Description;
        }

        private async void ButtonCart_Clicked(object sender, EventArgs e)
        {
            try
            {
                int amount = int.Parse(EntryAmount.Text);

                if (amount > 0)
                {
                    Cart.Instance.AddCartItem(item, amount);
                    Toast.Show($"{item.Name} ({amount}) added to the cart!");
                    EntryAmount.Text = "";
                    ToolBar.Make(this);
                }
                else
                {
                    Toast.Show("Amount should be greather than 0!");
                }
            }
            catch
            {
                Toast.Show("Write only digits to the amount field!");
            }
        }
    }
}