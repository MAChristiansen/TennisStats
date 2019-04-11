
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
using static TennisStats.Enum.StrokeTypeEnum;

namespace TennisStats
{
    public class FragmentStrokeType : Fragment
    {

        private ImageView ivDropShot, ivSmash, ivVolley, ivLob, ivApproach, ivReturn, ivBaseline;
        
        public static FragmentStrokeType NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentStrokeType();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StrokeType, container, false);

            ivDropShot = view.FindViewById<ImageView>(Resource.Id.ivDropShot);
            ivSmash = view.FindViewById<ImageView>(Resource.Id.ivSmash);
            ivVolley = view.FindViewById<ImageView>(Resource.Id.ivVolley);
            ivLob = view.FindViewById<ImageView>(Resource.Id.ivLob);
            ivApproach = view.FindViewById<ImageView>(Resource.Id.ivApproach);
            ivReturn = view.FindViewById<ImageView>(Resource.Id.ivReturn);
            ivBaseline = view.FindViewById<ImageView>(Resource.Id.ivBaseline);

            ivDropShot.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.DROPSHOT); Navigate(); };
            ivSmash.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.SMASH); Navigate();};
            ivVolley.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.VOLLEY); Navigate(); };
            ivLob.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.LOB); Navigate(); };
            ivApproach.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.APPROACH); Navigate(); };
            ivReturn.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.RETURN); Navigate();};
            ivBaseline.Click += delegate { MatchController.inPlayPB.strokeType(StrokeType.BASELINE); Navigate(); };
            
            
            return view;
        }

        private void Navigate()
        {
            NavigationService.NavigateToFragment(
                FragmentManager,
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentWhichHand.NewInstance());
        }
    }
}
