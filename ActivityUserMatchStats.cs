
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
using TennisStats.adapter;
using TennisStats.Model;
using TennisStats.src.Controller;

namespace TennisStats
{
    [Activity(Label = "ActivityLiveScore")]
    public class ActivityUserMatchStats : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserMatchStats);

            List<Match> matchList = new List<Match>();
            
            ListView listView = FindViewById<ListView>(Resource.Id.livescore);

            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            
            var matches = await firebaseClient.Child(FBTables.FbMatch).OnceAsync<Match>();

            foreach (var match in matches)
            {
                matchList.Add(match.Object);
            }
            
//            LiveScoreAdapter liveScoreAdapter = new LiveScoreAdapter(this, Resource.Layout.LiveScoreLayout, matchList, listView);
//            listView.Adapter = liveScoreAdapter;

//            List<string> list = new List<string>();
//            list.Add("lol");
//            list.Add("fuck");
//            list.Add("haha");
//            
//            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);
//            listView.Adapter = arrayAdapter;

        }
    }
}
