using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using ObjectPool;
using ObserverPattern;
using SingletonPattern;

namespace CommandPattern
{
    public class Invoker : Subject
    {
        private List<Command> _plannedCommands = new List<Command>();

        private Trap[] _components;
        
        private void OnEnable()
        {
            AddObserver(FindObjectOfType<UIManager>());
        }
        
        private void OnDisable()
        {
            RemoveObserver(FindObjectOfType<UIManager>());
        }

        public void ExecuteCommand(Command command)
        {
            _plannedCommands.Add(command);
            NotifyObservers();
            
            ToggleTraps();
        }

        public void PlayCommands()
        {
            StartCoroutine(PlaySteps());
        }

        public void Undo()
        {
            _plannedCommands.RemoveAt(_plannedCommands.Count - 1);
            NotifyObservers();
        }

        private IEnumerator PlaySteps()
        {
            // Reset all traps to initial state
            _components = FindObjectsOfType<Trap>();
            foreach (Trap trap in _components)
            {
                trap.ReturnToPool();
            }
            
            MazeGenerator.Instance.ResetTraps();
            
            
            foreach (Command command in _plannedCommands)
            {
                command.Execute();
                
                ToggleTraps();
                
                yield return new WaitForSeconds(0.2f);
            }

            _plannedCommands.Clear();
            NotifyObservers();
        }

        public List<Command> GetPlannedCommands()
        {
            return _plannedCommands;
        }

        private void ToggleTraps()
        {
            _components = FindObjectsOfType<Trap>();
            
            // If no traps are active
            if (_components.Length == 0)
            {
                foreach (var t in MazeGenerator.Instance.trapPos)
                {
                    TrapSpawner.Instance.SpawnTrap(t);
                }
            }
            
            // If traps are active
            foreach (Trap trap in _components)
            {
                trap.ToggleTrap();
            }
        }
    }
}