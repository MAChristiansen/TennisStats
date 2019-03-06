using Android.App;
using Android.OS;
using Android.Views;

namespace TennisStats.view
{
    public class FragmentMatchSetupTeam : Fragment
    {

        public static FragmentMatchSetupTeam NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentMatchSetupTeam();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.MatchSetupTeam, container, false);

            return view;
        }
    }
}
