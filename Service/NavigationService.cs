using System;
using Android.App;
using Android.Content;

namespace TennisStats.Service
{
    public class NavigationService
    {
        public static void NavigateToPage(Context context, Type activity) {

            Intent intent = new Intent(context, activity);
            context.StartActivity(intent);
        }

    }
}
