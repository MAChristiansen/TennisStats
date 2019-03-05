using System;
using Android.App;
using Android.Content;
using Android.Views;

namespace TennisStats.Service
{
    public class NavigationService
    {
        public static void NavigateToPage(Context context, Type activity) {

            Intent intent = new Intent(context, activity);
            context.StartActivity(intent);
        }

        public static void NavigateToFragment(FragmentManager fragmentManager, View container, Fragment newFragment, string tag = null)
        {
            if (tag != null)
            {
                fragmentManager.BeginTransaction().Replace(container.Id, newFragment, tag).Commit();
                return;
            }

            fragmentManager.BeginTransaction().Replace(container.Id, newFragment).Commit();
        }
    }
}
