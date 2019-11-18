using MotoWebShop.MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MotoWebShop.MobileApp.Pages
{
    class ToolBar
    {
        public static void Make(ContentPage page, Action onLogout = null)
        {
            page.ToolbarItems.Clear();
            Api api = Api.Instance;

            ToolbarItem cartItem = new ToolbarItem($"Cart ({Cart.Instance.Items.Sum(x => x.Value)})", "", () => { });
            cartItem.Order = ToolbarItemOrder.Secondary;
            cartItem.Clicked += (_, __) => { page.Navigation.PushAsync(new CartPage()); };
            page.ToolbarItems.Add(cartItem);

            if (api.IsLoggedIn)
            {
                ToolbarItem usernameItem = new ToolbarItem(api.Username, "", () => { });
                usernameItem.Order = ToolbarItemOrder.Secondary;
                page.ToolbarItems.Add(usernameItem);

                ToolbarItem logoutItem = new ToolbarItem("Logout", "", () => { });
                logoutItem.Order = ToolbarItemOrder.Secondary;
                logoutItem.Clicked += (_, __) => { api.Logout(); page.Navigation.PopToRootAsync(); onLogout?.Invoke(); Make(page); };
                page.ToolbarItems.Add(logoutItem);
            }
            else
            {
                ToolbarItem loginItem = new ToolbarItem("Login", "", () => { });
                loginItem.Order = ToolbarItemOrder.Secondary;
                loginItem.Clicked += (_, __) => { page.Navigation.PushAsync(new LoginPage()); };
                page.ToolbarItems.Add(loginItem);

                ToolbarItem registerItem = new ToolbarItem("Register", "", () => { });
                registerItem.Order = ToolbarItemOrder.Secondary;
                registerItem.Clicked += (_, __) => { page.Navigation.PushAsync(new RegisterPage()); };
                page.ToolbarItems.Add(registerItem);
            }
        }
    }
}
