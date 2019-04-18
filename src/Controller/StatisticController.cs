using System;
using System.Collections.Generic;
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
        public List<Point> GetListOfPointsToBeCalculated(string playerId, StatisticType statisticType, string matchId = null)
        {
            if (matchId == null && statisticType == StatisticType.MATCH) 
            {
                return null;
            }
            
            List<Point> points = new List<Point>();
            
            //TODO: Her skal der laves kode til at finde alle 

            switch (statisticType)
            {
                case StatisticType.OVERALL:
                    // DO something...
                    break;
                case StatisticType.MATCH:
                    // DO something...
                    break;
                case StatisticType.SET:
                    // DO something...
                    break;
                case StatisticType.LASTMOUNTH:
                    // DO something...
                    break;
                case StatisticType.LASTYEAR:
                    // DO something...
                    break;
            }
            
            return points;
        }

        public int calculateFirstServePercentage(string playerId, List<Point> points)
        {
            double servesInPlay = 0;
            double posibleServesInPlay = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && point.FaultCount == FaultCount.FIRSTSERVE)
                {
                    posibleServesInPlay++;
                    if (point.ServeStatus == ServeStatus.ACE)
                    {
                        servesInPlay++;
                    }
                }
            }

            return (int)(servesInPlay / posibleServesInPlay * 100);
        }
        
        public int calculateWinPercentageOnFirstServe(string playerId, List<Point> points)
        {
            double winOnFirstServe = 0;
            double totalFirstServesInPlay = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && point.FaultCount == FaultCount.FIRSTSERVE)
                {
                    totalFirstServesInPlay++;
                    if (playerId == point.WinnerId)
                    {
                        winOnFirstServe++;
                    }
                }
            }

            return (int)(winOnFirstServe / totalFirstServesInPlay * 100);
        }
        
        public int calculateWinPercentageOnSecondServe(string playerId, List<Point> points)
        {
            double winOnSecondServe = 0;
            double totalSecondServesInPlay = 0;

            foreach (Point point in points)
            {
                if (playerId == point.ServerId && point.FaultCount == FaultCount.SECONDSERVE)
                {
                    totalSecondServesInPlay++;
                    if (playerId == point.WinnerId)
                    {
                        winOnSecondServe++;
                    }
                }
            }

            return (int)(winOnSecondServe / totalSecondServesInPlay * 100);
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
                if (playerId != point.WinnerId && point.WinReason == WinReasonEnum.WinReason.UNFORCEDERROR)
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
                if (playerId == point.WinnerId && point.WinReason == WinReasonEnum.WinReason.WINNER)
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
                if (playerId != point.WinnerId && point.WinReason == WinReasonEnum.WinReason.FORCEDERROR)
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

    }
}
