using System;
using Android.App;
using Android.OS;
using Android.Views;

namespace TennisStats
{
    public class FragmentMatchSetupCategory : Fragment
    {
        public static FragmentMatchSetupCategory NewInstance() 
        {
            var bundle = new Bundle();
            return new FragmentMatchSetupCategory(); 
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.MatchSetupCategory, container, false);
            Console.WriteLine("Jeg er her!!");
            return view;
        }
    }
}
