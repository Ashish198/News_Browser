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

using NewsBrowser.Models;

namespace NewsBrowser.Adapters
{
    public class CategoryAdapter : BaseAdapter<CategoryProperty>
    {
        private List<CategoryProperty> _Collection;
        private Activity _Activity;


        public CategoryAdapter(Activity context, List<CategoryProperty> collection)
        {
            this._Collection = collection;
            this._Activity = context;
        }


        public override CategoryProperty this[int position]
        {
            get { return this._Collection[position]; }
        }

        public override int Count
        {
            get { return this._Collection.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this._Collection[position];
            View view = convertView;
            //If there is nothing to reuse, then create view from your row layout
            if (view == null)
                view = this._Activity.LayoutInflater.Inflate(Resource.Layout.Home, null);

            view.FindViewById<TextView>(Resource.Id.textView1).Text = item.CategoryName;

            return view;
        }
    }
}