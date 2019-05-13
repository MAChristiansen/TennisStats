
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
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "Your Profile")]
    public class ActivityProfilePage : Activity
    {
        private TextView tvHeader;
        private Button btnMyStats, btnCreateMatch, btnLiveMatches, btnLogOut;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ProfilePage);


            tvHeader = FindViewById<TextView>(Resource.Id.tvHeader);
            btnMyStats = FindViewById<Button>(Resource.Id.btnMyStats);
            btnCreateMatch = FindViewById<Button>(Resource.Id.btnCreateMatch);
            btnLiveMatches = FindViewById<Button>(Resource.Id.btnLiveMatches);
            btnLogOut = FindViewById<Button>(Resource.Id.btnLogOut);

            tvHeader.Text = "Welcome " + Util.GetStringFromPreference(this, Constants.UserId);
            
            btnMyStats.Click += delegate 
            { 
                NavigationService.NavigateToPage(this, typeof(ActivityProfileStat));
            };
            
            btnLiveMatches.Click += delegate
            {
                NavigationService.NavigateToPage(this, typeof(ActivityLiveScore));
            };
            
            btnLogOut.Click += delegate
            {
                //Remove the userId from SharedPreference, and kill all activities and start from the main.
                Util.RemoveStringFromPreference(this, Constants.UserId);
                FinishAffinity();
                StartActivity(typeof(MainActivity));
            };
            
            btnCreateMatch.Click += delegate
            {
                NavigationService.NavigateToPage(this, typeof(ActivityMatchSetup));
            };
            
        }
    }
}
