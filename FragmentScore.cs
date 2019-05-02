using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using TennisStats.src.Controller;
using TennisStats.src.Service;

namespace TennisStats
{
    public class FragmentScore : Fragment
    {
        public static FragmentScore NewInstance(Bundle bundle)
        {
            FragmentScore fragmentScore = new FragmentScore();
            fragmentScore.Arguments = bundle;

            return fragmentScore;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Score, container, false);
            MatchController matchController = MatchController.Instance;

            if (matchController.GetCurrentMatch().EndTime != 0)
            {
               //TODO Gør noget når kampen er færdig.
            }
            
            TextView tvScore = view.FindViewById<TextView>(Resource.Id.tvScore);
            int team1Score = Arguments.GetInt("team1", 0);
            int team2Score = Arguments.GetInt("team2", 0);
            tvScore.Text = PointService.Instance.convertPoints(team1Score, team2Score, matchController.getCurrentGameType()) + " - " + PointService.Instance.convertPoints(team2Score, team1Score, matchController.getCurrentGameType());
            Task.Delay(1000).ContinueWith(t=> FragmentManager.PopBackStack(null, PopBackStackFlags.Inclusive));



            return view;
        }
    }
}
