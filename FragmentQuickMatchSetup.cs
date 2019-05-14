
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
using Firebase.Database;
using TennisStats.Service;
using TennisStats.src.Controller;
using static TennisStats.Enum.MatchParticipantsEnum;
using static TennisStats.Enum.MatchTypeEnum;

namespace TennisStats
{
    public class FragmentQuickMatchSetup : Fragment
    {
        private Spinner sCategory, sType;

        private MatchType matchType = MatchType.THREESETTER;

        private MatchParticipants matchCategory = MatchParticipants.SINGLE;

        private static MatchController matchController;

        private EditText etTeam1Player1, etTeam1Player2, etTeam2Player1, etTeam2Player2;

        private ImageView ivNext;

        private TextView tvChoosePlayer1Team1, tvChoosePlayer2Team1, tvChoosePlayer1Team2, tvChoosePlayer2Team2;

        private Switch switchChooseServer;

        private Spinner spinnerTeam1Player1, spinnerTeam1Player2, spinnerTeam2Player1, spinnerTeam2Player2;

        private bool isSingle = true;

        private bool switchChooseServerStatus;

        public static FragmentQuickMatchSetup NewInstance()
        {
            Bundle bundle = new Bundle();
            matchController = MatchController.Instance;
            
            return new FragmentQuickMatchSetup();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.QuickMatchSetup, container, false);

            Activity.ActionBar.Title = "Quick Match Setup"; 
            
            sCategory = view.FindViewById<Spinner>(Resource.Id.sCategory);
            sType = view.FindViewById<Spinner>(Resource.Id.sType);
            switchChooseServer = view.FindViewById<Switch>(Resource.Id.switch_choose_server);
            etTeam1Player1 = view.FindViewById<EditText>(Resource.Id.etTeam1Player1);
            etTeam1Player2 = view.FindViewById<EditText>(Resource.Id.etTeam1Player2);
            etTeam2Player1 = view.FindViewById<EditText>(Resource.Id.etTeam2Player1);
            etTeam2Player2 = view.FindViewById<EditText>(Resource.Id.etTeam2Player2);
            tvChoosePlayer1Team1 = view.FindViewById<TextView>(Resource.Id.tvChoosePlayer1Team1);
            tvChoosePlayer2Team1 = view.FindViewById<TextView>(Resource.Id.tvChoosePlayer2Team1);
            tvChoosePlayer1Team2 = view.FindViewById<TextView>(Resource.Id.tvChoosePlayer1Team2);
            tvChoosePlayer2Team2 = view.FindViewById<TextView>(Resource.Id.tvChoosePlayer2Team2);
            ivNext = view.FindViewById<ImageView>(Resource.Id.ivNext);

            spinnerTeam1Player1 = view.FindViewById<Spinner>(Resource.Id.spinnerTeam1Player1);
            spinnerTeam1Player2 = view.FindViewById<Spinner>(Resource.Id.spinnerTeam1Player2);
            spinnerTeam2Player1 = view.FindViewById<Spinner>(Resource.Id.spinnerTeam2Player1);
            spinnerTeam2Player2 = view.FindViewById<Spinner>(Resource.Id.spinnerTeam2Player2);
            
            PopulatePlayerList(spinnerTeam1Player1, etTeam1Player1);
            PopulatePlayerList(spinnerTeam1Player2, etTeam1Player2);
            PopulatePlayerList(spinnerTeam2Player1, etTeam2Player1);
            PopulatePlayerList(spinnerTeam2Player2, etTeam2Player2);
            
            tvChoosePlayer1Team1.Click += delegate { UpdateUiForSpinner(spinnerTeam1Player1, etTeam1Player1, tvChoosePlayer1Team1); };
            tvChoosePlayer1Team2.Click += delegate { UpdateUiForSpinner(spinnerTeam2Player1, etTeam2Player1, tvChoosePlayer1Team2); };
            tvChoosePlayer2Team1.Click += delegate { UpdateUiForSpinner(spinnerTeam1Player2, etTeam1Player2, tvChoosePlayer2Team1); };
            tvChoosePlayer2Team2.Click += delegate { UpdateUiForSpinner(spinnerTeam2Player2, etTeam2Player2, tvChoosePlayer2Team2); };


            //Match Category spinner
            sCategory.ItemSelected += spinner_ItemSelected_Match_Category;
            var adapterCategory = ArrayAdapter.CreateFromResource (
                Activity, 
                Resource.Array.spinner_Match_Category, 
                Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sCategory.Adapter = adapterCategory;
            
            //Match Form spinner
            sType.ItemSelected += spinner_ItemSelected_Match_Type;
            var adapterForm = ArrayAdapter.CreateFromResource (
                Activity, 
                Resource.Array.spinner_Match_Form, 
                Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sType.Adapter = adapterForm;
            
            switchChooseServer.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e)
            {
                switchChooseServer.Text = e.IsChecked ? "Team 2 starts serving" : "Team 1 starts serving";
                switchChooseServerStatus = e.IsChecked;
            };

            ivNext.Click += delegate
            {
                switch (matchCategory)
                {
                    case MatchParticipants.SINGLE:
                        matchController.CreateMatch(etTeam1Player1.Text, 
                            etTeam2Player1.Text, 
                            matchCategory, 
                            matchType,
                            switchChooseServerStatus);
                        NavigationService.NavigateToPage(Activity, typeof(ActivityMatch));
                    break;
                    
                    case MatchParticipants.DOUBLE:
                        matchController.CreateMatch(etTeam1Player1.Text + " | " + etTeam1Player2.Text, 
                            etTeam2Player1.Text + " | " + etTeam2Player2.Text, 
                            matchCategory, 
                            matchType,
                            switchChooseServerStatus);
                        NavigationService.NavigateToPage(Activity, typeof(ActivityMatch));
                        break;
                }
            };
            
            return view;
        }

        private void UpdateUiForSpinner(Spinner spinner, EditText editText, TextView textView)
        {
            if (editText.Visibility == ViewStates.Visible)
            {
                textView.Text = "Enter name yourself";
                editText.Visibility = ViewStates.Invisible;
                spinner.Visibility = ViewStates.Visible;
            }
            else
            {
                textView.Text = "Choose player";
                editText.Visibility = ViewStates.Visible;
                spinner.Visibility = ViewStates.Invisible;
            }
        }

        private async void PopulatePlayerList(Spinner spinner, EditText editText)
        {
            spinner.ItemSelected += onClubSelected;
            List<string> playerList = new List<string>();

            FirebaseClient firebaseClient = Constants.FirebaseClient;
            
            var players = await firebaseClient.Child(Constants.FbUser).OnceAsync<object>();

            foreach (var player in players)
            {
                playerList.Add(player.Key);
            }
            
            playerList.Insert(0, "Choose player");

            var adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, playerList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }
        
        private void onClubSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            if (spinner == spinnerTeam1Player1)
            {
                etTeam1Player1.Text = spinner.GetItemAtPosition(e.Position).ToString();
            }

            if (spinner == spinnerTeam1Player2)
            {
                etTeam1Player2.Text = spinner.GetItemAtPosition(e.Position).ToString();
            }

            if (spinner == spinnerTeam2Player1)
            {
                etTeam2Player1.Text = spinner.GetItemAtPosition(e.Position).ToString();
            }

            if (spinner == spinnerTeam2Player2)
            {
                etTeam2Player2.Text = spinner.GetItemAtPosition(e.Position).ToString();
            }
        }
        
        private void spinner_ItemSelected_Match_Category (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            if (spinner.GetItemAtPosition(e.Position).ToString().Equals("Single"))
            {
                etTeam1Player2.Visibility = ViewStates.Invisible;
                tvChoosePlayer2Team1.Visibility = ViewStates.Invisible;
                etTeam2Player2.Visibility = ViewStates.Invisible;
                tvChoosePlayer2Team2.Visibility = ViewStates.Invisible;

                matchCategory = MatchParticipants.SINGLE;
            }
            else
            {
                etTeam1Player2.Visibility = ViewStates.Visible;
                tvChoosePlayer2Team1.Visibility = ViewStates.Visible;
                etTeam2Player2.Visibility = ViewStates.Visible;
                tvChoosePlayer2Team2.Visibility = ViewStates.Visible;

                matchCategory = MatchParticipants.DOUBLE;
            }
        }

        private void spinner_ItemSelected_Match_Type(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner) sender;

            switch (spinner.GetItemAtPosition(e.Position).ToString())
            {
               case "Best of 1 Set":
                   matchType = MatchType.ONESETTER;
                   break;
               case "Best of 3 Sets":
                   matchType = MatchType.THREESETTER;
                   break;
               case "Best of 5 Sets":
                   matchType = MatchType.FIVESETTER;
                   break;    
            }
        }
    }
}
