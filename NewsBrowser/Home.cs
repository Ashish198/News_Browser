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
using Android.Webkit;

using RestSharp;
using NewsBrowser.Adapters;
using NewsBrowser.Models;

using System.Threading.Tasks;

[assembly: Permission(Name = Android.Manifest.Permission.Internet)]

namespace NewsBrowser
{
    [Activity(Label = "@string/app_name")]
    public class Home : Activity,View.IOnTouchListener//,GestureDetector.IOnGestureListener
    {
        List<CategoryProperty> lCategoryCollection;
        ListView lstView;
        //WebView[] webview = new WebView[10];
        //WebView[] webview;
        private float _viewX;
        private float _viewY;
        private int left, top, right, bottom;
        private ProgressBar progress;
        private Guid _UserID = new Guid("7a0b5d8d-68d2-46f6-86cc-21279862fcad");
        private GestureDetector _gestureDetector;


        //public override bool DispatchTouchEvent(MotionEvent e){
        //    base.DispatchTouchEvent(e);
        //    return _gestureDetector.OnTouchEvent(e);
        //}
        //public override bool OnTouchEvent(MotionEvent e)
        //{
        //    _gestureDetector.OnTouchEvent(e);
        //    return false;
        //}

        //public bool OnDown(MotionEvent e)
        //{
        //    return false;
        //}

        //public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        //{
        //    //_textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
        //    return true;
        //}

        //public void OnLongPress(MotionEvent e) { }

        //public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        //{
        //    return false;
        //}

        //public void OnShowPress(MotionEvent e) { }

        //public bool OnSingleTapUp(MotionEvent e)
        //{
        //    return false;
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Home);
            //_gestureDetector = new GestureDetector(this);

            lstView = (ListView)FindViewById(Resource.Id.lstCategory);

            //lstView = FindViewById<ListView>(Resource.Id.lstCategory);

            getCategory();

            lstView.ItemClick += lstView_ItemClick;
        }

        void lstView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {  
            CategoryProperty lProperty = lCategoryCollection[e.Position];

            getURLs(lProperty.CategoryID);
        }

        void getCategory()
        {
            lstView = (ListView)FindViewById(Resource.Id.lstCategory);

            RestClient client = new RestClient(App_Common.getServiceBaseURL);
            RestRequest request = new RestRequest("api/category/getlist", Method.GET);

            var resp = client.Execute<List<CategoryProperty>>(request);
            lCategoryCollection = resp.Data;

            if (lCategoryCollection != null)
                lstView.Adapter = new CategoryAdapter(this, lCategoryCollection);

            //var asyncHandle2 = client.ExecuteAsync<List<CategoryProperty>>(request, (response) =>
            //{
            //    lCategoryCollection = response.Data;
            //    //resp.TrySetResult(response.Data);
            //    lstView.Adapter = new CategoryAdapter(this, lCategoryCollection);

            //    //((CategoryAdapter)lstView.Adapter).NotifyDataSetChanged();

            //    //this.LayoutInflater.Inflate(Resource.Id.lstCategory, null);
            //    //this.RunOnUiThread(new Action(() =>
            //    //{
            //    //    base.NotifyDataSetChanged();
            //    //})); 

            //});
        }

        private void getURLs(Guid cid)
        {
            RestClient client = new RestClient(App_Common.getServiceBaseURL);
            RestRequest request = new RestRequest("api/url/geturls", Method.GET);
            request.AddParameter("uid", _UserID);
            request.AddParameter("cid", cid);

            var resp = client.Execute<List<URLProperty>>(request);
            List<URLProperty> lURLCollection = resp.Data;

            //WebView[] webview = new WebView[lURLCollection.Count];
            WebView[] webview = new WebView[lURLCollection.Count];
            //RelativeLayout[] layout = new RelativeLayout[lURLCollection.Count];
            int lCounter = 0;
            int height = 100;
            //ViewGroup lGroup = 

            //LinearLayout linear_layout = new LinearLayout(this);
            RelativeLayout layout = new RelativeLayout(this);

            //layout.AddView(lstView);
            //FrameLayout layout = new FrameLayout(this);
            //ProgressDialog lDialog = new ProgressDialog(this);
            //lProgressBar = new ProgressBar(this);

            foreach (URLProperty lProperty in lURLCollection)
            {
                webview[lCounter] = new WebView(this);
                //webview[lCounter]. = lProperty.URLID.ToString();
                //Console.WriteLine(webview[lCounter].Visibility.ToString());
                //webview[lCounter].SetBackgroundColor(Android.Graphics.Color.White);
                //webview[lCounter].setid
                webview[lCounter].Layout(50, 50, 300, 390);
                webview[lCounter].Tag = lProperty.URLID.ToString();
                webview[lCounter].LoadUrl(lProperty.URL);
                webview[lCounter].Settings.JavaScriptEnabled = true;
                webview[lCounter].SetWebViewClient(new MyWebViewClient());
                webview[lCounter].SetOnTouchListener(this);
                


                layout.AddView(webview[lCounter]);
                
                lCounter = lCounter + 1;
            }

            //lDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            //layout.AddView(lDialog);
            //linear_layout.AddView(layout);

            SetContentView(layout);

            //lCounter = 0;
            //foreach (URLProperty lProperty in lURLCollection)
            //{
            //    webview[lCounter].LoadUrl(lProperty.URL);
            //    webview[lCounter].SetWebViewClient(new MyWebViewClient());
            //    webview[lCounter].SetOnTouchListener(this);

            //    lCounter = lCounter + 1;
            //}

        }

        public bool OnTouch(View v, MotionEvent e)
        {
            int old_top = v.Top;
            int old_bottom = v.Bottom;
            int old_left = v.Left;
            int old_right = v.Right;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _viewX = e.GetX();
                    _viewY = e.GetY();
                    break;
                case MotionEventActions.Move:
                    left = (int)(e.RawX - _viewX);
                    right = (left + v.Width);
                    top = (int)(e.RawY - _viewY);
                    bottom = (int)(top + v.Height);
                    v.Layout(left, top, right, bottom);
                    
                    WebView view = (WebView)v;

                    //if (left < 100)
                    //{
                    //    var matrics = Resources.DisplayMetrics;
                    //    var width = ConvertPixelsToDp(matrics.WidthPixels);
                    //    var height = ConvertPixelsToDp(matrics.HeightPixels);

                    //    SaveClientPreference(_UserID, new Guid(v.Tag.ToString()), false, 0);
                    //}

                    //if (right > 600)
                    //{
                    //    SaveClientPreference(_UserID, new Guid(v.Tag.ToString()), true, 5);
                    //}

                    //Toast.MakeText(this, view.Url, ToastLength.Short);

                    
                    break;
                case MotionEventActions.Up:
                    if (left > 280 || right < 40)
                        v.Layout(0, 0, 0, 0);
                    else
                        v.Layout(19, 20, 301, 390);
                    break;
            }

            return true;
        }


        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            //_textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
            return true;
        }


        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

        private void SaveClientPreference(Guid uid, Guid urlid, bool liked, float rating)
        {
            RestClient client = new RestClient(App_Common.getServiceBaseURL);
            RestRequest request = new RestRequest("api/url/adduserurl", Method.POST);
            request.Method = Method.POST;

            request.AddParameter("UserID", uid);
            request.AddParameter("URLID", urlid);
            request.AddParameter("IsLiked", liked);
            request.AddParameter("Rating", rating);

            client.PostAsync(request, null);
        }
    }

    public class MyWebViewClient : WebViewClient//,GestureDetector.IOnGestureListener
    {
        private ProgressBar progress;

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            //Console.WriteLine("Loading Url...");
            return true;
        }

        public override void OnPageFinished(WebView view, String url)
        {
            //Console.WriteLine("Finished loading..");
            //Toast.MakeText(this, "Page loaded", ToastLength.Short).Show();
            //Toast.MakeText(Application.Context, "Page loaded", ToastLength.Long).Show();

        }

        public override void OnPageStarted(WebView view, String url, Android.Graphics.Bitmap favicon)
        {
            //progress.Visibility(View.visib)
            Console.WriteLine("Loading page...");
            //progress.StartAnimation( new Android.Views.Animations.Animation(){});
            // progress.setVisibility(View.vis);
            //WebViewActivity.this.progress.setProgress(0);
            //super.onPageStarted(view, url, favicon);
            // progress.setVisibility(View.VISIBLE);
            //WebViewActivity.this.progress.setProgress(0);
            //super.onPageStarted(view, url, favicon);
        }


        //public override bool OnTouchEvent(MotionEvent e)
        //{
        //    _gestureDetector.OnTouchEvent(e);
        //    return false;
        //}

        //public bool OnDown(MotionEvent e)
        //{
        //    return false;
        //}

        //public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        //{
        //    //_textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
        //    return true;
        //}

        //public void OnLongPress(MotionEvent e) { }

        //public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        //{
        //    return false;
        //}

        //public void OnShowPress(MotionEvent e) { }

        //public bool OnSingleTapUp(MotionEvent e)
        //{
        //    return false;
        //}
    }

    public class MyWebView : WebView, GestureDetector.IOnGestureListener
    {
        private Context _context;
        private GestureDetector _gestureDetector;
        //private GestureDetector.IOnGestureListener listner;

        GestureDetector.SimpleOnGestureListener sogl = new GestureDetector.SimpleOnGestureListener();
 //{
 //           public boolean onDown(MotionEvent event) {
 //               return true;
 //           }

        public MyWebView(Context context)
            : base(context)
        {

            _context = context;
            _gestureDetector = new GestureDetector(context, sogl);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            base.DispatchTouchEvent(e);
            return _gestureDetector.OnTouchEvent(e);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            //base.OnTouchEvent(e);
                    //gestureDetector.onTouchEvent(e) || super.onTouchEvent(e));
            return _gestureDetector.OnTouchEvent(e);
            //return false;
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            //_textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
            return true;
        }

        public void OnLongPress(MotionEvent e) { }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return false;
        }

        public void OnShowPress(MotionEvent e) { }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }
    }
}