using System;
using System.Collections.Generic;
using static TennisStats.Enum.SetTypeEnum;

namespace TennisStats.Model
{
    public class Set
    {
        private string _winnerId;
        private List<Game> _games;

        //TODO HUSK AT SÆTTE DEM HER NÅR MAN TILFØJER GAMES
        private int _team1Score;
        private int _team2Score;
        private SetType _setType;

        public Set(){}

        private Set(SetBuilder sb)
        {
            _winnerId = sb._winnerId;
            _games = sb._games;
            _team1Score = sb._team1Score;
            _team2Score = sb._team2Score;
            _setType = sb._setType;
        }

        public string WinnerId { get { return _winnerId; } set { _winnerId = value; } }
        public List<Game> Games { get { return _games; } set { _games = value; } }
        public int Team1Score { get { return _team1Score; } set { _team1Score = value; } }
        public int Team2Score { get { return _team2Score; } set { _team2Score = value; } }
        public SetType SetType { get { return _setType; } set { _setType = value; } }


        public class SetBuilder {
            public string _winnerId;
            public List<Game> _games;
            public int _team1Score;
            public int _team2Score;
            public SetType _setType;

            //Mandatory variables
            public SetBuilder() {
                //initialize list
                _games = new List<Game>();
                _setType = SetType.NORMAL;

            }

            //Optional variables
            public SetBuilder winnderId(string winnderId) { _winnerId = winnderId; return this; }
            public SetBuilder addGame(Game game) { _games.Add(game);  return this; }
            public SetBuilder addScore(int team1Score, int team2Score) { _team1Score = team1Score; _team2Score = team2Score; return this; }
            public SetBuilder setType(SetType setType) { _setType = setType; return this; }

            //Build
            public Set build() { return new Set(this); }
        }

    }
}
