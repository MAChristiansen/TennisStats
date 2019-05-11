
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
using TennisStats.src.Controller;

namespace TennisStats
{
    [Activity(Label = "ActivitySpecificUserMatchStats")]
    public class ActivitySpecificUserMatchStats : Activity
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
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SpecificUserMatchStats);
            
            
        }
    }
}
