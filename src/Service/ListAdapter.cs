using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace TennisStats.Service
{
    public class ListAdapter : BaseAdapter
    {
        private ObservableCollection<FragmentLiveMathListItem> items;
        private Activity context;

        public ListAdapter(Activity context, ObservableCollection<FragmentLiveMathListItem> items)
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

            
            view.FindViewById<TextView>(Resource.Id.tvPlayer1).Text = item.getMatch().Team1Id;
            view.FindViewById<TextView>(Resource.Id.tvPlayer2).Text = item.getMatch().Team2Id;
            /*
            view.FindViewById<TextView>(Resource.Id.tvPlayer1).Text = "Hans";
            view.FindViewById<TextView>(Resource.Id.tvPlayer2).Text = "Erik";
*/
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

        public void setItems(ObservableCollection<FragmentLiveMathListItem> items)
        {
            this.items = items;
        }
        
    }
}