using System;
using System.Collections.Generic;
using TennisStats.Model;
using static TennisStats.Enum.FaultCountEnum;
using static TennisStats.Enum.MatchParticipantsEnum;
using static TennisStats.Enum.ServeStatusEnum;

namespace TennisStats.src.Controller
{
    public sealed class MatchController
    {
        private Match currentMatch;
        private Set currentSet;
        private Game currentGame;
        public static MatchController instance = null;
        private static readonly object padlock = new object();

        MatchController(){}

        public static MatchController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MatchController();
                    }
                    return instance;
                }
            }
        }


        /*
         *  Creates a new match.
         *  Instantializes a match, set and game.        
         */
        public void CreateMatch(string team1Id, string team2Id, MatchParticipants participants)
        {
            //TODO generer bedre id
            string matchId = team1Id + team2Id;
            currentMatch = new Match.MatchBuilder(matchId, team1Id, team2Id, participants).build();
            currentSet = new Set.SetBuilder().build();
            //TODO Hvem skal starte med serven?
            currentGame = new Game.GameBuilder(team1Id).build();

            Console.WriteLine("Match created: {0}", matchId);
        }

        /*
         * 
         *  This method is handles the "ace"-action
         * 
         */
        public void Ace()
        {
            //Create point descriping the action
            Point.PointBuilder pb = new Point.PointBuilder();
            pb.winnderId(currentGame.ServerId);
            pb.serveStatus(ServeStatus.ACE);
            Point p = pb.build();

            // If the server was team 1, add the point to him, else add to team 2
            if (currentGame.ServerId.Equals(currentMatch.Team1Id))
            {
                GivePointToTeam(currentMatch.Team1Id);
            }
            else
            {
                GivePointToTeam(currentMatch.Team2Id);
            }

            Console.WriteLine("ACE!! Server: {0}, team1: {1}, team2: {2}", currentGame.ServerId, currentGame.lastScoreTeam1, currentGame.lastScoreTeam2);

            //Add the point to the game.
            currentGame.Points.Add(p);
        }

        /*
         * 
         *  This method is handles the "fault"-action
         * 
         */
        public void Fault()
        {
            FaultCount currentFaultCount = findFaultCount();
            //Create point descriping the action
            Point.PointBuilder pb = new Point.PointBuilder();

            //Set the serve status to fault
            pb.serveStatus(ServeStatus.FAULT);

            //Check if first serve
            if (currentFaultCount==FaultCount.FIRSTSERVE)
            {
                pb.faultCount(FaultCount.FIRSTSERVE);

                GiveEmptyPoints();
                currentGame.Points.Add(pb.build());
                return;
            }


            // If it is a second serve we run this code!
            pb.faultCount(FaultCount.SECONDSERVE);

            // Find the winner and give him point
            if (currentGame.ServerId.Equals(currentMatch.Team1Id))
            {
                pb.winnderId(currentMatch.Team2Id);
                GivePointToTeam(currentMatch.Team2Id);
            }
            else
            {
                pb.winnderId(currentMatch.Team1Id);
                GivePointToTeam(currentMatch.Team1Id);
            }

            //Add the point to the game
            currentGame.Points.Add(pb.build());
        }


        /*
         *   Get the current score of the current set.
         * 
         */
        public List<int> GetCurrentMatchScore()
        {
            List<int> currentScore = new List<int>();
            currentScore.Add(currentMatch.Team1Score);
            currentScore.Add(currentMatch.Team2Score);
            return currentScore;
        }

        /*
         *   Get the current score of the current set.
         * 
         */
        public List<int> GetCurrentSetScore()
        {
            List<int> currentScore = new List<int>();
            currentScore.Add(currentSet.Team1Score);
            currentScore.Add(currentSet.Team2Score);
            return currentScore;
        }

        /*
         *   Get the current score of the current game.
         * 
         */        
        public List<int> GetCurrentGameScore()
        {
            List<int> currentScore = new List<int>();
            currentScore.Add(currentGame.lastScoreTeam1);
            currentScore.Add(currentGame.lastScoreTeam2);
            return currentScore;
        }



        /*
         *  Helping method to provide points to teams.
         * 
         */        
         private void GivePointToTeam(string winnerId)
        {
            if (winnerId.Equals(currentMatch.Team1Id))
            {
                currentGame.Team1Score.Add(currentGame.lastScoreTeam1 + 1);
                currentGame.Team2Score.Add(currentGame.lastScoreTeam2);
            }
            else
            {
                currentGame.Team1Score.Add(currentGame.lastScoreTeam1 + 1);
                currentGame.Team2Score.Add(currentGame.lastScoreTeam2);
            }
        }


        /*
         *   Service method to add empty points to both teams
         */
        private void GiveEmptyPoints()
        {
            currentGame.Team1Score.Add(currentGame.lastScoreTeam1);
            currentGame.Team2Score.Add(currentGame.lastScoreTeam2);
        }

        /*
         *   Service method that find out it is a first serve or second serve
         */
        private FaultCount findFaultCount()
        {
            //Check if last point was a firste serve point.
            if (currentGame.Points.Count > 0)
            {
                if (currentGame.Points[currentGame.Points.Count - 1].FaultCount == FaultCount.FIRSTSERVE)
                {
                    return FaultCount.SECONDSERVE;
                }
            }

            return FaultCount.FIRSTSERVE;
        }
    }
}
