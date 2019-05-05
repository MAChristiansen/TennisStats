using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TennisStats.src.Controller;
using TennisStats.Service;

namespace TennisStats
{
    public class FragmentProfilePage : Fragment
    {
        private TextView tvHeader;
        private Button btnMyStats, btnCreateMatch, btnLiveMatches, btnLogOut;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ProfilePage, container, false);

            btnMyStats = view.FindViewById<Button>(Resource.Id.btnMyStats);
            btnCreateMatch = view.FindViewById<Button>(Resource.Id.btnCreateMatch);
            btnLiveMatches = view.FindViewById<Button>(Resource.Id.btnLiveMatches);
            btnLogOut = view.FindViewById<Button>(Resource.Id.btnLogOut);

            btnMyStats.Click += delegate 
            { 
                // TODO : OutComment when we have a My Stats fragment.
                /*
                NavigationService.NavigateToFragment(FragmentManager,
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentMyStats.NewInstance());
                */
            };
            
            btnLiveMatches.Click += delegate
            {
                // TODO : OutComment when we have a Live Matches fragment.
                /*
                NavigationService.NavigateToFragment(FragmentManager,
                Activity.FindViewById<FrameLayout>(Resource.Id.fragmentContainer),
                FragmentLiveMatches.NewInstance());
                */
            };
            
            btnLogOut.Click += delegate
            {
                // TODO : Some Logout code here, HANDLE IT JAAFAR!!
                Util.logUser(view.Context, false);
                NavigationService.NavigateToPage(view.Context, typeof(MainActivity));
            };
            
            btnCreateMatch.Click += async delegate
            {
                NavigationService.NavigateToPage(view.Context, typeof(ActivityMatchSetup));
            };

            return view;
        }

        
    }
}
