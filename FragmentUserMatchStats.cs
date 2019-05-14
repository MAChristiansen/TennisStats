
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using TennisStats.adapter;
using TennisStats.Model;
using TennisStats.src.Controller;

namespace TennisStats
{
    public class FragmentUserMatchStats : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        
        public static FragmentUserMatchStats NewInstance(Bundle bundle)
        {
            FragmentUserMatchStats fragmentUserMatchStats = new FragmentUserMatchStats();
            fragmentUserMatchStats.Arguments = bundle;

            return fragmentUserMatchStats;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.UserMatchStats, container, false);

        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            FrameLayout fragmentContainer = Activity.FindViewById<FrameLayout>(Resource.Id.FrameLayoutStat);

            StatisticController statisticController = new StatisticController();

            List<Match> matchList = await statisticController.GetMatches(Util.GetStringFromPreference(Activity, Constants.UserId ));
            
            ListView listView = view.FindViewById<ListView>(Resource.Id.lvMatchStats);
            
            MatchStatisticsAdapter matchStatisticsAdapter = new MatchStatisticsAdapter(
                Activity, 
                Resource.Layout.UserMatchStatsLayout, 
                matchList, 
                listView,
                FragmentManager,
                fragmentContainer);
            
            listView.Adapter = matchStatisticsAdapter;
        }
    }
}
