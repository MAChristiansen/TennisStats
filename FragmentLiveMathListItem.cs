
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
using TennisStats.Model;

namespace TennisStats
{
    public class FragmentLiveMathListItem : Fragment
    {

        private Match thisMatch;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.LiveMatchListItem, null);
            return view;
        }

        public List<String> getMatchNames()
        {
            var strings = new List<String>();
            strings.Add("Hans");
            strings.Add("Erik");

            return strings;
        }
        
    }
}
