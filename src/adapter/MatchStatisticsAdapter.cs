using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using TennisStats.Model;
using TennisStats.src.Controller;
using TennisStats.Service;

namespace TennisStats.adapter
{
    public class MatchStatisticsAdapter : ArrayAdapter<Match>, AdapterView.IOnItemClickListener
    {
        private Context _context;
        private int _resource;
        private List<Match> _matches;
        private ListView _listView;
        private FragmentManager _fragmentManager;
        private FrameLayout _fragmentContainer;

        public MatchStatisticsAdapter(Context context,
            int resource,
            List<Match> matches,
            ListView listView,
            FragmentManager fragmentManager,
            FrameLayout fragmentContainer) 
            : base(context, resource, matches)
        {
            _context = context;
            _resource = resource;
            _matches = matches;
            _listView = listView;
            _fragmentManager = fragmentManager;
            _fragmentContainer = fragmentContainer;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? LayoutInflater.From(_context).Inflate(_resource, null);
            
            MatchController matchController = MatchController.Instance;

            TextView team1 = view.FindViewById<TextView>(Resource.Id.tvTeam1Name);
            TextView team2 = view.FindViewById<TextView>(Resource.Id.tvTeam2Name);

            TextView set1 = view.FindViewById<TextView>(Resource.Id.tvListViewScore);

            Match match = GetItem(position);
            
            team1.Text = match.Team1Id;
            team2.Text = match.Team2Id;

            set1.Text = matchController.GetMatchScore(match);

            _listView.OnItemClickListener = this;
            
            return view;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Console.WriteLine(_matches[position].ToString());
            MatchController.Match = _matches[position];
            NavigationService.NavigateToFragment(_fragmentManager, _fragmentContainer, FragmentMatchStats.NewInstance(null));
        }
    }
}