using System;
using Android.App;
using Android.Content;
using Android.Preferences;

namespace TennisStats.src.Controller
{
    public class Util
    {

        private static ISharedPreferences prefs;
        
        public static AlertDialog.Builder SimpleAlert(Context context, string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder (context);
            alert.SetTitle (title);
            alert.SetMessage (message);

            return alert;
        }

        public static ProgressDialog SimpleLoading(Context context, string message, bool cancelable = false)
        {
            ProgressDialog progressDialog = new ProgressDialog(context);
            progressDialog.SetMessage(message);
            progressDialog.SetCancelable(cancelable);

            return progressDialog;
        }

        public static long GenerateTimeStamp()
        {
            //DateTime.MinValue is 01/01/01 00:00 so add 1969 years. to get 1/1/1970
            return (long) DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;           
        }

        public static void logIn(Context context)
        {
            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean("userLogin", true);
        }

        public static Boolean isLoggetIn(Context context)
        {
            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            return prefs.GetBoolean("userLogin", false);
        }
        
    }
}