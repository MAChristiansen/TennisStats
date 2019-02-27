using System;
using static Tennistats.Enum.GenderEnum;
using static Tennistats.Enum.HandEnum;

namespace Tennistats.Model
{
    public class Player
    {
        private string _playerId;
        private string _name;
        private string _clubId;
        private Hand _playingHand;
        private Gender _gender;
        private long _birthday;

        public Player() { }
        private Player(PlayerBuilder pb) {
            _playerId = pb._playerId;
            _name = pb._name;
            _clubId = pb._clubId;
            _playingHand = pb._playingHand;
            _gender = pb._gender;
            _birthday = pb._birthday;
        }

        public string PlayerId { get { return _playerId; } set { _playerId = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string ClubId { get { return _clubId; } set { _clubId = value; } }
        public long Birthday { get { return _birthday; } set{ _birthday = value; } }
        public Hand PlayingHand { get { return _playingHand; } set { _playingHand = value; } }
        public Gender Gender { get { return _gender; } set { _gender = value; } }

        //ToString Method
        override
        public string ToString()
        {
            return "ID: " + _playerId +
                "\nName: " + _name +
                "\nClubId: " + _clubId +
                "\nBirthday: " + _birthday +
                "\nHand: " + _playingHand +
                "\nGender: " + _gender;
        }

        //Builder pattern
        public class PlayerBuilder {
            public string _playerId;
            public string _name;
            public string _clubId;
            public Hand _playingHand;
            public Gender _gender;
            public long _birthday;

            //Mandatory variables
            public PlayerBuilder(string playerId, string name, string clubId, Hand playingHand, Gender gender, long birthday) {
                _playerId = playerId;
                _name = name;
                _clubId = clubId;
                _playingHand = playingHand;
                _gender = gender;
                _birthday = birthday;
            }

            //Build
            public Player build() {
                return new Player(this);
            }
        }
    }
}
