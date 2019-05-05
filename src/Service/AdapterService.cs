
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace TennisStats.src.Service
{
    public class AdapterService : ListFragment
    {
        public static void createAdapter(Context context)
        {
            var listAdapter = new ArrayAdapter<string>(context, Resource.Layout.Layout, countries);
        }

    }
}
