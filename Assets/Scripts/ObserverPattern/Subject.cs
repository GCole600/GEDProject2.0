using System.Collections;
using UnityEngine;

namespace ObserverPattern
{
    public abstract class Subject : MonoBehaviour
    {
        private readonly ArrayList _observers = new ArrayList();
        
        protected void AddObserver(Observer observer)
        {
            _observers.Add(observer);
        }
        
        protected void RemoveObserver(Observer observer)
        {
            _observers.Remove(observer);
        }
        
        protected void NotifyObservers()
        {
            foreach (Observer observer in _observers)
            {
                observer.Notify(this);
            }
        }
    }
}