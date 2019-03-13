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
                fragmentManager.BeginTransaction()
                                .SetTransition(FragmentTransit.FragmentFade)
                                .AddToBackStack(tag)
                                .Replace(container.Id, newFragment, tag)
                                .Commit();
                return;
            }

            fragmentManager.BeginTransaction()
                            .SetTransition(FragmentTransit.FragmentFade)
                            .AddToBackStack(null)
                            .Replace(container.Id, newFragment, tag)
                            .Commit();
        }

        public static void AddFragment(FragmentManager fragmentManager, View container, Fragment newFragment, string tag = null)
        {
            if (tag != null)
            {
                fragmentManager.BeginTransaction()
                    .Add(container.Id, newFragment, tag)
                    .Commit();
                return;
            }
            
            fragmentManager.BeginTransaction()
                .Add(container.Id, newFragment, tag)
                .Commit();
        }
    }
}
