using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace TennisStats.Service
{
    public class ListAdapter : BaseAdapter
    {
        private List<FragmentLiveMathListItem> items;
        private Activity context;

        public ListAdapter(Activity context, List<FragmentLiveMathListItem> items)
        {
            this.context = context;
            this.items = items;
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var view = convertView;
            
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.LiveMatchListItem, null);
            }

            view.FindViewById<TextView>(Resource.Id.tvPlayer1).Text = item.getMatchNames()[0];
            view.FindViewById<TextView>(Resource.Id.tvPlayer2).Text = item.getMatchNames()[1];

            return view;
        }
        
        public override int Count => items.Count;
        
        public override long GetItemId(int position)
        {
            return position;
        }

        public override Object GetItem(int position)
        {
            throw new System.NotImplementedException();
        }
        
    }
}