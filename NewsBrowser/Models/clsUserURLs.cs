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
    public class UserURLs
    {
        public Guid URLID { get; set; }
        public Guid UserID { get; set; }
        public bool IsLiked { get; set; }
        public int Rating { get; set; }
        public DateTime CreateDate { get; set; }
    }
}