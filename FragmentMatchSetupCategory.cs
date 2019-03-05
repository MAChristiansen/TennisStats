
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

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

            Spinner sCategory = view.FindViewById<Spinner>(Resource.Id.sCategory);
            Spinner sForm = view.FindViewById<Spinner>(Resource.Id.sForm);
            Spinner sType = view.FindViewById<Spinner>(Resource.Id.sType);
            
            //Match Category spinner
            sCategory.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
            var adapterCategory = ArrayAdapter.CreateFromResource (
              Activity, Resource.Array.spinner_Match_Category, Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sCategory.Adapter = adapterCategory;
            
            //Match Form spinner
            sForm.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
            var adapterForm = ArrayAdapter.CreateFromResource (
                Activity, Resource.Array.spinner_Match_Form, Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sForm.Adapter = adapterForm;
            
            //Match Type spinner
            sType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
            var adapterType = ArrayAdapter.CreateFromResource (
                Activity, Resource.Array.spinner_Match_Type, Android.Resource.Layout.SimpleSpinnerItem);
            adapterCategory.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sType.Adapter = adapterType;
            
            return view;
        }
        
        private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format ("The Category is {0}", spinner.GetItemAtPosition (e.Position));
            Toast.MakeText (Activity, toast, ToastLength.Long).Show ();
        }
    }
}
