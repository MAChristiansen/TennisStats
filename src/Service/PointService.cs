using System;
using TennisStats.Enum;
using TennisStats.Model;
using static TennisStats.Enum.MatchTypeEnum;

namespace TennisStats.src.Service
{
    public class PointService
    {
        public PointService()
        {
        }
        
        public string convertPoints(int pointToConvert, int opponent) {
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
