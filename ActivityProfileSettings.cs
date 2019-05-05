
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using TennisStats.src.Controller;

namespace TennisStats
{
    [Activity(Label = "ActivityProfileSettings")]
    public class ActivityProfileSettings : Activity, DatePickerDialog.IOnDateSetListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileSettings);

            Button btnBirthDay = FindViewById<Button>(Resource.Id.btnProfileSettingsBirthday);
            Spinner clubSpinner = FindViewById<Spinner>(Resource.Id.spinnerClub);
            
            PopulateClubList(clubSpinner);

            btnBirthDay.Click += delegate { OnClickDatePicker(this); };

        }

        private async void PopulateClubList(Spinner spinner)
        {
            spinner.ItemSelected += onClubSelected;
            List<string> clubList = new List<string>();

            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            
            var clubs = await firebaseClient.Child(FBTables.FbClub).OnceAsync<object>();

            foreach (var club in clubs)
            {
                clubList.Add(club.Key);
            }

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, clubList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void onClubSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Console.WriteLine(spinner.GetItemAtPosition(e.Position).ToString());
        }
        
        private void OnClickDatePicker(Context context)
        {
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(context, this, dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day);
            datePicker.Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            throw new NotImplementedException();
        }
    }
}
