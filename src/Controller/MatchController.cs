using System;
using System.Collections.Generic;
using TennisStats.Model;
using static TennisStats.Enum.FaultCountEnum;
using static TennisStats.Enum.GameTypeEnum;
using static TennisStats.Enum.MatchParticipantsEnum;
using static TennisStats.Enum.MatchTypeEnum;
using static TennisStats.Enum.ServeStatusEnum;
using static TennisStats.Enum.SetTypeEnum;

namespace TennisStats.src.Controller
{
    public sealed class MatchController : IObservable<Match>
    {
        public static Point.PointBuilder inPlayPB { get; set; }

        private List<IObserver<Match>> matchObservers = new List<IObserver<Match>>();
        private Match currentMatch;
        private Set currentSet;
        private Game currentGame;
        private static MatchController instance = null;
        private static readonly object padlock = new object();

        MatchController(){ inPlayPB = new Point.PointBuilder(); }

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
        public void CreateMatch(string team1Id, string team2Id, MatchParticipants participants, MatchType matchType)
        {
            //TODO generer bedre id
            string matchId = team1Id + team2Id;
            currentMatch = new Match.MatchBuilder(matchId, team1Id, team2Id, participants).matchType(matchType).build();
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
            pb.winnderId(currentGame.Servers[currentGame.Servers.Count-1]);
            pb.serveStatus(ServeStatus.ACE);
            Point p = pb.build();

            // If the server was team 1, add the point to him, else add to team 2
            if (currentGame.Servers[currentGame.Servers.Count - 1].Equals(currentMatch.Team1Id))
            {
                GivePointToTeam(currentMatch.Team1Id);
            }
            else
            {
                GivePointToTeam(currentMatch.Team2Id);
            }

            //Add the point to the game.
            currentGame.Points.Add(p);

            changeServer();

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
            if (currentGame.Servers[currentGame.Servers.Count - 1].Equals(currentMatch.Team1Id))
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

            changeServer();

            return currentFaultCount;
        }


        public void inPlay()
        {
            // Build the point from the point builder
            Point point = inPlayPB.build();

            //Reset the point builder
            inPlayPB = new Point.PointBuilder();

            // Give points to the winner team
            GivePointToTeam(point.WinnerId);
            currentGame.Points.Add(point);

            //Notify observers
            updateObservers();

            //CHanger server if tiebreak
            changeServer();
        }

        /*
         *   Checks if the game is tiebreak,
         *   if so, change server accordingly.        
         */
        private void changeServer()
        {
            // Never change the server, if the gametype is normal
            if (currentGame.GameType == GameType.NORMAL) return;

            string currentServer = currentGame.Servers[currentGame.Servers.Count-1];

            if (currentGame.Servers.Count % 2 == 1)
            {
                //Change the server
                if (currentServer.Equals(currentMatch.Team1Id)){
                    //team 2 should serve now
                    currentGame.Servers.Add(currentMatch.Team2Id);
                }
                else
                {
                    currentGame.Servers.Add(currentMatch.Team1Id);
                }
            }
            else
            {
                currentGame.Servers.Add(currentServer);
            }
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

        public GameType getCurrentGameType()
        {
            return currentGame.GameType;
        }

        public Game getCurrentGame()
        {
            return currentGame;
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

        /*
         *   Helping method used to notify all observers
         * 
         */        
        private void updateObservers()
        {
            //Update game status before notifying observers
            updateGameStatus();

            // If the game is not finished, notify with OnNext
            foreach (IObserver<Match> observer in matchObservers)
            {
                observer.OnNext(currentMatch);
            }

            // If the game is finished, notify with OnCompleted
            //foreach (IObserver<Match> observer in matchObservers)
            //{
            //    observer.OnComplete(currentMatch);
            //}
        }

        private void updateGameStatus()
        {
            /*
             *   Checking if someone has won the current game:
             * 
             *   If one of the teams has more than 3 points:
             *   - Is the absolute value from the subtraction,
             *     of the team scores more/equal than 2
             *   - Who has the more points wins
             *   
             */
            int minimumScore = 3;
            if (currentGame.GameType == Enum.GameTypeEnum.GameType.TIEBREAK) minimumScore = 6;

                if (currentGame.lastScoreTeam1 > minimumScore || currentGame.lastScoreTeam2 > minimumScore)
                {

                    if (Math.Abs(currentGame.lastScoreTeam1 - currentGame.lastScoreTeam2) >= 2)
                    {
                        if (currentGame.lastScoreTeam1 > currentGame.lastScoreTeam2)
                        {
                            //Team 1 has won this game
                            registerGameWinner(currentMatch.Team1Id);
                        }
                        else
                        {
                            //Team 2 has won this game
                            registerGameWinner(currentMatch.Team2Id);
                        }
                    }
                }

            /*
             *   Checking if someone has won the current set:
             * 
             *   If one of the teams has more than 5 points:
             *   - Is the absolute value from the subtraction,
             *     of the team scores more/equal than 2          
             *   
             */
            if (currentSet.Team1Score > 5 || currentSet.Team2Score > 5)
            {
                if (Math.Abs(currentSet.Team1Score - currentSet.Team2Score) >= 2 ||
                (currentSet.Team1Score==6 && currentSet.Team2Score==7) ||
                (currentSet.Team1Score==7 && currentSet.Team2Score==6))
                {

                    if (currentSet.Team1Score > currentSet.Team2Score)
                    {
                        // Team1 wins
                        registerSetWinner(currentMatch.Team1Id);
                    }
                    else
                    {
                        // team2 wins
                        registerSetWinner(currentMatch.Team2Id);
                    }
                }
            }

            // Has somebody won the match?

            switch (currentMatch.Type)
            {
                case MatchType.ONESETTER:
                    if (currentMatch.Team1Score > 0)
                    {
                        //Team 1 wins
                        matchObservers[0].OnCompleted();
                    }
                    else if (currentMatch.Team2Score > 0)
                    {
                        //Team 2 wins
                        matchObservers[0].OnCompleted();
                    }
                    break;
                case MatchType.THREESETTER:
                    if (currentMatch.Team1Score >= 2)
                    {
                        //Team 1 wins
                        matchObservers[0].OnCompleted();
                    }
                    else if (currentMatch.Team2Score >= 2)
                    {
                        //team 2 wins
                        matchObservers[0].OnCompleted();
                    }
                    break;
                case MatchType.FIVESETTER:
                    if (currentMatch.Team1Score >= 3)
                    {
                        //Team 1 wins
                        matchObservers[0].OnCompleted();
                    }
                    else if (currentMatch.Team2Score >= 3)
                    {
                        //team 2 wins
                        matchObservers[0].OnCompleted();
                    }
                    break;
            }
        }
        /*             
         *   Following is executed when registering a winner:
         *   - Set the winner id of current game
         *   - Add the finished game to the current set
         *   - Add a point to the winner in the set
         *   - Create a new game
         *   - find the new server of that game 
         */            
        private void registerGameWinner(string winnerId)
        {
            // Register the winner of the current game
            currentGame.WinnerId = winnerId;
            currentSet.Games.Add(currentGame);

            // Give a point to the right team
            if (winnerId.Equals(currentMatch.Team1Id))
            {
                currentSet.Team1Score += 1;
            }
            else
            {
                currentSet.Team2Score += 1;
            }


            // Create a new game
            string newServer;

            if (currentGame.GameType == GameType.NORMAL)
            {
                if (currentGame.Servers[currentGame.Servers.Count - 1].Equals(currentMatch.Team1Id))
                {
                    newServer = currentMatch.Team2Id;
                }
                else
                {
                    newServer = currentMatch.Team1Id;
                }
            }
            else
            {
                if (currentGame.Servers[0].Equals(currentMatch.Team1Id))
                {
                    newServer = currentMatch.Team2Id;
                }
                else
                {
                    newServer = currentMatch.Team1Id;
                }

            }


            // Checking whether the new game should be a normal or tiebreak
            if (currentSet.Team1Score == 6 && currentSet.Team2Score == 6)
            {
                currentGame = new Game.GameBuilder(newServer).gameType(GameType.TIEBREAK).build();
            }
            else
            {
                currentGame = new Game.GameBuilder(newServer).build();
            }

        }

        /*
         *   Service method used to register the winner of a set
         */        
        private void registerSetWinner(string winnerId)
        {
            //Set the winner of the set, and add it to the match
            currentSet.WinnerId = winnerId;
            currentMatch.Sets.Add(currentSet);

            // Give a point to the right team
            if (winnerId.Equals(currentMatch.Team1Id))
            {
                currentMatch.Team1Score += 1;
            }
            else
            {
                currentMatch.Team2Score += 1;
            }

            //Create a new current set
            currentSet = new Set.SetBuilder().build();
        }
    }
}
