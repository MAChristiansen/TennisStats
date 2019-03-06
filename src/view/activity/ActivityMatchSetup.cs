using Android.App;
using Android.OS;
using TennisStats.control;

namespace TennisStats.view
{
    [Activity(Label = "MatchSetup")]
    public class ActivityMatchSetup : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MatchSetup);
            
            if (savedInstanceState == null) {
               
                NavigationService.AddFragment(FragmentManager, 
                    FindViewById(Resource.Id.fragmentContainer),
                    FragmentMatchSetupCategory.NewInstance());
            }
        }
    }
}
