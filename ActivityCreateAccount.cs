
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
using Firebase.Database.Query;
using TennisStats.Enum;
using TennisStats.Model;
using TennisStats.src.Controller;
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "ActivityCreateAccount")]
    public class ActivityCreateAccount : Activity
    {
        private readonly List<string> _existingUsernames = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);

            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            
            Button btnCreateAccount = FindViewById<Button>(Resource.Id.btnCreateAccount);
            EditText txtUsername = FindViewById<EditText>(Resource.Id.txtCreateAccountUsername);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.txtCreateAccountPassword);
            
            btnCreateAccount.Click += async delegate
            {
                if (!(txtUsername.Text.Trim().Length > 0) || !(txtPassword.Text.Trim().Length > 0))
                {
                    Dialog dialog = Util.SimpleAlert(this, "Missing fields", "Username or password is invalid").Create();
                    dialog.Show();
                    return;
                }

                ProgressDialog progressDialog = Util.SimpleLoading(this, "Creating profile...");
                progressDialog.Show();
                
                // Get all usernames from database
                var users = await firebaseClient.Child(FBTables.FbUser).OnceAsync<Player>();
  
                foreach (var user in users)
                {
                    _existingUsernames.Add(user.Key);
                }
                
                Player player = new Player.PlayerBuilder(txtUsername.Text.Trim())
                    .Password(txtPassword.Text.Trim())
                    .build();
                
                // If username already exists
                if (_existingUsernames.Contains(player.PlayerId))
                {
                    progressDialog.Dismiss();
                    Dialog dialog = Util.SimpleAlert(this, "Error", "Username already in use").Create();
                    dialog.Show();
                }
                else
                {
                    await firebaseClient.Child(FBTables.FbUser).Child(player.PlayerId).PutAsync(player);
                    progressDialog.Dismiss();
                    NavigationService.NavigateToPage(this, typeof(ActivityProfileSettings));
                }
            };
        }
    }
}
