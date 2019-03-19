using System;
using System.Collections.Generic;
using TennisStats.Model;
using static TennisStats.Enum.FaultCountEnum;
using static TennisStats.Enum.MatchParticipantsEnum;
using static TennisStats.Enum.ServeStatusEnum;

namespace TennisStats.src.Controller
{
    public sealed class MatchController : IObservable<Match>
    {
        private List<IObserver<Match>> matchObservers = new List<IObserver<Match>>();

        private Match currentMatch;
        private Set currentSet;
        private Game currentGame;
        private static MatchController instance = null;
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

            // Update the observers
            updateObservers();
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

            //Add the point to the game.
            currentGame.Points.Add(p);

            //Update the observers
            updateObservers();
        }

        /*
         * 
         *  This method is handles the "fault"-action
         * 
         */
        public FaultCount Fault()
        {
            FaultCount currentFaultCount = findFaultCount();
            //Create point descriping the action
            Point.PointBuilder pb = new Point.PointBuilder();

            //Set the serve status to fault
            pb.serveStatus(ServeStatus.FAULT);

            //Check if first serve
            if (currentFaultCount == FaultCount.FIRSTSERVE)
            {
                pb.faultCount(FaultCount.FIRSTSERVE);

                GiveEmptyPoints();
                currentGame.Points.Add(pb.build());
                return currentFaultCount;
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

            //Update the observers
            updateObservers();

            return currentFaultCount;
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
         *   Get the current team names.
         * 
         *   First entry in the list is team1
         *   Second entry in the list is team2        
         * 
         */        
         public List<string> GetTeamNames()
        {
            List<string> teamNames = new List<string>();
            teamNames.Add(currentMatch.Team1Id);
            teamNames.Add(currentMatch.Team2Id);
            return teamNames;
        }

        /*
         *   Subscribe method used by the observerpattern
         * 
         */        
        public IDisposable Subscribe(IObserver<Match> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!matchObservers.Contains(observer))
            {
                matchObservers.Add(observer);
                // Provide observer with existing data.
                observer.OnNext(currentMatch);
            }
            return new Unsubscriber<Match>(matchObservers, observer);
        }


        /*
         *  Helping method to provide points to teams.
         * 
         */
        private void GivePointToTeam(string winnerId)
        {
            // Check on which team the winner is, and give points accordingly
            if (winnerId.Equals(currentMatch.Team1Id))
            {
                currentGame.Team1Score.Add(currentGame.lastScoreTeam1 + 1);
                currentGame.Team2Score.Add(currentGame.lastScoreTeam2);
            }
            else
            {
                currentGame.Team1Score.Add(currentGame.lastScoreTeam1);
                currentGame.Team2Score.Add(currentGame.lastScoreTeam2 + 1);
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
         * 
         *   - If it is the first point given, it is a first serve
         *   - If the former point (point.count - 1) was a first serve, the current is a secondserve
         *   - If the former was a defaultserve (neither first, nor second serve) the current is firstserve
         */
        private FaultCount findFaultCount()
        {

            if (currentGame.Points.Count > 0)
            {
                if (currentGame.Points[currentGame.Points.Count - 1].FaultCount == FaultCount.FIRSTSERVE)
                {
                    return FaultCount.SECONDSERVE;
                }
            }

            return FaultCount.FIRSTSERVE;
        }

        private void updateObservers()
        {
            foreach (IObserver<Match> observer in matchObservers)
            {
                observer.OnNext(currentMatch);
            }
        }
    }
}
