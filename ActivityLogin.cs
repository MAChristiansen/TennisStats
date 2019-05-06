
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
using Java.Lang;
using TennisStats.Model;
using TennisStats.src.Controller;
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "LoginActivity")]
    public class ActivityLogin : Activity
    {
        private readonly List<string> _existingUsernames = new List<string>();
        private readonly List<Player> _existingPlayers = new List<Player>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            
            FirebaseClient firebaseClient = FBTables.FirebaseClient;

            EditText etUsername = FindViewById<EditText>(Resource.Id.txtLoginUsername);
            EditText etPassword = FindViewById<EditText>(Resource.Id.txtLoginPassword);
            
            Button btnCreateAccount = FindViewById<Button>(Resource.Id.btnCreateAccount);
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnLogin.Click += async delegate
            {
                if (!(etUsername.Text.Trim().Length > 0) || !(etPassword.Text.Trim().Length > 0))
                {
                    Dialog dialog = Util.SimpleAlert(this, "Missing fields", "Username or password is invalid").Create();
                    dialog.Show();
                    return;
                }
                
                ProgressDialog progressDialog = Util.SimpleLoading(this, "Loading profile...");
                progressDialog.Show();
                
                var users = await firebaseClient.Child(FBTables.FbUser).OnceAsync<Player>();
  
                foreach (var user in users)
                {
                    _existingUsernames.Add(user.Key);
                    _existingPlayers.Add(user.Object);
                }

                if (_existingUsernames.Contains(etUsername.Text))
                {
                    Player player = FindSpecificPlayer(etUsername.Text, _existingPlayers);
                    
                    if (player.Password == etPassword.Text.Trim())
                    {
                        if (player.Birthday == 0)
                        {
                            Bundle bundle = new Bundle();
                            bundle.PutString("Id", etUsername.Text.Trim());
                            bundle.PutString("password", etPassword.Text.Trim());
                            NavigationService.NavigateToPage(this, typeof(ActivityProfileSettings), bundle);
                        }
                    }
                    else
                    {
                        progressDialog.Dismiss();
                        ShowErrorMessage(this);
                    }
                }
                else
                {
                    progressDialog.Dismiss();
                    ShowErrorMessage(this);
                }
            };
            btnCreateAccount.Click += delegate { NavigationService.NavigateToPage(this, typeof(ActivityCreateAccount)); };
        }

        private void ShowErrorMessage(Context context)
        {
            Dialog dialog = Util.SimpleAlert(this, "Error", "Wrong username or password").Create();
            dialog.Show();
        }

        private Player FindSpecificPlayer(string username, List<Player> players)
        {
            var desiredPlayer = new Player();

            foreach (var player in players)
            {
                if (player.PlayerId == username)
                {
                    desiredPlayer = player;
                }
            }
            
            return desiredPlayer;
        }
    }
}
