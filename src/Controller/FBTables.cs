using Firebase.Database;
using Firebase.Database.Query;

namespace TennisStats.src.Controller
{
    public static class FBTables
    {
        private const string User = "user";

        public static FirebaseClient FirebaseClient { get; } = new FirebaseClient("https://tennisstats-7926f.firebaseio.com/");

        public static string FbUser => User;
    }
}