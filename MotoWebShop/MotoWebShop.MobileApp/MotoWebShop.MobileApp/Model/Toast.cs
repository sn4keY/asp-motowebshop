using System;
using System.Collections.Generic;
using System.Text;

namespace MotoWebShop.MobileApp
{
    public static class Toast
    {
        public enum Time { Short, Long }
        public static IToast Handler = null;

        public static void Show(string text, Time time = Time.Short)
        {
            Handler?.Show(text, time);
        }
    }

    public interface IToast
    {
        void Show(string text, Toast.Time time);
    }
}
