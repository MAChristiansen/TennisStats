
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

namespace TennisStats.Resources.layout
{

    [Activity(Label = "ProfileStats")]
    public class ProfileStats : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ProfilStat);
            
            // Define widgets
            TextView tvProfileName = FindViewById(Resource.Id.tvProfilName);
            TextView tvClub = FindViewById(Resource.Id.tvClub);
            Button btnOverall = FindViewById<Button>(Resource.Id.btOverall);
            Button btnLastYear = FindViewById<Button>(Resource.Id.btLastYear);
            Button btnLastMonth = FindViewById<Button>(Resource.Id.btLastMonth);
            Button btnMatch = FindViewById<Button>(Resource.Id.btMatch);
        }
    }
}
