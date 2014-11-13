using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NewsBrowser
{
    public class App_Common
    {
        public static string getServiceBaseURL
        {
            get { return "http://newalliancemedia.net/revenueservice/"; }
        }
    }
}