
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

namespace TennisStats
{
    [Activity(Label = "ActivityCreateAccount")]
    public class ActivityCreateAccount : Activity
    {
        private List<string> _existingUsernames = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);

            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            
            Button btnCreateAccount = FindViewById<Button>(Resource.Id.btnCreateAccount);
            
            
            btnCreateAccount.Click += async delegate
            {
                //TODO Put in a spinner
                
                // Get all usernames from database
                var users = await firebaseClient.Child(FBTables.FbUser).OnceAsync<Player>();
  
                foreach (var user in users)
                {
                    _existingUsernames.Add(user.Key);
                    Console.WriteLine("User key is: " + user.Key);
                }
                
                Player player = new Player.PlayerBuilder("21", "jaafar", "København", HandEnum.Hand.RIGHT, GenderEnum.Gender.MALE, 100).build();

                // If username already exists
                if (_existingUsernames.Contains(player.Name))
                {
                    //TODO Show alert 
                    Console.WriteLine("User already exists");
                }
                else
                {
                    await firebaseClient.Child(FBTables.FbUser).Child("jaafar").PutAsync(player);
                }
            };
        }
    }
}
