using MotoWebShop.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotoWebShop.MobileApp.Model
{
    class Cart
    {
        private static Cart cart;

        public static Cart Instance
        {
            get
            {
                if (cart == null)
                    cart = new Cart();
                return cart;
            }
        }

        public Dictionary<Item, int> Items = new Dictionary<Item, int>();

        public void AddCartItem(Item item, int amount)
        {
            if (Items.ContainsKey(item) == false)
                Items.Add(item, 0);
            Items[item] += amount;
        }

        public void SetCartItemAmount(Item item, int amount)
        {
            AddCartItem(item, 0);
            Items[item] = amount;
        }

        public void RemoveCartItem(Item item)
        {
            Items.Remove(item);
        }

        public int GetAmount(Item item)
        {
            try
            {
                return Items[item];
            }
            catch
            {
                return 0;
            }
        }
    }
}
