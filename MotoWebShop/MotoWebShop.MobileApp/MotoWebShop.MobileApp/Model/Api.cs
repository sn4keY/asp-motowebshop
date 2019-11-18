using MotoWebShop.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public void GetManufacturers()
        {

        }

        public void GetCategories(int manufacturerId)
        {

        }

        public void GetItems(int manufacturerId, int categoryId)
        {

        }
    }
}