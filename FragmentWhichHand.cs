
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
using static TennisStats.Enum.HandPositionEnum;
using static TennisStats.Enum.StrokeTypeEnum;

namespace TennisStats
{
    public class FragmentWhichHand : Fragment
    {
        private ImageView ivForeHand, ivBackHand;

        private MatchController matchController;

        public static FragmentWhichHand NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentWhichHand();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            matchController = MatchController.Instance;
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.WhichHand, container, false);

            ivForeHand = view.FindViewById<ImageView>(Resource.Id.ivForeHand);
            ivBackHand = view.FindViewById<ImageView>(Resource.Id.ivBackHand);

            ivForeHand.Click += delegate { MatchController.inPlayPB.handPosition(HandPosition.FORHAND); Navigate(); };
            ivBackHand.Click += delegate { MatchController.inPlayPB.handPosition(HandPosition.BACKHAND); Navigate(); };    
            
            return view;
        }

        private void Navigate()
        {
            MatchController.Instance.inPlay();
            Bundle bundle = new Bundle();
            bundle.PutInt("team1", matchController.GetCurrentGameScore()[0]);
            bundle.PutInt("team2", matchController.GetCurrentGameScore()[1]);
            NavigationService.NavigateToFragment(
                FragmentManager,
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentScore.NewInstance(bundle));
        }
    }
}
