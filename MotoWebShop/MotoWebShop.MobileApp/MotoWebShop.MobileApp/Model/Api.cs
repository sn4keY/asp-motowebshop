using MotoWebShop.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

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

        private Response GetResult(string path, Dictionary<string, string> data)
        {
            try
            {
                string fullUrl = url + path;
                Console.WriteLine($"URL: {fullUrl}");

                string jsonData = JsonConvert.SerializeObject(data);
                var task = client.PostAsync(fullUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                task.Wait();
                var task2 = task.Result.Content.ReadAsStringAsync();
                task2.Wait();
                Console.WriteLine($"Result code: {task.Result.StatusCode}");
                Console.WriteLine($"Result:\n{task2.Result}");
                return new Response(task2.Result, task.Result.StatusCode, task.Result.IsSuccessStatusCode);
            }
            catch(AggregateException ex)
            {
                foreach(var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return new Response("", HttpStatusCode.ServiceUnavailable, false);
        }

        public delegate void LoginResult(bool success);
        public void Login(string username, string password, LoginResult loginResultHandler)
        {
            new Thread(() =>
            {
                string path = "Auth/Login";
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("username", username);
                data.Add("password", password);
                var res = GetResult(path, data);

                var resData = res.As<Dictionary<string, string>>();

                if (resData != null && resData.ContainsKey("token"))
                {
                    this.authKey = resData["token"];
                    this.authExpiration = DateTime.Parse(resData["expiration"]);
                    this.Username = username;
                    this.Password = password;
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => loginResultHandler?.Invoke(true));
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => loginResultHandler?.Invoke(false));
                }

                Console.WriteLine(res.Text);
            })
            .Start();
        }

        public delegate void RegisterResult(bool success);
        public void Register(string username, string password, RegisterResult registerResultHandler)
        {
            new Thread(() =>
            {
                string path = "Auth/Register";
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("email", username);
                data.Add("password", password);
                var res = GetResult(path, data);

                var resData = res.As<Dictionary<string, string>>();

                if (resData != null && resData.ContainsKey("username") && resData["username"] == username)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => registerResultHandler?.Invoke(true));
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => registerResultHandler?.Invoke(false));
                }

                Console.WriteLine(res.Text);
            })
            .Start();
        }

        public void Logout()
        {
            authKey = null;
        }

        public delegate void GetManufacturersResult(IEnumerable<Manufacturer> manufacturers);
        public void GetManufacturers(GetManufacturersResult getManufacturersResultHandler)
        {
            new Thread(() =>
            {
                string path = "Manufacturers";

                List<Manufacturer> manufacturers = new List<Manufacturer>();
                manufacturers.Add(new Manufacturer() { Id = 1, Name = "BMW", PictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/BMW.svg/150px-BMW.svg.png" });
                manufacturers.Add(new Manufacturer() { Id = 2, Name = "Suzuki", PictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Suzuki_Motor_Corporation_logo.svg/150px-Suzuki_Motor_Corporation_logo.svg.png" });

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => getManufacturersResultHandler?.Invoke(manufacturers));
            })
            .Start();
        }


        public delegate void GetModelsResult(IEnumerable<Common.Model> models);
        public void GetModels(int manufacturerId, GetModelsResult getModelsResultHandler)
        {
            new Thread(() =>
            {
                string path = "Categories";

                List<Common.Model> models = new List<Common.Model>();
                models.Add(new Common.Model() { Id = 1, ManufacturerId = 1, Name = "F650", PictureURL = "https://www.motorcyclespecs.co.za/Gallery/BMW%20F650%2094.jpg" });
                models.Add(new Common.Model() { Id = 2, ManufacturerId = 2, Name = "GS500", PictureURL = "http://www.motorrevu.hu/img/galery/galria-500-asok-500-alatt_motorrevu.jpg" });

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => getModelsResultHandler?.Invoke(models.Where(x => x.ManufacturerId == manufacturerId)));
            })
            .Start();
        }

        public delegate void GetCategoriesResult(IEnumerable<Category> categories);
        public void GetCategories(int manufacturerId, GetCategoriesResult getCategoriesResultHandler)
        {
            new Thread(() =>
            {
                string path = "Categories";

                List<Category> categories = new List<Category>();
                categories.Add(new Category() { Id = 1, Name = "Piston" });
                categories.Add(new Category() { Id = 2, Name = "Fueltank" });

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => getCategoriesResultHandler?.Invoke(categories));
            })
            .Start();
        }

        public delegate void GetItemsResult(IEnumerable<Item> items);
        public void GetItems(int modelId, int categoryId, GetItemsResult getItemsResultHandler)
        {
            new Thread(() =>
            {
                string path = "Items";

                List<Item> items = new List<Item>();
                items.Add(new Item() { Id = 1, CategoryId = 1, ModelId = 1, Name = "Baszott nagy piston", Description="leírásssdad ad ", Price = 1000 });
                items.Add(new Item() { Id = 2, CategoryId = 1, ModelId = 1, Name = "Baszott nagy könnyű piston", Description="leíráasdsadsadsa", Price=1500 });
                items.Add(new Item() { Id = 2, CategoryId = 2, ModelId = 2, Name = "Nagy tank", Description="asdsadsasadsadsa", Price=2000 });
                items.Add(new Item() { Id = 2, CategoryId = 2, ModelId = 2, Name = "Közepes tank", Description = "asdsadsa4d56s", Price=2500 });

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => getItemsResultHandler?.Invoke(items.Where(x => x.ModelId == modelId && x.CategoryId == categoryId)));
            })
            .Start();
        }
    }
}