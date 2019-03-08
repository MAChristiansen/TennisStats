
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
    public class FragmentServeScenario : Fragment
    {
        private TextView tvHeader;

        private ImageView ivAce, ivFault, ivInPlay, ivFootFault, ivServiceWinner;

        public static FragmentServeScenario NewInstance(int serve)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt("serve", serve);

            FragmentServeScenario newFragment = new FragmentServeScenario {Arguments = bundle};
            
            return newFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.ServScenario, container, false);

            int serve = Arguments.GetInt("serve");
            
            tvHeader = view.FindViewById<TextView>(Resource.Id.tvHeader);
            ivAce = view.FindViewById<ImageView>(Resource.Id.ivAce);
            ivFault = view.FindViewById<ImageView>(Resource.Id.ivFault);
            ivInPlay = view.FindViewById<ImageView>(Resource.Id.ivInPlay);
            ivFootFault = view.FindViewById<ImageView>(Resource.Id.ivFootFault);
            ivServiceWinner = view.FindViewById<ImageView>(Resource.Id.ivServiceWinner);

            if (serve == 2)
            {
                tvHeader.Text = "Second Serve";
            }

            ivFault.Click += delegate
            {
                if (serve == 2)
                {
                    Navigate(FragmentScore.NewInstance());
                    return;
                }
                Navigate(NewInstance(2));
            };

            ivAce.Click += delegate
            {
                Navigate(FragmentScore.NewInstance());
            };

            ivInPlay.Click += delegate
            {
                Navigate(FragmentWhoWon.NewInstance());
            };

            ivFootFault.Click += delegate
            {
                if (serve == 2)
                {
                    Navigate(FragmentScore.NewInstance());
                    return;
                }
                Navigate(NewInstance(2));
            };

            ivServiceWinner.Click += delegate
            {
                Navigate(FragmentScore.NewInstance());
            };
            
            return view;
        }

        private void Navigate(Fragment destination)
        {
            NavigationService.NavigateToFragment(
                FragmentManager, 
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer), 
                destination);
        }
    }
}
