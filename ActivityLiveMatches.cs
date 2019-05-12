
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media.Effect;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TennisStats.Model;
using TennisStats.src.Controller;
using TennisStats.Service;

namespace TennisStats
{
    [Activity(Label = "ActivityLiveMatches")]
    public class ActivityLiveMatches : Activity
    {
        public static IDisposable returnedEvents;
        
        private ListView lvLiveMatches;
        private ObservableCollection<FragmentLiveMathListItem> fragmentList;
        private List<Match> matchList;

        private ListAdapter listAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LiveMatches); 
            
            lvLiveMatches = FindViewById<ListView>(Resource.Id.lvLiveMatches);
            
            matchList = new List<Match>();
            fragmentList = new ObservableCollection<FragmentLiveMathListItem>();
            
            var firebaseClient = FBTables.FirebaseClient;
            
            listAdapter = new ListAdapter(this, fragmentList);
            lvLiveMatches.Adapter = listAdapter;
            
            
            //var matches = firebaseClient.Child(FBTables.FBMatch).OnceAsync<Match>();

            /*
            foreach (var match in matches)
            {
                matchList.Add(new FragmentLiveMathListItem());
                matchList[matchList.Count - 1].setMatch(match.Object);
            }
            */
            
            /*
            firebaseClient.Child(FBTables.FBMatch).AsObservable<Match>().Subscribe(res =>
            {
                matchList.Add(new FragmentLiveMathListItem());
                matchList[matchList.Count - 1].setMatch(res.Object);
                Console.WriteLine("Match " + "Callback from LiveMatches RES: " + res.Object.Team1Id);
            });
            */
            
            

            firebaseClient.Child(FBTables.FBMatch).AsObservable<Match>().Where(res => !matchList.Contains(res.Object))
                .Subscribe(res =>
                {
                    matchList.Add(res.Object);
                    updateListView();
                });
        }

        void updateListView()
        {
            /*
            Console.WriteLine("Match Amount " + matchList.Count);
            fragmentList.Add(new FragmentLiveMathListItem());
            fragmentList[fragmentList.Count - 1].setMatch(matchList[matchList.Count - 1]);

            listAdapter.setItems(fragmentList);
            */
            
            /*
            Console.WriteLine("Match Amount " + matchList.Count);
            fragmentList.Add(new FragmentLiveMathListItem());
            Console.WriteLine("Match Fragment " + fragmentList.Count);
            fragmentList[fragmentList.Count - 1].setMatch(matchList[matchList.Count - 1]);
            Console.WriteLine("Match With in fragment " + fragmentList[0].getMatch().MatchId);
            */
            
            listAdapter.NotifyDataSetChanged();
            
        }
        
    }
}
