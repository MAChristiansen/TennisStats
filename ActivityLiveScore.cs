using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ActivityLiveScore : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LiveScore);

            List<Match> matchList = new List<Match>();
            
            ListView listView = FindViewById<ListView>(Resource.Id.livescore);
            Button btnUpdate = FindViewById<Button>(Resource.Id.btnUpdateLiveScore);

            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            
            var matches = await firebaseClient.Child(FBTables.FbMatch).OnceAsync<Match>();
            
            foreach (var match in matches)
            {
                matchList.Add(match.Object);
            }

            LiveScoreAdapter liveScoreAdapter = new LiveScoreAdapter(this, Resource.Layout.LiveScoreLayout, matchList);
            listView.Adapter = liveScoreAdapter;
            
            btnUpdate.Click += async delegate { await UpdateMatchList(firebaseClient, matchList, liveScoreAdapter); };
        }

        private static async Task UpdateMatchList(FirebaseClient firebaseClient, List<Match> matchList, LiveScoreAdapter liveScoreAdapter)
        {
            var matches = await firebaseClient.Child(FBTables.FbMatch).OnceAsync<Match>();
            
            liveScoreAdapter.Clear();
            
            foreach (var match in matches)
            {
                liveScoreAdapter.Add(match.Object);
            }
            
            liveScoreAdapter.NotifyDataSetChanged();
        }
    }
}