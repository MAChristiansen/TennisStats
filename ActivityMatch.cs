
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using TennisStats.Model;
using TennisStats.Service;
using TennisStats.src.Controller;
using TennisStats.src.Service;

namespace TennisStats
{
    [Activity(Label = "Match")]


    public class ActivityMatch : Activity, IObserver<Match>
    {
        private TextView tvTeam1Names;
        private TextView tvTeam2Names;

        private TextView tvTeam1Sets;
        private TextView tvTeam2Sets;

        private TextView tvTeam1Games;
        private TextView tvTeam2Games;

        private TextView tvTeam1Points;
        private TextView tvTeam2Points;

        private ImageView ivTeam1Serving;
        private ImageView ivTeam2Serving;

        private static Button btnStats;

        private MatchController matchController;
        private PointService pointService;

        private Unsubscriber<Match> unsubscriber;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Get the match controller and subscribe to info
            matchController = MatchController.Instance;

            // Create your application here
            SetContentView(Resource.Layout.Match);

            // Define interface elements
            tvTeam1Names = FindViewById<TextView>(Resource.Id.tvTeam1Names);
            tvTeam2Names = FindViewById<TextView>(Resource.Id.tvTeam2Names);

            tvTeam1Sets = FindViewById<TextView>(Resource.Id.tvTeam1Sets);
            tvTeam2Sets = FindViewById<TextView>(Resource.Id.tvTeam2Sets);

            tvTeam1Games = FindViewById<TextView>(Resource.Id.tvTeam1Games);
            tvTeam2Games = FindViewById<TextView>(Resource.Id.tvTeam2Games);

            tvTeam1Points = FindViewById<TextView>(Resource.Id.tvTeam1Points);
            tvTeam2Points = FindViewById<TextView>(Resource.Id.tvTeam2Points);

            ivTeam1Serving = FindViewById<ImageView>(Resource.Id.ivTeam1Serving);
            ivTeam2Serving = FindViewById<ImageView>(Resource.Id.ivTeam2Serving);

            btnStats = FindViewById<Button>(Resource.Id.btnStats);
            
            NavigationService.AddFragment(FragmentManager, FindViewById<FrameLayout>(Resource.Id.fragmentContainer), FragmentServeScenario.NewInstance(1));
            
            btnStats.Click += delegate
            {
                MatchController.Match = matchController.GetCurrentMatch();
                NavigationService.NavigateToFragment(FragmentManager, FindViewById<FrameLayout>(Resource.Id.fragmentContainer), FragmentMatchStats.NewInstance(null));
            };

            // set the team names
            tvTeam1Names.Text = matchController.GetTeamNames(matchController.GetCurrentMatch())[0];
            tvTeam2Names.Text = matchController.GetTeamNames(matchController.GetCurrentMatch())[1];
            setServerUI();


            //Creating first serve scenario
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            unsubscriber = (Unsubscriber<Match>)matchController.Subscribe(this);
        }


        /*
         *   Implementation of the observerpattern!
         * 
         *   - OnNext is called when information has been updated
         *   - OnCompleted is called when the game is finished
         * 
         */
        public void OnCompleted()
        {
            NavigationService.NavigateToPage(this, typeof(ActivityResultPage));
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Match value)
        {
            matchController = MatchController.Instance;
            pointService = PointService.Instance;
            //Update the score of the match
            tvTeam1Sets.Text =  matchController.GetCurrentMatchScore()[0] + "";
            tvTeam2Sets.Text = matchController.GetCurrentMatchScore()[1] + "";

            //Update the score of the set
            tvTeam1Games.Text = matchController.GetCurrentSetScore()[0] + "";
            tvTeam2Games.Text = matchController.GetCurrentSetScore()[1] + "";

            //Update the score of the game
            tvTeam1Points.Text = pointService.convertPoints(matchController.GetCurrentGameScore()[0], matchController.GetCurrentGameScore()[1], matchController.getCurrentGameType());
            tvTeam2Points.Text = pointService.convertPoints(matchController.GetCurrentGameScore()[1], matchController.GetCurrentGameScore()[0], matchController.getCurrentGameType());

            setServerUI();
        }


        private void setServerUI()
        {
            if (matchController.getCurrentGame().Servers[matchController.getCurrentGame().Servers.Count - 1].Equals(matchController.GetTeamNames(matchController.GetCurrentMatch())[0])){
                //team 1 is serving
                ivTeam1Serving.Visibility = Android.Views.ViewStates.Visible;
                ivTeam2Serving.Visibility = Android.Views.ViewStates.Invisible;
            }
            else
            {
                //team 2 is serving
                ivTeam1Serving.Visibility = Android.Views.ViewStates.Invisible;
                ivTeam2Serving.Visibility = Android.Views.ViewStates.Visible;
            }
        }
    }
}
