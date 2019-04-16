using System;
using System.Collections.Generic;
using TennisStats.Model;
using static TennisStats.Enum.StatisticTypeEnum;
using Point = TennisStats.Model.Point;

namespace TennisStats.src.Controller
{
    public class StatisticController
    {
        public List<Point> GetListOfPointToBeCalculated(string playerId, StatisticType statisticType, string matchId = null)
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
            int firstServePercentage = 0;

            
            //TODO: Do the calculation

            return firstServePercentage;
        }
        
        public int calculateWinPercentageOnFirstServe(string playerId, List<Point> points)
        {
            int winpercentageOnFirstServe = 0;

            //TODO: Do the calculation
            
            return winpercentageOnFirstServe;
        }
        
        public int calculateWinPercentageOnSecondServe(string playerId, List<Point> points)
        {
            int winpercentageOnSecondServe = 0;

            //TODO: Do the calculation

            return winpercentageOnSecondServe;
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

            //TODO: Do the calculation

            return totalPointsWon;
        }
        
        public int calculateAmountOfAces(string playerId, List<Point> points)
        {
            int aces = 0;

            //TODO: Do the calculation

            return aces;
        }
        
        public int calculateAmountOfDoubleFaults(string playerId, List<Point> points)
        {
            int doubleFaults = 0;

            //TODO: Do the calculation

            return doubleFaults;
        }
        
        public int calculateAmountOfUnforcedErrors(string playerId, List<Point> points)
        {
            int unforcedErrors = 0;

            //TODO: Do the calculation

            return unforcedErrors;
        }
        
        public int calculateAmountOfWinners(string playerId, List<Point> points)
        {
            int winners = 0;

            //TODO: Do the calculation

            return winners;
        }
        
        public int calculateAmountOfForcedErrors(string playerId, List<Point> points)
        {
            int forcedErrors = 0;

            //TODO: Do the calculation

            return forcedErrors;
        }
        
        public int calculateAmountOfMatchWins(string playerId, List<Match> matches)
        {
            int matchWins = 0;

            //TODO: Do the calculation

            return matchWins;
        }
        
        public int calculateAmountOfMatchLosses(string playerId, List<Match> matches)
        {
            int matchLosses = 0;

            //TODO: Do the calculation

            return matchLosses;
        }

    }
}
