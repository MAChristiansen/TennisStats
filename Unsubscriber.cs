using System;
using System.Collections.Generic;

namespace TennisStats
{
    internal class Unsubscriber<Match> : IDisposable
    {
        private List<IObserver<Match>> _observers;
        private IObserver<Match> _observer;

        internal Unsubscriber(List<IObserver<Match>> observers, IObserver<Match> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
