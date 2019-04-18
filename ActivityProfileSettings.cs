
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

            btnBirthDay.Click += delegate { OnClickDatePicker(this); };

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
