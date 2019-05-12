using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using TennisStats.Model;
using static TennisStats.Enum.FaultCountEnum;
using static TennisStats.Enum.GameTypeEnum;
using static TennisStats.Enum.MatchParticipantsEnum;
using static TennisStats.Enum.MatchTypeEnum;
using static TennisStats.Enum.ServeStatusEnum;

namespace TennisStats.src.Controller
{
    public sealed class MatchController : IObservable<Match>
    {
        public static Match Match { get; set; }
        public static Point.PointBuilder inPlayPB { get; set; }

        private List<IObserver<Match>> matchObservers = new List<IObserver<Match>>();
        private Match currentMatch { get; set; }
        private Set currentSet;
        private Game currentGame;
        private static MatchController instance;
        private static readonly object padlock = new object();

        MatchController(){ inPlayPB = new Point.PointBuilder(); }

        public static MatchController Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ?? (instance = new MatchController());
                }
            }
        }


        /*
         *  Creates a new match.
         *  Instantializes a match, set and game.
         */
        public void CreateMatch(string team1Id, string team2Id, MatchParticipants participants, MatchType matchType, bool server)
        {
            string matchId = team1Id + "||"+ team2Id + "||" + Util.GenerateRamdom6DNumber();
            currentMatch = new Match.MatchBuilder(matchId, team1Id, team2Id, participants).matchType(matchType).startTime(Util.GenerateTimeStamp()).build();
            currentSet = new Set.SetBuilder().build();
            currentMatch.Sets.Add(currentSet);
            currentGame = new Game.GameBuilder(server ? team2Id : team1Id).build();
            currentSet.Games.Add(currentGame);

            // Update the observers
            updateObservers(currentMatch, currentSet, currentGame);
        }

        /*
         *
         *  This method is handles the "ace"-action
         *
         */
        public void Ace(Match match, Game game, int serve, bool ace = true)
        {
            //Creating point
            Point.PointBuilder pb = new Point.PointBuilder();
            pb.serverId(game.Servers[game.Servers.Count - 1]);
            pb.winnderId(game.Servers[game.Servers.Count - 1]);
            pb.serveStatus(ace ? ServeStatus.ACE : ServeStatus.SERVEWINNER);
            pb.faultCount(serve == 1 ? FaultCount.FIRSTSERVE : FaultCount.SECONDSERVE);

            //building the current point
            Point p = pb.build();

            // If the server was team 1, add the point to him, else add to team 2
            GivePointToTeam(currentMatch, currentGame, game.Servers[game.Servers.Count - 1].Equals(match.Team1Id) ? match.Team1Id : match.Team2Id);

            //Add the point to the game.
            game.Points.Add(p);

            ChangeServer(currentMatch, currentGame);

            //Update the observers
            updateObservers(currentMatch, currentSet, currentGame);
        }

        /*
         *
         *  This method is handles the "fault"-action
         *
         */
        public FaultCount Fault(Match match, Game game, int serve, bool fault = true)
        {
            //Creating point
            Point.PointBuilder pb = new Point.PointBuilder();
            pb.serverId(game.Servers[game.Servers.Count - 1]);
            pb.faultCount(serve == 1 ? FaultCount.FIRSTSERVE : FaultCount.SECONDSERVE);

            //Determine which type of fault it is
            pb.serveStatus(fault ? ServeStatus.FAULT : ServeStatus.FOOTFAULT);

            //Check if first serve
            if (pb._faultCount == FaultCount.FIRSTSERVE)
            {
                GiveEmptyPoints(currentGame);
                game.Points.Add(pb.build());
                return pb._faultCount;
            }

            // Find the winner and give point
            if (game.Servers[currentGame.Servers.Count - 1].Equals(match.Team1Id))
            {
                pb.winnderId(match.Team2Id);
                GivePointToTeam(currentMatch, currentGame, match.Team2Id);
            }
            else
            {
                pb.winnderId(match.Team1Id);
                GivePointToTeam(currentMatch, currentGame, match.Team1Id);
            }

            //Add the point to the game
            game.Points.Add(pb.build());

            //Update the observers
            updateObservers(currentMatch, currentSet, currentGame);

            ChangeServer(currentMatch, currentGame);

            return pb._faultCount;
        }


        public void InPlay(Game game)
        {
            //Build the point from the point builder
            Point point = inPlayPB.serverId(game.Servers[game.Servers.Count - 1]).build();

            //Reset the static point builder
            inPlayPB = new Point.PointBuilder();

            // Give points to the winner team
            GivePointToTeam(currentMatch, currentGame, point.WinnerId);
            game.Points.Add(point);

            //Notify observers
            updateObservers(currentMatch, currentSet, currentGame);

            //CHanger server if tiebreak
            ChangeServer(currentMatch, currentGame);
        }

        /*
         *   Get the current score of the current set.
         *
         */
        public List<int> GetCurrentMatchScore(Match match)
        {
            List<int> currentScore = new List<int> {match.Team1Score, match.Team2Score};
            return currentScore;
        }

        /*
         *   Get the current score of the current set.
         *
         */
        public List<int> GetCurrentSetScore(Set set)
        {
            List<int> currentScore = new List<int> {set.Team1Score, set.Team2Score};
            return currentScore;
        }

        /*
         *   Get the current score of the current game.
         *
         */
        public List<int> GetCurrentGameScore(Game game)
        {
            List<int> currentScore = new List<int> {game.lastScoreTeam1, game.lastScoreTeam2};
            return currentScore;
        }

        /*
         *   Get the current team names.
         *
         *   First entry in the list is team1
         *   Second entry in the list is team2
         *
         */
         public List<string> GetTeamNames(Match match)
        {
            List<string> teamNames = new List<string> {match.Team1Id, match.Team2Id};
            return teamNames;
        }

        public GameType GetCurrentGameType()
        {
            return currentGame.GameType;
        }

        public Game GetCurrentGame()
        {
            return currentGame;
        }

        public Set GetCurrentSet()
        {
            return currentSet;
        }

        public Match GetCurrentMatch()
        {
            return currentMatch;
        }

        public string GetMatchScore(Match match)
        {
            string score = "";

            for (int i = 0; i < match.Sets.Count; i++)
            {
                if (i == 0 && match.Sets.Count > 1)
                {
                    score += match.Sets[i].Team1Score + " / " + match.Sets[i].Team2Score + " ";
                }
                else
                {
                    score += match.Sets[i].Team1Score + " / " + match.Sets[i].Team2Score;
                }
            }
            return score;
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
        private void GivePointToTeam(Match match, Game game, string winnerId)
        {
            // Check on which team the winner is, and give points accordingly
            if (winnerId.Equals(match.Team1Id))
            {
                game.Team1Score.Add(game.lastScoreTeam1 + 1);
                game.Team2Score.Add(game.lastScoreTeam2);
            }
            else
            {
                game.Team1Score.Add(game.lastScoreTeam1);
                game.Team2Score.Add(game.lastScoreTeam2 + 1);
            }
        }

        /*
         *   Service method to add empty points to both teams
         */
        private void GiveEmptyPoints(Game game)
        {
            game.Team1Score.Add(game.lastScoreTeam1);
            game.Team2Score.Add(game.lastScoreTeam2);
        }

        /*
         *   Helping method used to notify all observers
         *
         */
        private void updateObservers(Match match, Set set, Game game)
        {
            //Update game status before notifying observers
            UpdateEntireMatchStatus(match, set, game);

            // If the game is not finished, notify with OnNext
            foreach (IObserver<Match> observer in matchObservers)
            {
                observer.OnNext(match);
            }

            if (match.EndTime != 0)
            {
                //TODO If the game is finished, notify with OnCompleted
                //foreach (IObserver<Match> observer in matchObservers)
                //{
                //    observer.OnComplete(currentMatch);
                //}
            }
        }

        private async void UpdateEntireMatchStatus(Match match, Set set, Game game)
        {
            UpdateGameStatus(match, set, game);
            UpdateSetStatus(match, set);
            UpdateMatchStatus(match);

            //Remove the last empty set, if the match is done
            if (match.EndTime != 0)
            {
                match.Sets.Remove(match.Sets[match.Sets.Count - 1]);
            }

            //Post current match data
            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            await firebaseClient.Child(FBTables.FBMatch).Child(match.MatchId).PutAsync(match);
        }

        private void UpdateMatchStatus(Match match)
        {
            switch (match.Type)
            {
                case MatchType.ONESETTER:
                    if (match.Team1Score > 0)
                    {
                        //Team 1 wins
                        matchObservers[0].OnCompleted();
                        match.EndTime = Util.GenerateTimeStamp();
                        match.WinnerId = match.Team1Id;
                    }
                    else if (match.Team2Score > 0)
                    {
                        //Team 2 wins
                        matchObservers[0].OnCompleted();
                        match.EndTime = Util.GenerateTimeStamp();
                        match.WinnerId = match.Team2Id;
                    }
                    break;
                case MatchType.THREESETTER:
                    if (match.Team1Score >= 2)
                    {
                        //Team 1 wins
                        matchObservers[0].OnCompleted();
                        match.EndTime = Util.GenerateTimeStamp();
                        match.WinnerId = match.Team1Id;
                    }
                    else if (currentMatch.Team2Score >= 2)
                    {
                        //team 2 wins
                        matchObservers[0].OnCompleted();
                        match.EndTime = Util.GenerateTimeStamp();
                        match.WinnerId = match.Team2Id;
                    }
                    break;
                case MatchType.FIVESETTER:
                    if (match.Team1Score >= 3)
                    {
                        //Team 1 wins
                        matchObservers[0].OnCompleted();
                        match.EndTime = Util.GenerateTimeStamp();
                        match.WinnerId = match.Team1Id;
                    }
                    else if (match.Team2Score >= 3)
                    {
                        //team 2 wins
                        matchObservers[0].OnCompleted();
                        match.EndTime = Util.GenerateTimeStamp();
                        match.WinnerId = match.Team2Id;
                    }
                    break;
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
        private void UpdateSetStatus(Match match, Set set)
        {
            if (set.Team1Score <= 5 && set.Team2Score <= 5) return;
            if (Math.Abs(set.Team1Score - set.Team2Score) < 2 &&
                (set.Team1Score != 6 || set.Team2Score != 7) &&
                (set.Team1Score != 7 || set.Team2Score != 6)) return;

            set.Games.Remove(set.Games[set.Games.Count - 1]);

            RegisterSetWinner(match, set, set.Team1Score > set.Team2Score ? match.Team1Id : match.Team2Id);
        }

        /*
         *   Checking if someone has won the current game:
         *
         *   If one of the teams has more than 3 points:
         *   - Is the absolute value from the subtraction,
         *     of the team scores more/equal than 2
         *   - Who has the more points wins
         *
         */
        private void UpdateGameStatus(Match match, Set set, Game game)
        {
            int minimumScore = 3;
            if (game.GameType == GameType.TIEBREAK) minimumScore = 6;

            if (game.lastScoreTeam1 <= minimumScore && game.lastScoreTeam2 <= minimumScore) return;
            if (Math.Abs(game.lastScoreTeam1 - game.lastScoreTeam2) < 2) return;

            RegisterGameWinner(match, set, game,
                game.lastScoreTeam1 > game.lastScoreTeam2 ? match.Team1Id : match.Team2Id);
        }


        /*
         *   Following is executed when registering a winner:
         *   - Set the winner id of current game
         *   - Add the finished game to the current set
         *   - Add a point to the winner in the set
         *   - Create a new game
         *   - find the new server of that game
         */
        private void RegisterGameWinner(Match match, Set set, Game game, string winnerId)
        {
            // Register the winner of the current game
            game.WinnerId = winnerId;

            // Give a point to the right team
            if (winnerId.Equals(match.Team1Id))
            {
                set.Team1Score += 1;
            }
            else
            {
                set.Team2Score += 1;
            }

            // Create a new game
            string newServer;

            if (game.GameType == GameType.NORMAL)
            {
                newServer = game.Servers[game.Servers.Count - 1].Equals(match.Team1Id) ? match.Team2Id : match.Team1Id;
            }
            else
            {
                newServer = game.Servers[0].Equals(match.Team1Id) ? match.Team2Id : match.Team1Id;
            }

            // Checking whether the new game should be a normal or tiebreak
            if (set.Team1Score == 6 && set.Team2Score == 6)
            {
                currentGame = new Game.GameBuilder(newServer).gameType(GameType.TIEBREAK).build();
            }
            else
            {
                currentGame = new Game.GameBuilder(newServer).build();
            }

            set.Games.Add(currentGame);
        }

        /*
         *   Service method used to register the winner of a set
         */
        private void RegisterSetWinner(Match match, Set set, string winnerId)
        {
            //Set the winner of the set, and add it to the match
            set.WinnerId = winnerId;

            // Give a point to the right team
            if (winnerId.Equals(match.Team1Id))
            {
                match.Team1Score += 1;
            }
            else
            {
                match.Team2Score += 1;
            }

            //Create a new current set
            currentSet = new Set.SetBuilder().build();
            match.Sets.Add(currentSet);
        }

        /*
         *   Checks if the game is tiebreak,
         *   if so, change server accordingly.
         */
        private void ChangeServer(Match match, Game game)
        {
            // Never change the server, if the gametype is normal
            if (game.GameType == GameType.NORMAL) return;

            string currentServer = game.Servers[game.Servers.Count-1];

            if (game.Servers.Count % 2 == 1)
            {
                //Change the server
                game.Servers.Add(currentServer.Equals(match.Team1Id) ? match.Team2Id : match.Team1Id);
            }
            else
            {
                game.Servers.Add(currentServer);
            }
        }
    }
}
