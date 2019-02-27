using System;
namespace Tennistats.Model
{
    public class Club
    {

        private string _clubId;
        private string _name;

        private Club(ClubBuilder clubBuilder)
        {
            _clubId = clubBuilder._clubId;
            _name = clubBuilder._name;

        }

        public string ClubId { get { return _clubId; } set { _clubId = value; } }
        public string Name { get { return _name; } set { _name = value; } }


        //Builder pattern
        class ClubBuilder {

            public string _clubId { get; }
            public string _name { get; }

            public ClubBuilder(string clubId, string name) {
                _clubId = clubId;
                _name = name;
            }

            public Club build() {
                return new Club(this);
            }


        }
    }
}
