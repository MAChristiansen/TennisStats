
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
using Java.Lang;

namespace TennisStats
{
    public class FragmentScore : Fragment
    {
        public static FragmentScore NewInstance(Bundle bundle)
        {
            FragmentScore fragmentScore = new FragmentScore();
            fragmentScore.Arguments = bundle;

            return fragmentScore;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Score, container, false);

            TextView tvScore = view.FindViewById<TextView>(Resource.Id.tvScore);

            tvScore.Text = Arguments.GetInt("team1", 0)*15 + " - " + Arguments.GetInt("team2", 0)*15;
            Task.Delay(1000).ContinueWith(t=> FragmentManager.PopBackStack(null, PopBackStackFlags.Inclusive));



            return view;
        }
    }
}
