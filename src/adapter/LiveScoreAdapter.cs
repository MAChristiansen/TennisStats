using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using TennisStats.Model;
using TennisStats.src.Service;

namespace TennisStats.adapter
{
    public class LiveScoreAdapter : ArrayAdapter<Match>
    {
        private Context _context;
        private int _resource;
        private List<Match> _matches;
        private PointService _pointService;

        public LiveScoreAdapter(Context context, int resource, List<Match> matches) : base(context, resource, matches)
        {
            _context = context;
            _resource = resource;
            _matches = matches;
            _pointService = PointService.Instance;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? LayoutInflater.From(_context).Inflate(_resource, null);

            TextView team1 = view.FindViewById<TextView>(Resource.Id.tvTeam1Name);
            TextView team2 = view.FindViewById<TextView>(Resource.Id.tvTeam2Name);

            TextView set1 = view.FindViewById<TextView>(Resource.Id.tvTeam1Set);
            TextView set2 = view.FindViewById<TextView>(Resource.Id.tvTeam2Set);

            TextView game1 = view.FindViewById<TextView>(Resource.Id.tvTeam1Game);
            TextView game2 = view.FindViewById<TextView>(Resource.Id.tvTeam2Game);

            TextView point1 = view.FindViewById<TextView>(Resource.Id.tvTeam1Point);
            TextView point2 = view.FindViewById<TextView>(Resource.Id.tvTeam2Point);

            Match match = GetItem(position);
            
            team1.Text = match.Team1Id;
            team2.Text = match.Team2Id;

            set1.Text = match.Team1Score.ToString();
            set2.Text = match.Team2Score.ToString();

            game1.Text = match.Sets[match.Sets.Count - 1].Team1Score.ToString();
            game2.Text = match.Sets[match.Sets.Count - 1].Team2Score.ToString();

            int point1ForConversion = match.Sets[match.Sets.Count - 1].Games[match.Sets[match.Sets.Count - 1].Games.Count - 1].lastScoreTeam1;
            int point2ForConversion = match.Sets[match.Sets.Count - 1].Games[match.Sets[match.Sets.Count - 1].Games.Count - 1].lastScoreTeam2;
            var gameType = match.Sets[match.Sets.Count - 1].Games[match.Sets[match.Sets.Count - 1].Games.Count - 1].GameType;

            point1.Text = _pointService.convertPoints(point1ForConversion, point2ForConversion, gameType);
            point2.Text = _pointService.convertPoints(point2ForConversion, point1ForConversion, gameType);
            
            return view;
        }
    }
}