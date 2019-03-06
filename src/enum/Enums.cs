using System;
namespace TennisStats.Enum
{

    /*
     * This namespace includes all enums used in the program. 
     * 
     * In order to use an enum please import it statically:
     * using static Tennistat.Enum.<classname>    
     * 
     */    
    public class GenderEnum
    {
        public enum Gender { MALE, FEMALE }
    }

    public class MatchTypeEnum
    {
        public enum MatchType { ONESETTER, THREESETTER, FIVESETTER, MATCHTIEBREAK, TIEBREAK }
    }

    public class HandEnum
    {
        public enum Hand { LEFT, RIGHT }
    }

    // These enums are used in the Point class to express how a point is won.
    public class ServeStatusEnum {
        public enum ServeStatus { ACE, SERVEWINNER, FOOTFAULT, FAULT, INPLAY };
    }

    public class WinReasonEnum {
        public enum WinReason { FORCEDERROR, UNFORCEDERROR, WINNER };
    }

    public class StrokeTypeEnum {
        public enum StrokeType { DROPSHOT, SMASH, VOLLEY, LOB, APPROACH, RETURN, BASELINE };
    }

    public class HandPositionEnum {
        public enum HandPosition { FORHAND, BACKHAND };
    }

    public class SetTypeEnum {
        public enum SetType { NORMAL, TIEBREAK, MATCHTIEBREAK };
    }

}
