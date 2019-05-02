
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
using TennisStats.Model;
using TennisStats.src.Controller;

namespace TennisStats
{
    public class FragmentMatchStats : Fragment
    {
        private MatchController _matchController;
        private StatisticController _statisticController;
        
        private TextView
            tvScore,
            tvTeam1,
            tvTeam2,
            tvT1FirstServePercent,
            tvT2FirstServePercent,
            tvT1WinPercentOnFirstServe,
            tvT2WinPercentOnFirstServe,
            tvT1WinPercentOnSecondServe,
            tvT2WinPercentOnSecondServe,
            tvT1Aces,
            tvT2Aces,
            tvT1DoubleFaults,
            tvT2DoubleFaults,
            tvT1Winners,
            tvT2Winners,
            tvT1UnforcedError,
            tvT2UnforcedError,
            tvT1ForcedError,
            tvT2ForcedError,
            tvT1TotalPointsWon,
            tvT2TotalPointsWon;

        private Button btnStats;
            
        public static FragmentMatchStats NewInstance(Bundle bundle)
        {
            FragmentMatchStats fragmentMatchStats = new FragmentMatchStats();
            fragmentMatchStats.Arguments = bundle;

            return fragmentMatchStats;
        }
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            btnStats = Activity.FindViewById<Button>(Resource.Id.btnStats);
            btnStats.Visibility = ViewStates.Invisible;
            
            View view = inflater.Inflate(Resource.Layout.MatchStats, container, false);
            
            _matchController = MatchController.Instance;
            _statisticController = new StatisticController();
            

            tvScore = view.FindViewById<TextView>(Resource.Id.tvScore);
            tvTeam1 = view.FindViewById<TextView>(Resource.Id.tvTeam1);
            tvTeam2 = view.FindViewById<TextView>(Resource.Id.tvTeam2);
            tvT1FirstServePercent = view.FindViewById<TextView>(Resource.Id.tvT1FirstServePercent);
            tvT2FirstServePercent = view.FindViewById<TextView>(Resource.Id.tvT2FirstServePercent);
            tvT1WinPercentOnFirstServe = view.FindViewById<TextView>(Resource.Id.tvT1WinOnFirstServe);
            tvT2WinPercentOnFirstServe = view.FindViewById<TextView>(Resource.Id.tvT2WinOnFirstServe);
            tvT1WinPercentOnSecondServe = view.FindViewById<TextView>(Resource.Id.tvT1WinOnSecondServe);
            tvT2WinPercentOnSecondServe = view.FindViewById<TextView>(Resource.Id.tvT2WinOnSecondServe);
            tvT1Aces = view.FindViewById<TextView>(Resource.Id.tvT1Aces);
            tvT2Aces = view.FindViewById<TextView>(Resource.Id.tvT2Aces);
            tvT1DoubleFaults = view.FindViewById<TextView>(Resource.Id.tvT1DoubleFaults);
            tvT2DoubleFaults = view.FindViewById<TextView>(Resource.Id.tvT2DoubleFaults);
            tvT1Winners = view.FindViewById<TextView>(Resource.Id.tvT1Winners);
            tvT2Winners = view.FindViewById<TextView>(Resource.Id.tvT2Winners);
            tvT1UnforcedError = view.FindViewById<TextView>(Resource.Id.tvT1UnforcedError);
            tvT2UnforcedError = view.FindViewById<TextView>(Resource.Id.tvT2UnforcedError);
            tvT1ForcedError = view.FindViewById<TextView>(Resource.Id.tvT1ForcedError);
            tvT2ForcedError = view.FindViewById<TextView>(Resource.Id.tvT2ForcedError);
            tvT1TotalPointsWon = view.FindViewById<TextView>(Resource.Id.tvT1TotalPointsWon);
            tvT2TotalPointsWon = view.FindViewById<TextView>(Resource.Id.tvT2TotalPointsWon);

            Match currentMatch = _matchController.GetCurrentMatch();
            List<string> teamNames = _matchController.GetTeamNames();
            List<Point> points = _statisticController.GetPointsBasedOnMatch(currentMatch);

            tvScore.Text = _matchController.GetMatchScore(currentMatch);
            tvTeam1.Text = teamNames[0];
            tvTeam2.Text = teamNames[1];
            tvT1FirstServePercent.Text = _statisticController.calculateFirstServePercentage(currentMatch.Team1Id, points).ToString();
            tvT2FirstServePercent.Text = _statisticController.calculateFirstServePercentage(currentMatch.Team2Id, points).ToString();
            tvT1WinPercentOnFirstServe.Text = _statisticController.calculateWinPercentageOnFirstServe(currentMatch.Team1Id, points).ToString();
            tvT2WinPercentOnFirstServe.Text = _statisticController.calculateWinPercentageOnFirstServe(currentMatch.Team2Id, points).ToString();
            tvT1WinPercentOnSecondServe.Text = _statisticController.calculateWinPercentageOnSecondServe(currentMatch.Team1Id, points).ToString();
            tvT2WinPercentOnSecondServe.Text = _statisticController.calculateWinPercentageOnSecondServe(currentMatch.Team2Id, points).ToString();
            tvT1Aces.Text = _statisticController.calculateAmountOfAces(currentMatch.Team1Id, points).ToString();
            tvT2Aces.Text = _statisticController.calculateAmountOfAces(currentMatch.Team2Id, points).ToString();
            tvT1DoubleFaults.Text = _statisticController.calculateAmountOfDoubleFaults(currentMatch.Team1Id, points).ToString();
            tvT2DoubleFaults.Text = _statisticController.calculateAmountOfDoubleFaults(currentMatch.Team2Id, points).ToString();
            tvT1Winners.Text = _statisticController.calculateAmountOfWinners(currentMatch.Team1Id, points).ToString();
            tvT2Winners.Text = _statisticController.calculateAmountOfWinners(currentMatch.Team2Id, points).ToString();
            tvT1UnforcedError.Text = _statisticController.calculateAmountOfUnforcedErrors(currentMatch.Team1Id, points).ToString();
            tvT2UnforcedError.Text = _statisticController.calculateAmountOfUnforcedErrors(currentMatch.Team2Id, points).ToString();
            tvT1ForcedError.Text = _statisticController.calculateAmountOfForcedErrors(currentMatch.Team1Id, points).ToString();
            tvT2ForcedError.Text = _statisticController.calculateAmountOfForcedErrors(currentMatch.Team2Id, points).ToString();
            
            

            tvT1TotalPointsWon.Text = _statisticController.calculateTotalPointsWon(currentMatch.Team1Id, points).ToString();
            tvT2TotalPointsWon.Text = _statisticController.calculateTotalPointsWon(currentMatch.Team2Id, points).ToString();
            
            return view;
        }

        public override void OnDestroy()
        {
            btnStats.Visibility = ViewStates.Visible;
            base.OnDestroy();
            
        }
    }
}
