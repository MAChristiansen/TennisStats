
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
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "ActivityMatch")]
    public class ActivityMatch : Activity
    {

        private FrameLayout fragmentContainer;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Match);
            
            //Creating first serve scenario
            NavigationService.AddFragment(
                FragmentManager, 
                FindViewById<FrameLayout>(Resource.Id.fragmentContainer), 
                FragmentServeScenario.NewInstance(1));
        }
    }
}
