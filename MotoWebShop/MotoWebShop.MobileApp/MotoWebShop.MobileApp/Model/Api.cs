using MotoWebShop.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MotoWebShop.MobileApp.Model
{
    class Api
    {
        private const string url = "https://";
        private static Api instance;

        public Api Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Api();
                }

                return instance;
            }
        }

        private Api()
        {

        }

        private WebClient webClient = new WebClient();
        private string authKey;

        public bool IsLoggedIn => authKey != null;

        public bool Login(string username, string password)
        {
            return false;
        }

        public bool Register(string username, string password)
        {
            return false;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return null;
        }

        public IEnumerable<Category> GetCategories(int manufacturerId)
        {
            return null;
        }

        public IEnumerable<Item> GetItems(int manufacturerId, int categoryId)
        {
            return null;
        }
    }
}