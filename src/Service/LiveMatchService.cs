using System;
using System.Collections.Generic;
using TennisStats.Model;

namespace TennisStats.Service
{
    public class LiveMatchService : IObservable<Match>
    {

        private List<IObserver<Match>> observers;
        private List<Match> matches;

        public LiveMatchService()
        {
            observers = new List<IObserver<Match>>();
            matches = new List<Match>();
        }
        
        
        
        public IDisposable Subscribe(IObserver<Match> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (! observers.Contains(observer)) {
                observers.Add(observer);
                // Provide observer with existing data.
                foreach (var item in matches)
                    observer.OnNext(item);
            }
            return new Unsubscriber<Match>(observers, observer);
        }
    }
}