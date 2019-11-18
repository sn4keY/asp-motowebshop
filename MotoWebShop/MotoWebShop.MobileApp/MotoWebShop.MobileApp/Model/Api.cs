using MotoWebShop.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotoWebShop.MobileApp.Model
{
    public class Response
    {
        public string Text { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }

        public Response(string text, HttpStatusCode statusCode, bool success)
        {
            Text = text;
            StatusCode = statusCode;
            Success = success;
        }

        public T As<T>(T defaultValue = default(T))
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Text);
            }
            catch
            {
                return defaultValue;
            }
        }
    }

    class Api
    {
        private const string url = "https://10.0.2.2:44370/";
        private static Api instance;

        public static Api Instance
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
        private HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());
        private string authKey = null;
        private DateTime authExpiration = DateTime.Now;

        public bool IsLoggedIn => authKey != null;
        public string Username { get; private set; }
        public string Password { get; private set; }

        private async Task<Response> PostData(string path, Dictionary<string, string> data)
        {
            try
            {
                if (data == null)
                {
                    data = new Dictionary<string, string>();
                }

                string fullUrl = url + path;
                Console.WriteLine($"URL: {fullUrl}");

                string jsonData = JsonConvert.SerializeObject(data);
                var task = await client.PostAsync(fullUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                var task2 = await task.Content.ReadAsStringAsync();
                Console.WriteLine($"Result code: {task.StatusCode}");
                Console.WriteLine($"Result:\n{task2}");
                return new Response(task2, task.StatusCode, task.IsSuccessStatusCode);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Response("", HttpStatusCode.ServiceUnavailable, false);
        }

        private async Task<Response> GetData(string path)
        {
            string fullUrl = url + path;
            Console.WriteLine($"URL: {fullUrl}");
            var task = await client.GetAsync(fullUrl);
            string json = await task.Content.ReadAsStringAsync();

            return new Response(json, task.StatusCode, task.IsSuccessStatusCode);
        }

        public async Task<bool> Login(string username, string password)
        {

            string path = "Auth/Login";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("username", username);
            data.Add("password", password);
            var res = await PostData(path, data);

            var resData = res.As<Dictionary<string, string>>();

            if (resData != null && resData.ContainsKey("token"))
            {
                this.authKey = resData["token"];
                this.authExpiration = DateTime.Parse(resData["expiration"]);
                this.Username = username;
                this.Password = password;
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Register(string username, string password)
        {

            string path = "Auth/Register";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("email", username);
            data.Add("password", password);
            var res = await PostData(path, data);

            var resData = res.As<Dictionary<string, string>>();

            if (resData != null && resData.ContainsKey("username") && resData["username"] == username)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            authKey = null;
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturers()
        {
            string path = "api/Values/manufacturers";
            var res = await GetData(path);
            return res.As<List<Manufacturer>>();
        }

        public async Task<IEnumerable<Common.Model>> GetModels(int manufacturerId)
        {
            string path = $"api/Values/manufacturers/{manufacturerId}";
            var res = await GetData(path);
            return res.As<List<Common.Model>>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            string path = "api/Values/categories";
            var res = await GetData(path);
            return res.As<List<Category>>();
        }

        public async Task<IEnumerable<Item>> GetItems(int modelId, int categoryId)
        {
            string path = $"api/Values/categories/{categoryId}/{modelId}";
            var res = await GetData(path);
            return res.As<List<Item>>();
        }

        public async Task<bool> SendOrder()
        {
            if (IsLoggedIn)
            {
                string path = "api/Values/orders";
                string fullUrl = url + path;

                NewOrderPostModel newOrder = new NewOrderPostModel();
                newOrder.username = this.Username;
                newOrder.Cart = new Dictionary<int, int>();

                HttpClient client = new HttpClient(new HttpClientHandler());
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authKey);
                client.DefaultRequestHeaders.Add("username", newOrder.username);

                foreach (var item in Cart.Instance.Items)
                {
                    newOrder.Cart.Add(item.Key.Id, item.Value);
                }

                await client.PostAsync(fullUrl, new StringContent(JsonConvert.SerializeObject(newOrder.Cart), Encoding.UTF8, "application/json"));
                Toast.Show("Order sent!");
                Cart.Instance.Items.Clear();
                return true;
            }
            else
            {
                Toast.Show("You are not signed in!");
                return false;
            }
        }
    }
}