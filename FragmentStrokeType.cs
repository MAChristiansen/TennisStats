
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
    public class FragmentStrokeType : Fragment
    {

        public static FragmentStrokeType NewInstance()
        {
            Bundle bundle = new Bundle();
            
            return new FragmentStrokeType();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StrokeType, container, false);
            
            
            
            return view;
        }
    }
}
