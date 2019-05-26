using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Store
{
    internal class Unsubscriber<T> : IDisposable
    {
        private IList<IObserver<T>> observers;
        private IObserver<T> observer;

        internal Unsubscriber(IList<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }
}
