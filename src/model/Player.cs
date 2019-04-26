using System;
using TennisStats.Enum;
using static TennisStats.Enum.GenderEnum;
using static TennisStats.Enum.HandEnum;

namespace TennisStats.Model
{
    public class Player
    {
        private string _playerId;
        private string _password;
        private string _name;
        private string _clubId;
        private Hand _playingHand;
        private Gender _gender;
        private long _birthday;

        public Player() { }
        private Player(PlayerBuilder pb) {
            _playerId = pb._playerId;
            _password = pb._password;
            _name = pb._name;
            _clubId = pb._clubId;
            _playingHand = pb._playingHand;
            _gender = pb._gender;
            _birthday = pb._birthday;
        }

        public string PlayerId { get { return _playerId; } set { _playerId = value; } }

        public string Password { get { return _password; } set { _password = value; } }
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
            public string _password;
            public string _name;
            public string _clubId;
            public Hand _playingHand;
            public Gender _gender;
            public long _birthday;

            //Mandatory variables
            public PlayerBuilder(string playerId) {
                _playerId = playerId;
            }

            public PlayerBuilder Password(string password)
            {
                _password = password;
                return this;
            }

            public PlayerBuilder Name(string name)
            {
                _name = name;
                return this;
            }

            public PlayerBuilder ClubId(string clubId)
            {
                _clubId = clubId;
                return this;
            }

            public PlayerBuilder Hand(bool hand)
            {
                _playingHand = hand ? HandEnum.Hand.LEFT : HandEnum.Hand.RIGHT;
                return this;
            }

            public PlayerBuilder Gender(bool gender)
            {
                _gender = gender ? GenderEnum.Gender.FEMALE : GenderEnum.Gender.MALE;
                return this;
            }

            public PlayerBuilder Birthday(long timestamp)
            {
                _birthday = timestamp;
                return this;
            }

            //Build
            public Player build() {
                return new Player(this);
            }
        }
    }
}
