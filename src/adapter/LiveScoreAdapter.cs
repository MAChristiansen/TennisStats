using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using TennisStats.Model;

namespace TennisStats.adapter
{
    public class LiveScoreAdapter : ArrayAdapter<Match>
    {
        private Context _context;
        private int _resource;
        private List<Match> _matches;


        public LiveScoreAdapter(Context context, int resource, List<Match> matches) : base(context, resource, matches)
        {
            _context = context;
            _resource = resource;
            _matches = matches;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? LayoutInflater.From(_context).Inflate(_resource, null);

            TextView team1 = view.FindViewById<TextView>(Resource.Id.tvTeam1Name);
            TextView team2 = view.FindViewById<TextView>(Resource.Id.tvTeam2Name);

            Match match = GetItem(position);
            
            team1.Text = match.Team1Id;
            team2.Text = match.Team2Id;

            return view;
        }
    }
}