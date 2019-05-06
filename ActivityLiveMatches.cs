
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
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "ActivityLiveMatches")]
    public class ActivityLiveMatches : Activity
    {
        private ListView lvLiveMatches;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TODO
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LiveMatches); 
            
            lvLiveMatches = FindViewById<ListView>(Resource.Id.lvLiveMatches);

            var mItems = new List<FragmentLiveMathListItem>();
            mItems.Add(new FragmentLiveMathListItem());
            
            
            lvLiveMatches.Adapter = new ListAdapter(this, mItems);
           
        }
    }
}
