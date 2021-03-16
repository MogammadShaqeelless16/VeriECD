using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VeriECD.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public static Color BackgroundColor = Color.WhiteSmoke;
        public static Color MainTextColor = Color.Black;
        public static int LoginIconHeight = 200;

        // Login


        public static string LoginUrl = "https://test.com/api/Auth/Login";
        public static string NoInternetText = "No Internet Connection";
    }
}
