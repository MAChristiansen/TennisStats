
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
using TennisStats.Service;

namespace TennisStats
{
    public class FragmentWhoWon : Fragment
    {
        private Button btnTeam1;
        private Button btnTeam2;
        
        public static FragmentWhoWon NewInstance()
        {
            Bundle bundle = new Bundle();

            return new FragmentWhoWon();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.WhoWin, container, false);

            btnTeam1 = view.FindViewById<Button>(Resource.Id.btnTeam1);
            btnTeam2 = view.FindViewById<Button>(Resource.Id.btnTeam2);

            btnTeam1.Click += delegate
            {
                AddWinnerTeamToPoint(1);
                Navigate();
            };

            btnTeam2.Click += delegate
            {
                AddWinnerTeamToPoint(2);
                Navigate();
            };

            return view;
        }

        private void AddWinnerTeamToPoint(int winnerTeam)
        {
            if (winnerTeam == 1)
            {
                //Add winning team to the POINT... /MAC
            }
            else if (winnerTeam == 2)
            {
                //Add winning team to the POINT... /MAC
            }
        }

        private void Navigate()
        {
            NavigationService.NavigateToFragment(
                FragmentManager, 
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentScore.NewInstance());
        }
    }
}
