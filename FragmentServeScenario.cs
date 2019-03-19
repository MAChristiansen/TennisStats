
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
using TennisStats.src.Controller;
using static TennisStats.Enum.FaultCountEnum;

namespace TennisStats
{
    public class FragmentServeScenario : Fragment
    {
        private TextView tvHeader;

        private static MatchController matchController;

        private ImageView ivAce, ivFault, ivInPlay, ivFootFault, ivServiceWinner;

        public static FragmentServeScenario NewInstance(int serve)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt("serve", serve);

            FragmentServeScenario newFragment = new FragmentServeScenario {Arguments = bundle};
            matchController = MatchController.Instance;
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
                // Run logic for the fault scenario
                FaultCount currentFaultCount = matchController.Fault();

                // Check the fault count - Second or First serve
                if (currentFaultCount == FaultCount.SECONDSERVE)
                {
                    //Run code for the second serve scenario
                    Bundle bundle = new Bundle();
                    bundle.PutInt("team1", matchController.GetCurrentGameScore()[0]);
                    bundle.PutInt("team2", matchController.GetCurrentGameScore()[1]);
                    Navigate(FragmentScore.NewInstance(bundle));
                    return;
                }

                /*
                 *   It was a first serve, so reset the activity,
                 *   but set the serve count to 2. This is used
                 *   to set the header of the activity
                 */
                Navigate(NewInstance(2));
            };

            ivAce.Click += delegate
            {
                // Run logic for ace scenario
                matchController.Ace();
                Bundle bundle = new Bundle();
                bundle.PutInt("team1", matchController.GetCurrentGameScore()[0]);
                bundle.PutInt("team2", matchController.GetCurrentGameScore()[1]);
                Navigate(FragmentScore.NewInstance(bundle));


            };

            ivInPlay.Click += delegate
            {
                Navigate(FragmentWhoWon.NewInstance());
            };

            ivFootFault.Click += delegate
            {
                if (serve == 2)
                {
                    Navigate(FragmentScore.NewInstance(new Bundle()));
                    return;
                }
                Navigate(NewInstance(2));
            };

            ivServiceWinner.Click += delegate
            {
                Navigate(FragmentScore.NewInstance(new Bundle()));
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
