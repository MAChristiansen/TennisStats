
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using TennisStats.Model;
using TennisStats.Service;
using TennisStats.src.Controller;
using TennisStats.src.Service;

namespace TennisStats
{
    [Activity(Label = "Actual Match")]


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

            // set the team names
            tvTeam1Names.Text = matchController.GetTeamNames()[0];
            tvTeam2Names.Text = matchController.GetTeamNames()[1];



            //Creating first serve scenario
            NavigationService.AddFragment(FragmentManager, FindViewById<FrameLayout>(Resource.Id.fragmentContainer), FragmentServeScenario.NewInstance(1));
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
            //TODO når der er fundet en vinder skal der vises en statestik side.
            Console.WriteLine("The match is done!!");
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
            tvTeam1Points.Text = pointService.convertPoints(matchController.GetCurrentGameScore()[0], matchController.GetCurrentGameScore()[1]);
            tvTeam2Points.Text = pointService.convertPoints(matchController.GetCurrentGameScore()[1], matchController.GetCurrentGameScore()[0]);

        }
    }
}
