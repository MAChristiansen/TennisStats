
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TennisStats.Model;
using TennisStats.src.Controller;

namespace TennisStats
{
    [Activity(Label = "Result")]
    public class ActivityResultPage : Activity
    {
        private StatisticController _statisticController;
        private MatchController _matchController;

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

        private Button btnDone;
           


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ResultPage);

            _matchController = MatchController.Instance;
            _statisticController = new StatisticController();

            btnDone = FindViewById<Button>(Resource.Id.btnDone);
            tvScore = FindViewById<TextView>(Resource.Id.tvScore);
            tvTeam1 = FindViewById<TextView>(Resource.Id.tvTeam1);
            tvTeam2 = FindViewById<TextView>(Resource.Id.tvTeam2);
            tvT1FirstServePercent = FindViewById<TextView>(Resource.Id.tvT1FirstServePercent);
            tvT2FirstServePercent = FindViewById<TextView>(Resource.Id.tvT2FirstServePercent);
            tvT1WinPercentOnFirstServe = FindViewById<TextView>(Resource.Id.tvT1WinOnFirstServe);
            tvT2WinPercentOnFirstServe = FindViewById<TextView>(Resource.Id.tvT2WinOnFirstServe);
            tvT1WinPercentOnSecondServe = FindViewById<TextView>(Resource.Id.tvT1WinOnSecondServe);
            tvT2WinPercentOnSecondServe = FindViewById<TextView>(Resource.Id.tvT2WinOnSecondServe);
            tvT1Aces = FindViewById<TextView>(Resource.Id.tvT1Aces);
            tvT2Aces = FindViewById<TextView>(Resource.Id.tvT2Aces);
            tvT1DoubleFaults = FindViewById<TextView>(Resource.Id.tvT1DoubleFaults);
            tvT2DoubleFaults = FindViewById<TextView>(Resource.Id.tvT2DoubleFaults);
            tvT1Winners = FindViewById<TextView>(Resource.Id.tvT1Winners);
            tvT2Winners = FindViewById<TextView>(Resource.Id.tvT2Winners);
            tvT1UnforcedError = FindViewById<TextView>(Resource.Id.tvT1UnforcedError);
            tvT2UnforcedError = FindViewById<TextView>(Resource.Id.tvT2UnforcedError);
            tvT1ForcedError = FindViewById<TextView>(Resource.Id.tvT1ForcedError);
            tvT2ForcedError = FindViewById<TextView>(Resource.Id.tvT2ForcedError);
            tvT1TotalPointsWon = FindViewById<TextView>(Resource.Id.tvT1TotalPointsWon);
            tvT2TotalPointsWon = FindViewById<TextView>(Resource.Id.tvT2TotalPointsWon);

            Match currentMatch = _matchController.GetCurrentMatch();
            List<string> teamNames = _matchController.GetTeamNames(currentMatch);
            List<Point> points = _statisticController.GetPointsBasedOnMatch(currentMatch);

            tvScore.Text = _matchController.GetMatchScore(currentMatch);
            tvTeam1.Text = teamNames[0];
            tvTeam2.Text = teamNames[1];
            tvT1FirstServePercent.Text = _statisticController
                .calculateFirstServePercentage(currentMatch.Team1Id, points).ToString();
            tvT2FirstServePercent.Text = _statisticController
                .calculateFirstServePercentage(currentMatch.Team2Id, points).ToString();
            tvT1WinPercentOnFirstServe.Text = _statisticController
                .calculateWinPercentageOnFirstServe(currentMatch.Team1Id, points).ToString();
            tvT2WinPercentOnFirstServe.Text = _statisticController
                .calculateWinPercentageOnFirstServe(currentMatch.Team2Id, points).ToString();
            tvT1WinPercentOnSecondServe.Text = _statisticController
                .calculateWinPercentageOnSecondServe(currentMatch.Team1Id, points).ToString();
            tvT2WinPercentOnSecondServe.Text = _statisticController
                .calculateWinPercentageOnSecondServe(currentMatch.Team2Id, points).ToString();
            tvT1Aces.Text = _statisticController.calculateAmountOfAces(currentMatch.Team1Id, points).ToString();
            tvT2Aces.Text = _statisticController.calculateAmountOfAces(currentMatch.Team2Id, points).ToString();
            tvT1DoubleFaults.Text = _statisticController.calculateAmountOfDoubleFaults(currentMatch.Team1Id, points)
                .ToString();
            tvT2DoubleFaults.Text = _statisticController.calculateAmountOfDoubleFaults(currentMatch.Team2Id, points)
                .ToString();
            tvT1Winners.Text = _statisticController.calculateAmountOfWinners(currentMatch.Team1Id, points).ToString();
            tvT2Winners.Text = _statisticController.calculateAmountOfWinners(currentMatch.Team2Id, points).ToString();
            tvT1UnforcedError.Text = _statisticController.calculateAmountOfUnforcedErrors(currentMatch.Team1Id, points)
                .ToString();
            tvT2UnforcedError.Text = _statisticController.calculateAmountOfUnforcedErrors(currentMatch.Team2Id, points)
                .ToString();
            tvT1ForcedError.Text = _statisticController.calculateAmountOfForcedErrors(currentMatch.Team1Id, points)
                .ToString();
            tvT2ForcedError.Text = _statisticController.calculateAmountOfForcedErrors(currentMatch.Team2Id, points)
                .ToString();

            tvT1TotalPointsWon.Text =
                _statisticController.calculateTotalPointsWon(currentMatch.Team1Id, points).ToString();
            tvT2TotalPointsWon.Text =
                _statisticController.calculateTotalPointsWon(currentMatch.Team2Id, points).ToString();

            btnDone.Click += delegate
            {
                FinishAffinity();
                StartActivity(typeof(MainActivity));
            };


        }
    }
}
