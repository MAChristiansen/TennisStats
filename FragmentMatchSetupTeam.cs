
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
    public class FragmentMatchSetupTeam : Fragment
    {
        private Spinner sCategory;

        private EditText etTeam1Player1, etTeam1Player2, etTeam2Player1, etTeam2Player2;

        private ImageView ivNext;

        public static FragmentMatchSetupTeam NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentMatchSetupTeam();
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
            sCategory.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
            var adapterCategory = ArrayAdapter.CreateFromResource (
                Activity, Resource.Array.spinner_Match_Category, Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sCategory.Adapter = adapterCategory;

            ivNext.Click += delegate
            {
                NavigationService.NavigateToPage(Activity, typeof(ActivityMatch));
            };

            return view;
        }
        
        private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format ("The Category is {0}", spinner.GetItemAtPosition (e.Position));
            Toast.MakeText (Activity, toast, ToastLength.Long).Show ();

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
