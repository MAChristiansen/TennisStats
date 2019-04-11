
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
using TennisStats.src.Controller;

namespace TennisStats
{
    public class FragmentWhoWon : Fragment
    {
        private Button btnTeam1;
        private Button btnTeam2;

        private MatchController matchController;
        
        public static FragmentWhoWon NewInstance()
        {
            Bundle bundle = new Bundle();

            return new FragmentWhoWon();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            matchController = MatchController.Instance;
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.WhoWin, container, false);

            btnTeam1 = view.FindViewById<Button>(Resource.Id.btnTeam1);
            btnTeam2 = view.FindViewById<Button>(Resource.Id.btnTeam2);

             //Set the text of the buttons
            btnTeam1.Text = matchController.GetTeamNames()[0];
            btnTeam2.Text = matchController.GetTeamNames()[1];

            btnTeam1.Click += delegate
            {
                MatchController.inPlayPB.winnderId(matchController.GetTeamNames()[0]);
                Navigate();
            };

            btnTeam2.Click += delegate
            {
                MatchController.inPlayPB.winnderId(matchController.GetTeamNames()[1]);

                Navigate();
            };

            return view;
        }

        private void Navigate()
        {
            NavigationService.NavigateToFragment(
                FragmentManager, 
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentHowWin.NewInstance());
        }
    }
}
