using System;
using TennisStats.Enum;
using TennisStats.Model;
using static TennisStats.Enum.GameTypeEnum;
using static TennisStats.Enum.MatchTypeEnum;

namespace TennisStats.src.Service
{
    public sealed class PointService
    {
        private static PointService instance = null;
        private static readonly object padlock = new object();

        public static PointService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PointService();
                    }
                    return instance;
                }
            }
        }
        public PointService() { }

        public string convertPoints(int pointToConvert, int opponent, GameType gameType) {

            if (gameType == GameType.TIEBREAK) return pointToConvert + "";


            if (pointToConvert < 4 && opponent < 4) {
                switch (pointToConvert) {
                    case 0:
                        return "0";
                    case 1:
                        return "15";
                    case 2:
                        return "30";
                    case 3:
                        return "40";
                }
            }
            else {
                if (pointToConvert > opponent) {
                    return "AD";
                }
                
                return "40";
                
            }
            return "";
        }
        
        
        //TODO: Do this method
        public bool isGameDone(Game game, MatchType matchType)
        {
            
            return false;
        }

        //TODO: Do this method
        public bool isSetDone(Set set, MatchType matchType)
        {
            
            return false;
        }

        //TODO: Do this method
        public bool isMatchDone(Match match)
        {

            return false;
        }
    }
}
