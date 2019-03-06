using System;
using static TennisStats.Enum.HandPositionEnum;
using static TennisStats.Enum.ServeStatusEnum;
using static TennisStats.Enum.StrokeTypeEnum;
using static TennisStats.Enum.WinReasonEnum;

namespace TennisStats.Model
{
    public class Point
    {

        private string _winnerId;
        private ServeStatus _serveStatus;
        private WinReason _winReason;
        private StrokeType _strokeType;
        private HandPosition _handPosition;

        private Point(PointBuilder pb) {
            _winnerId = pb._winnerId;
            _serveStatus = pb._serveStatus;
            _winReason = pb._winReason;
            _strokeType = pb._strokeType;
            _handPosition = pb._handPosition;
        }


        public string WinnerId { get { return _winnerId; } set { _winnerId = value; } }
        public ServeStatus ServeStatus { get { return _serveStatus; } set { _serveStatus = value; } }
        public WinReason WinReason { get { return _winReason; }set { _winReason = value; } }
        public StrokeType StrokeType { get { return _strokeType; } set{_strokeType=value; } }
        public HandPosition HandPosition { get{ return _handPosition; } set { _handPosition = value; } }


        class PointBuilder {
            public string _winnerId;
            public ServeStatus _serveStatus;
            public WinReason _winReason;
            public StrokeType _strokeType;
            public HandPosition _handPosition;

            //Mandatory variables
            public PointBuilder() { }

            //optional variables
            public PointBuilder winnderId (string winnderId){ _winnerId = winnderId; return this; }
            public PointBuilder serveStatus(ServeStatus serveStatus) { _serveStatus = serveStatus; return this; }
            public PointBuilder winReason(WinReason winReason) { _winReason = winReason; return this; }
            public PointBuilder strokeType(StrokeType strokeType) { _strokeType = strokeType; return this; }
            public PointBuilder handPosition(HandPosition handPosition) { _handPosition = handPosition; return this; }

            //Build
            public Point build() { return new Point(this); }

        }
    }
}
