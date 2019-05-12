using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Java.Util;
using TennisStats.Model;
using TennisStats.src.Controller;
using TennisStats.Service;
using static TennisStats.Enum.StatisticTypeEnum;
using Point = System.Drawing.Point;

namespace TennisStats
{
    [Activity(Label = "TennisStats", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnQuickMatch = FindViewById<Button>(Resource.Id.btnQuickMatch);
            Button btnCreateMatch = FindViewById<Button>(Resource.Id.btnCreateMatch);
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogIn);
            Button btnLiveScore = FindViewById<Button>(Resource.Id.btnLiveScore);

            Console.WriteLine(Util.GetStringFromPreference(this, Constants.UserId));
            
            if (Util.GetStringFromPreference(this, Constants.UserId) != Constants.Default) { btnLogin.Text = "Go To Profile"; }

            btnQuickMatch.Click += delegate { NavigationService.NavigateToPage(this, typeof(ActivityMatchSetup)); };

            btnLogin.Click += delegate
            {
                //Determine the navigation based on logged in or not
                NavigationService.NavigateToPage(this,
                    Util.GetStringFromPreference(this, Constants.UserId) != Constants.Default
                        ? typeof(ActivityProfilePage)
                        : typeof(ActivityLogin));
            };

            btnCreateMatch.Click += delegate { NavigationService.NavigateToPage(this, typeof(ActivityProfileStat)); };

            btnLiveScore.Click += delegate { NavigationService.NavigateToPage(this, typeof(ActivityLiveScore)); };
        }
    }
}
