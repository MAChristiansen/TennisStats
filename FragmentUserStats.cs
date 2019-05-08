
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TennisStats.Enum;
using TennisStats.Model;
using TennisStats.src.Controller;

namespace TennisStats
{
    public class FragmentUserStats : Fragment
    {
        private StatisticController _statisticController;

        private TextView
            tvT1FirstServePercent,
            tvT1WinPercentOnFirstServe,
            tvT1WinPercentOnSecondServe,
            tvT1Aces,
            tvT1DoubleFaults,
            tvT1Winners,
            tvT1UnforcedError,
            tvT1ForcedError,
            tvT1TotalPointsWon;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        
        public static FragmentUserStats NewInstance(Bundle bundle)
        {
            FragmentUserStats fragmentUserStats = new FragmentUserStats();
            fragmentUserStats.Arguments = bundle;

            return fragmentUserStats;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.UserStats, container, false);
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
            _statisticController = new StatisticController();

            string playerId = Arguments.GetString("playerId");
            StatisticTypeEnum.StatisticType type = (StatisticTypeEnum.StatisticType) Arguments.GetInt("statType");
            
            tvT1FirstServePercent = view.FindViewById<TextView>(Resource.Id.tvT1FirstServePercent);
            tvT1WinPercentOnFirstServe = view.FindViewById<TextView>(Resource.Id.tvT1WinOnFirstServe);
            tvT1WinPercentOnSecondServe = view.FindViewById<TextView>(Resource.Id.tvT1WinOnSecondServe);
            tvT1Aces = view.FindViewById<TextView>(Resource.Id.tvT1Aces);
            tvT1DoubleFaults = view.FindViewById<TextView>(Resource.Id.tvT1DoubleFaults);
            tvT1Winners = view.FindViewById<TextView>(Resource.Id.tvT1Winners);
            tvT1UnforcedError = view.FindViewById<TextView>(Resource.Id.tvT1UnforcedError);
            tvT1ForcedError = view.FindViewById<TextView>(Resource.Id.tvT1ForcedError);
            tvT1TotalPointsWon = view.FindViewById<TextView>(Resource.Id.tvT1TotalPointsWon);
            
            List<Point> points = await _statisticController.GetListOfPointsToBeCalculatedAsync(playerId, type);

            tvT1FirstServePercent.Text = _statisticController.calculateFirstServePercentage(playerId, points).ToString();
            tvT1WinPercentOnFirstServe.Text = _statisticController.calculateWinPercentageOnFirstServe(playerId, points).ToString();
            tvT1WinPercentOnSecondServe.Text = _statisticController.calculateWinPercentageOnSecondServe(playerId, points).ToString();
            tvT1Aces.Text = _statisticController.calculateAmountOfAces(playerId, points).ToString();
            tvT1DoubleFaults.Text = _statisticController.calculateAmountOfDoubleFaults(playerId, points).ToString();
            tvT1Winners.Text = _statisticController.calculateAmountOfWinners(playerId, points).ToString();
            tvT1UnforcedError.Text = _statisticController.calculateAmountOfUnforcedErrors(playerId, points).ToString();
            tvT1ForcedError.Text = _statisticController.calculateAmountOfForcedErrors(playerId, points).ToString();
            
            tvT1TotalPointsWon.Text = _statisticController.calculateTotalPointsWon(playerId, points).ToString();
        }
    }
}
