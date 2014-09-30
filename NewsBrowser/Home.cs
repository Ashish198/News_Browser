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

using RestSharp;
using NewsBrowser.Adapters;
using NewsBrowser.Models;

using System.Threading.Tasks;

[assembly: Permission(Name = Android.Manifest.Permission.Internet)]

namespace NewsBrowser
{
    [Activity(Label = "@string/app_name")]
    public class Home : Activity
    {
        ListView lstView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Home);

            lstView = (ListView)FindViewById(Resource.Id.lstCategory);

            getCategory();
        }

        void getCategory()
        {
            List<CategoryProperty> lCategoryCollection = new List<CategoryProperty>();

            ListView lstView = (ListView)FindViewById(Resource.Id.lstCategory);

            RestClient client = new RestClient("http://newalliancemedia.net/revenueservice/");
            RestRequest request = new RestRequest("api/category/getlist", Method.GET);

            var resp = client.Execute<List<CategoryProperty>>(request);
            lCategoryCollection = resp.Data;
            lstView.Adapter = new CategoryAdapter(this, lCategoryCollection);
            //var resp = new TaskCompletionSource<List<CategoryProperty>>();

            //var asyncHandle2 = client.ExecuteAsync<List<CategoryProperty>>(request, (response) =>
            //{
            //    lCategoryCollection = response.Data;
            //    //resp.TrySetResult(response.Data);
            //    lstView.Adapter = new CategoryAdapter(this, lCategoryCollection);

            //    //((CategoryAdapter)lstView.Adapter).NotifyDataSetChanged();

            //    this.LayoutInflater.Inflate(Resource.Id.lstCategory, null);
            //    //this.RunOnUiThread(new Action(() =>
            //    //{
            //    //    base.NotifyDataSetChanged();
            //    //})); 

            //});
        }
    }
}