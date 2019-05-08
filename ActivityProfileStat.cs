
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
using TennisStats.Enum;
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "ActivityProfileStat")]
    public class ActivityProfileStat : Activity
    {
        private TextView tvProfileName;
        private TextView tvClub;
        private Button btnOverAll;
        private Button btnLastYear;
        private Button btnLastMonth;
        private Button btnMatch;
        private FrameLayout frameLayoutStat; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ProfileStat);

            // Define widgets
            tvProfileName = FindViewById<TextView>(Resource.Id.tvProfilName);
            btnOverAll = FindViewById<Button>(Resource.Id.btOverall);
            btnLastYear = FindViewById<Button>(Resource.Id.btLastYear);
            btnLastMonth = FindViewById<Button>(Resource.Id.btLastMonth);
            btnMatch = FindViewById<Button>(Resource.Id.btMatch);

            
            Bundle bundle = new Bundle();
            bundle.PutString("playerId", "Jaafar92");

            btnOverAll.Click += delegate
            {
                bundle.PutInt("statType", (int) StatisticTypeEnum.StatisticType.OVERALL);
                FragmentManager.PopBackStack();
                NavigationService.AddFragment(FragmentManager, FindViewById<FrameLayout>(Resource.Id.FrameLayoutStat), FragmentUserStats.NewInstance(bundle));
            };

            btnLastMonth.Click += delegate
            {
                bundle.PutInt("statType", (int) StatisticTypeEnum.StatisticType.LASTMOUNTH);
                FragmentManager.PopBackStack();
                NavigationService.AddFragment(FragmentManager, FindViewById<FrameLayout>(Resource.Id.FrameLayoutStat), FragmentUserStats.NewInstance(bundle));
            };

            btnLastYear.Click += delegate
            {
                bundle.PutInt("statType", (int) StatisticTypeEnum.StatisticType.LASTYEAR);
                FragmentManager.PopBackStack();
                NavigationService.AddFragment(FragmentManager, FindViewById<FrameLayout>(Resource.Id.FrameLayoutStat), FragmentUserStats.NewInstance(bundle));
            };

        }
    }
}
