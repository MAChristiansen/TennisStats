using System;
using System.Collections.Generic;

namespace TennisStats.Model
{
    public class Team
    {
        // teamId is concat by the one/two players
        private string _teamId;

        public Team(string teamId)
        {
            _teamId = teamId;
        }

        public string TeamId { get { return _teamId; } set { _teamId = value; } }

    }
}
