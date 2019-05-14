
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Database.Query;
using TennisStats.Model;
using TennisStats.src.Controller;
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "ActivityProfileSettings")]
    public class ActivityProfileSettings : Activity, DatePickerDialog.IOnDateSetListener
    {
        private Player.PlayerBuilder _player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileSettings);
            
            _player = new Player.PlayerBuilder(Intent.Extras.GetString("Id")).Password(Intent.Extras.GetString("password"));

            EditText etName = FindViewById<EditText>(Resource.Id.txtProfileSettingsName);
            Button btnBirthDay = FindViewById<Button>(Resource.Id.btnProfileSettingsBirthday);
            Button btnSaveSettings = FindViewById<Button>(Resource.Id.btnSaveSettings);
            Spinner clubSpinner = FindViewById<Spinner>(Resource.Id.spinnerClub);
            Switch handSwitch = FindViewById<Switch>(Resource.Id.switchProfileSettingsHand);
            Switch genderSwitch = FindViewById<Switch>(Resource.Id.switchProfileSettingsGender);
            
            
            handSwitch.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e) {
                var hand = e.IsChecked ? "Right" : "Left";
                handSwitch.Text = "Playing hand is set to: " + hand;
                Console.WriteLine(e.IsChecked);
                _player.Hand(e.IsChecked);
            };
            
            genderSwitch.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e) {
                var gender = e.IsChecked ? "Male" : "Female";
                genderSwitch.Text = "Gender is set to: " + gender;
                _player.Gender(e.IsChecked);
            };
                
            PopulateClubList(clubSpinner);

            btnBirthDay.Click += delegate { OnClickDatePicker(this); };

            btnSaveSettings.Click += async delegate
            {
                FirebaseClient firebaseClient = Constants.FirebaseClient;
                _player.Name(etName.Text.Trim());
                Player player = _player.build();
                Util.PutStringToPreference(this, Constants.UserId, player.PlayerId);
                await firebaseClient.Child(Constants.FbUser).Child(player.PlayerId).PutAsync(player);
                NavigationService.NavigateToPage(this, typeof(ActivityProfilePage));
            };
        }

        private async void PopulateClubList(Spinner spinner)
        {
            spinner.ItemSelected += onClubSelected;
            List<string> clubList = new List<string>();

            FirebaseClient firebaseClient = Constants.FirebaseClient;
            
            var clubs = await firebaseClient.Child(Constants.FbClub).OnceAsync<object>();

            foreach (var club in clubs)
            {
                clubList.Add(club.Key);
            }
            
            clubList.Insert(0, "Choose club");

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, clubList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void onClubSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Console.WriteLine(spinner.GetItemAtPosition(e.Position).ToString());
            _player.ClubId(spinner.GetItemAtPosition(e.Position).ToString());
        }
        
        private void OnClickDatePicker(Context context)
        {
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(context, this, dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day);
            datePicker.Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            var baseDate = new DateTime (1970, 01, 01);
            var toDate = new DateTime (year, (month + 1), dayOfMonth);
            var numberOfSeconds = toDate.Subtract (baseDate).TotalSeconds;
            var timestamp = (long)numberOfSeconds * 1000;
            _player.Birthday(timestamp);
        }
    }
}
