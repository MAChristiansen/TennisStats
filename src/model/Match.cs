using System.Collections.Generic;
using static TennisStats.Enum.MatchTypeEnum;
using static TennisStats.Enum.MatchParticipantsEnum;
namespace TennisStats.Model
{
    public class Match
    {
        private string _matchId;
        private string _team1Id;
        private string _team2Id;
        private string _location;
        private string _court;
        private string _winnerId;

        private long _startTime;
        private long _endTime;

        //TODO HUSK AT SÆTTE DEN HER NÅR MAN TILFØJER SETS!!
        private int _team1score;
        private int _team2score;

        private MatchType _type;
        private MatchParticipants _participants;

        private List<Set> _sets;

        public Match()
        {}

        private Match(MatchBuilder mb)
        {
            _matchId = mb._matchId;
            _team1Id = mb._team1Id;
            _team2Id = mb._team2Id;
            _location = mb._location;
            _court = mb._court;
            _winnerId = mb._winnerId;
            _startTime = mb._startTime;
            _endTime = mb._endTime;
            _type = mb._matchType;
            _participants = mb._participants;
            _sets = mb._sets;
        }

        public string MatchId { get { return _matchId; } set { _matchId = value; } }
        public string Team1Id { get { return _team1Id; } set { _team1Id = value; } }
        public string Team2Id { get { return _team2Id; } set { _team2Id = value; } }
        public string Location { get { return _location; } set { _location = value; } }
        public string Court { get { return _court; } set { _court = value; } }
        public string WinnerId { get { return _winnerId; } set { _winnerId = value; } }
        public long StartTime { get { return _startTime; } set { _startTime = value; } }
        public long EndTime { get { return _endTime; } set { _endTime = value; } }
        public int Team1Score { get { return _team1score; } set { _team1score = value; } }
        public int Team2Score { get { return _team2score; } set { _team2score = value; } }
        public MatchType Type { get { return _type; } set { _type = value; } }
        public MatchParticipants Participants { get { return _participants; } set { _participants = value; } }
        public List<Set> Sets { get { return _sets; } set { _sets = value; } }

        public class MatchBuilder {
            public string _matchId;
            public string _team1Id;
            public string _team2Id;
            public string _location;
            public string _court;
            public string _winnerId;

            public long _startTime;
            public long _endTime;

            public MatchType _matchType;
            public MatchParticipants _participants;

            public List<Set> _sets;

            //Mandatory variables
            public MatchBuilder(string matchId, string team1Id, string team2Id, MatchParticipants participants) {
                _matchId = matchId;
                _team1Id = team1Id;
                _team2Id = team2Id;
                _endTime = 0;
                _participants = participants;

                //Initialize list
                _sets = new List<Set>();
            }

            //Nonmandatory variables
            public MatchBuilder location(string location) {
                _location = location;
                return this;
            }

            public MatchBuilder court(string court) {
                _court = court;
                return this;
            }

            public MatchBuilder winnerId(string winnerId) {
                _winnerId = winnerId;
                return this;
            }

            public MatchBuilder startTime(long startTime) {
                _startTime = startTime;
                return this;
            }

            public MatchBuilder endTime(long endTime) {
                _endTime = endTime;
                return this;
            }

            public MatchBuilder matchType(MatchType matchType) {
                _matchType = matchType;
                return this;
            }

            public MatchBuilder addSet(Set set) {
                _sets.Add(set);
                return this;
            }

            //Building the match
            public Match build() {
                return new Match(this);
            }
        }
    }
}
