
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
using static TennisStats.Enum.MatchParticipantsEnum;

namespace TennisStats
{
    public class FragmentQuickMatchSetup : Fragment
    {
        private Spinner sCategory;

        private static MatchController matchController;

        private EditText etTeam1Player1, etTeam1Player2, etTeam2Player1, etTeam2Player2;

        private ImageView ivNext;

        public static FragmentQuickMatchSetup NewInstance()
        {
            Bundle bundle = new Bundle();
            matchController = MatchController.Instance;
            
            return new FragmentQuickMatchSetup();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.MatchSetupTeam, container, false);

            
            sCategory = view.FindViewById<Spinner>(Resource.Id.sCategory);
            etTeam1Player1 = view.FindViewById<EditText>(Resource.Id.etTeam1Player1);
            etTeam1Player2 = view.FindViewById<EditText>(Resource.Id.etTeam1Player2);
            etTeam2Player1 = view.FindViewById<EditText>(Resource.Id.etTeam2Player1);
            etTeam2Player2 = view.FindViewById<EditText>(Resource.Id.etTeam2Player2);
            ivNext = view.FindViewById<ImageView>(Resource.Id.ivNext);
            
            etTeam1Player2.Visibility = ViewStates.Invisible;
            etTeam2Player2.Visibility = ViewStates.Invisible;

            //Match Category spinner
            sCategory.ItemSelected += spinner_ItemSelected;
            var adapterCategory = ArrayAdapter.CreateFromResource (
                Activity, Resource.Array.spinner_Match_Category, Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sCategory.Adapter = adapterCategory;

            ivNext.Click += delegate
            {
                matchController.CreateMatch(etTeam1Player1.Text, etTeam2Player1.Text, MatchParticipants.SINGLE);
                NavigationService.NavigateToPage(Activity, typeof(ActivityMatch));
            };

            return view;
        }
        
        private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            if (spinner.GetItemAtPosition(e.Position).ToString().Equals("Single"))
            {
                etTeam1Player2.Visibility = ViewStates.Invisible;
                etTeam2Player2.Visibility = ViewStates.Invisible;
            }
            else
            {
                etTeam1Player2.Visibility = ViewStates.Visible;
                etTeam2Player2.Visibility = ViewStates.Visible;
            }
        }
    }
}
