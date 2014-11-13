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

namespace NewsBrowser.Models
{
    public class URLProperty
    {
        public Guid URLID { get; set; }
        public Guid CategoryID { get; set; }
        public string URL { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}