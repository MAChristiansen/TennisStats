
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
    public class FragmentWhichHand : Fragment
    {
        private ImageView ivForeHand, ivBackHand;

        public static FragmentWhichHand NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentWhichHand();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.WhichHand, container, false);

            ivForeHand = view.FindViewById<ImageView>(Resource.Id.ivForeHand);
            ivBackHand = view.FindViewById<ImageView>(Resource.Id.ivBackHand);

            ivForeHand.Click += delegate { Navigate(); };
            ivBackHand.Click += delegate { Navigate(); };    
            
            return view;
        }

        private void Navigate()
        {
            NavigationService.NavigateToFragment(
                FragmentManager,
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentScore.NewInstance());
        }
    }
}
