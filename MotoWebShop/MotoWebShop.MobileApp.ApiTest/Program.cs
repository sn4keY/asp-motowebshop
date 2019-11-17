using MotoWebShop.MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoWebShop.MobileApp.ApiTest
{
    class Program
    {
        static Api api = Api.Instance;

        static void Enter(string next)
        {
            Console.WriteLine("Press enter to go: " + next);
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            string email = "gabor@mail.hu";
            string pw = "asdasd123";

            Console.WriteLine("\nEND");
            while (true) Console.ReadLine();
        }
    }
}
