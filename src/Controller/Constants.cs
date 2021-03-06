using Firebase.Database;
using Firebase.Database.Query;

namespace TennisStats.src.Controller
{
    public static class Constants
    {
        public static FirebaseClient FirebaseClient { get; } = new FirebaseClient("https://tennisstats-7926f.firebaseio.com/");

        public static string FbUser => "user";
        public static string FbPassword => "Password";
        public static string FbBirthday => "Birthday";
        public static string FBMatch => "match";
        public static string FbClub => "club";
        public static string FbClubId => "ClubId";
        public static string FbMatch => "match";
        public static string UserId => "userId";
        public static string Default => "default";
        public static string StatType => "statType";
    }
}