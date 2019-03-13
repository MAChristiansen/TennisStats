
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
using TennisStats.Service;

namespace TennisStats
{
    public class FragmentHowWin : Fragment
    {
        private ImageView ivWinner, ivForcedError, ivUnforcedError;
        public static FragmentHowWin NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentHowWin();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.HowWin, container, false);

            ivWinner = view.FindViewById<ImageView>(Resource.Id.ivWinner);
            ivForcedError = view.FindViewById<ImageView>(Resource.Id.ivForcedError);
            ivUnforcedError = view.FindViewById<ImageView>(Resource.Id.ivUnforcedError);


            ivWinner.Click += delegate
            {
                Navigate();
            };

            ivForcedError.Click += delegate
            {
                Navigate();
            };

            ivUnforcedError.Click += delegate
            {
                Navigate();
            };

            return view;
        }

        private void Navigate()
        {
            NavigationService.NavigateToFragment(
                FragmentManager, 
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentStrokeType.NewInstance());
        }
    }
}
