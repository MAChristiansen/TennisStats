﻿using Android.App;
using Android.OS;
using Android.Widget;
using TennisStats.Service;

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
            Button button = FindViewById<Button>(Resource.Id.btnContinue3);

            button.Click += delegate
            {
                NavigationService.NavigateToPage(this, typeof(ActivityMatchSetup));
            };
        }
    }
}

