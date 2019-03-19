
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
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "LoginActivity")]
    public class ActivityLogin : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            Button btnCreateAccount = FindViewById<Button>(Resource.Id.btnCreateAccount);
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnLogin.Click += delegate { NavigationService.NavigateToPage(this, typeof(ActivityProfileSettings)); };
            btnCreateAccount.Click += delegate { NavigationService.NavigateToPage(this, typeof(ActivityCreateAccount)); };
        }
    }
}
