using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TennisStats.Enum;
using TennisStats.Model;
using static TennisStats.Enum.FaultCountEnum;
using static TennisStats.Enum.ServeStatusEnum;
using static TennisStats.Enum.StatisticTypeEnum;
using Point = TennisStats.Model.Point;

namespace TennisStats.src.Controller
{
    public class StatisticController
    {
        public int calculateFirstServePercentage(string playerId, List<Point> points)
        {
            double servesInPlay = 0;
            double posibleServesInPlay = 0;

            foreach (Point point in points)
            {
                if (point.ServerId == playerId && point.FaultCount == FaultCount.FIRSTSERVE && point.WinnerId != null)
                {
                    servesInPlay++;
                    posibleServesInPlay++;
                } else if (point.ServerId == playerId && point.FaultCount == FaultCount.SECONDSERVE)
                {
                    posibleServesInPlay++;
                }
            }
            return posibleServesInPlay == 0 ? 0 : (int)(servesInPlay / posibleServesInPlay * 100);
        }
        
        public int calculateWinPercentageOnFirstServe(string playerId, List<Point> points)
        {
            double winOnFirstServe = 0;
            double totalFirstServesInPlay = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && point.FaultCount == FaultCount.FIRSTSERVE && point.WinnerId == playerId)
                {
                    totalFirstServesInPlay++;
                    winOnFirstServe++;
                }
                else if (playerId == point.ServerId && point.FaultCount == FaultCount.FIRSTSERVE && point.WinnerId != playerId && point.WinnerId != null)
                {
                    totalFirstServesInPlay++;
                }
            }

            return totalFirstServesInPlay == 0 ? 0 : (int)(winOnFirstServe / totalFirstServesInPlay * 100);
        }
        
        public int calculateWinPercentageOnSecondServe(string playerId, List<Point> points)
        {
            double winOnSecondServe = 0;
            double totalSecondServesInPlay = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && point.FaultCount == FaultCount.SECONDSERVE && point.WinnerId == playerId)
                {
                    totalSecondServesInPlay++;
                    winOnSecondServe++;
                }
                else if (playerId == point.ServerId && point.FaultCount == FaultCount.SECONDSERVE && point.WinnerId != playerId && point.WinnerId != null)
                {
                    totalSecondServesInPlay++;
                }
            }

            return totalSecondServesInPlay == 0 ? 0 : (int)(winOnSecondServe / totalSecondServesInPlay * 100);
        }

        //TODO: Vurdere om der skal returneres en liste, så vi kan få hvor mange vundet breakpoints ud fra hvor mange mulige.
        public int calculateBreakPointsWon(string playerId, List<Point> points)
        {
            int breakPointsWon = 0;

            //TODO: Do the calculation

            return breakPointsWon;
        }
        
        public int calculateTotalPointsWon(string playerId, List<Point> points)
        {
            int totalPointsWon = 0;

            foreach (Point point in points)
            {
                if (playerId == point.WinnerId)
                {
                    totalPointsWon++;
                }
            }

            return totalPointsWon;
        }
        
        public int calculateAmountOfAces(string playerId, List<Point> points)
        {
            int aces = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && point.ServeStatus == ServeStatus.ACE)
                {
                    aces++;
                }
            }
            
            return aces;
        }
        
        public int calculateAmountOfDoubleFaults(string playerId, List<Point> points)
        {
            int doubleFaults = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && 
                    (point.ServeStatus == ServeStatus.FAULT || point.ServeStatus == ServeStatus.FOOTFAULT) &&
                    point.FaultCount == FaultCount.SECONDSERVE)
                {
                    doubleFaults++;
                }
            }

            return doubleFaults;
        }
        
        public int calculateAmountOfUnforcedErrors(string playerId, List<Point> points)
        {
            int unforcedErrors = 0;

            foreach (Point point in points)
            {
                if (playerId != point.WinnerId && point.WinnerId != null && point.WinReason == WinReasonEnum.WinReason.UNFORCEDERROR)
                {
                    unforcedErrors++;
                }
            }

            return unforcedErrors;
        }
        
        public int calculateAmountOfWinners(string playerId, List<Point> points)
        {
            int winners = 0;

            foreach (Point point in points)
            {
                if (playerId == point.WinnerId && point.WinnerId != null && point.WinReason == WinReasonEnum.WinReason.WINNER)
                {
                    winners++;
                }
            }

            return winners;
        }
        
        public int calculateAmountOfForcedErrors(string playerId, List<Point> points)
        {
            int forcedErrors = 0;

            foreach (Point point in points)
            {
                if (playerId != point.WinnerId && point.WinnerId != null && point.WinReason == WinReasonEnum.WinReason.FORCEDERROR)
                {
                    forcedErrors++;
                }
            }

            return forcedErrors;
        }
        
        public int calculateAmountOfMatchWins(string playerId, List<Match> matches)
        {
            int matchWins = 0;

            foreach (Match match in matches)
            {
                if (playerId == match.WinnerId)
                {
                    matchWins++;
                }
            }

            return matchWins;
        }
        
        public int calculateAmountOfMatchLosses(string playerId, List<Match> matches)
        {
            int matchLosses = 0;

            foreach (Match match in matches)
            {
                if (playerId == match.WinnerId)
                {
                    matchLosses++;
                }
            }

            return matchLosses;
        }

        public async Task<List<Match>> GetMatches(string playerId)
        {
            FirebaseClient firebaseClient = FBTables.FirebaseClient;
            
            List<Match> matches = new List<Match>();
            
            var collectedMatches = await firebaseClient.Child(FBTables.FBMatch).OnceAsync<Match>();

            foreach (var match in collectedMatches)
            {
                if (match.Object.MatchId.Contains(playerId))
                {
                    matches.Add(match.Object);
                }
            }

            return matches;
        }

        public async Task<List<Point>> GetListOfPointsToBeCalculatedAsync(string playerId, StatisticType statisticType, string matchId = null, int set = 0)
        {

            List<Match> matches = await GetMatches(playerId);
            List<Point> points = new List<Point>();
            
            Console.WriteLine("Size of Matches: " + matches.Count);
            

            if (matchId == null && statisticType == StatisticType.MATCH) 
            {
                return null;
            }
            
            switch (statisticType)
            {
                //TODO jeg kan ikke lig vi har 3 nested for-loops, for at finde alle point
                case StatisticType.OVERALL:
                    foreach (Match match in matches)
                    {
                        foreach (Set matchSet in match.Sets)
                        {
                            foreach (Game game in matchSet.Games)
                            {
                                if (game.Points != null)
                                {
                                    points.AddRange(game.Points);
                                }
                            }
                        }
                    }
                    Console.WriteLine("Just before breaking");
                    break;
                case StatisticType.MATCH:
                    Console.WriteLine("Went in to match");
                    foreach (Match match in matches)
                    {
                        if (match.MatchId.Equals(matchId))
                        {
                            foreach (Set matchSet in match.Sets)
                            {
                                foreach (Game game in matchSet.Games)
                                {
                                    if (game.Points != null)
                                    {
                                        points.AddRange(game.Points);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case StatisticType.SET:
                    foreach (Match match in matches)
                    {
                        if (match.MatchId.Equals(matchId))
                        {
                            foreach (Game game in match.Sets[set].Games)
                            {
                                if (game.Points != null)
                                {
                                    points.AddRange(game.Points);
                                }
                            }
                        }
                    }
                    break;
                case StatisticType.LASTMOUNTH:
                    foreach (Match match in matches)
                    {
                        if (Util.GenerateTimeStamp() - match.EndTime < Util.OneYearInMili)
                        {
                            foreach (Set matchSet in match.Sets)
                            {
                                foreach (Game game in matchSet.Games)
                                {
                                    if (game.Points != null)
                                    {
                                        points.AddRange(game.Points);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case StatisticType.LASTYEAR:
                    foreach (Match match in matches)
                    {
                        if (Util.GenerateTimeStamp() - match.EndTime < Util.OneMonthInMili)
                        {
                            foreach (Set matchSet in match.Sets)
                            {
                                foreach (Game game in matchSet.Games)
                                {
                                    if (game.Points != null)
                                    {
                                        points.AddRange(game.Points);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            
            Console.WriteLine("Size of Points: " + points.Count);
            return points;
        }

        public List<Point> GetPointsBasedOnMatch(Match match, int set = 0)
        {
            List<Point> points = new List<Point>();
            if (set == 0)
            {
                foreach (Set matchSet in match.Sets)
                {
                    foreach (Game game in matchSet.Games)
                    {
                        points.AddRange(game.Points);
                    }
                }
            }
            else
            {
                foreach (Game game in match.Sets[set].Games)
                {
                    points.AddRange(game.Points);
                }
            }
            return points;
        }
    }
}
