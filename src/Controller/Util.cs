using Android.App;
using Android.Content;

namespace TennisStats.src.Controller
{
    public class Util
    {
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
    }
}