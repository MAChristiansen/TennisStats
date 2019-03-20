﻿using System;
using System.Collections.Generic;
using static TennisStats.Enum.GameTypeEnum;

namespace TennisStats.Model
{
    public class Game
    {

        private string _winnerId;
        private string _serverId;
        private GameType _gameType;
        private List<int> _team1Score;
        private List<int> _team2Score;
        private List<Point> _points;

        private Game(GameBuilder gb)
        {
            _winnerId = gb._winnerId;
            _serverId = gb._serverId;
            _team1Score = gb._team1Score;
            _team2Score = gb._team2Score;
            _gameType = gb._gameType;
            _points = gb._points;
        }

        public string WinnerId { get { return _winnerId; } set { _winnerId = value; } }
        public string ServerId { get { return _serverId; } set { _serverId = value; } }
        public List<int> Team1Score { get { return _team1Score; } set { _team1Score = value; } }
        public List<int> Team2Score { get { return _team2Score; } set { _team2Score = value; } }
        public List<Point> Points { get { return _points; } set { _points = value; } }
        public GameType GameType { get { return _gameType; } set { _gameType = value; } }

        public int lastScoreTeam1 { get { if (_team1Score.Count > 0) { return _team1Score[_team1Score.Count - 1]; } else return 0; } }
        public int lastScoreTeam2 { get { if (_team2Score.Count > 0) { return _team2Score[_team2Score.Count - 1]; } else return 0; } }


        //Builder pattern
        public class GameBuilder {

            public string _winnerId;
            public string _serverId;

            public List<string> _servers;


            public List<int> _team1Score;
            public List<int> _team2Score;
            public List<Point> _points;
            public GameType _gameType;

            //Mandatory values
            public GameBuilder(string serverId) {
                _serverId = serverId;
                _team1Score = new List<int>();
                _team2Score = new List<int>();
                _points = new List<Point>();
                _gameType = GameType.NORMAL;
            }

            public GameBuilder winnerId(string winnerId) {
                _winnerId = winnerId;
                return this;
            }

            public GameBuilder gameType(GameType gametype)
            {
                _gameType = gametype; return this;
            }

            public GameBuilder addScore(int team1Score, int team2Score, Point point) {
                _team1Score.Add(team1Score);
                _team2Score.Add(team2Score);
                _points.Add(point);
                return this;
            }

            public Game build() {
                return new Game(this);
            }

        }
    }



}
