using System;
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

        public bool remove;
        public Command NewCommand;

        public static Invoker Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);
            DontDestroyOnLoad(this);
        }

        private void OnEnable() { AddObserver(FindObjectOfType<UIManager>()); }
        private void OnDisable() { RemoveObserver(FindObjectOfType<UIManager>()); }

        public void ExecuteCommand(Command command)
        {
            _plannedCommands.Add(command);
            
            NewCommand = command;
            remove = false;
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
            remove = true;
            NotifyObservers();
        }

        private IEnumerator PlaySteps()
        {
            // Reset all traps to initial state
            _components = FindObjectsOfType<Trap>();
            foreach (var trap in _components)
            {
                trap.ReturnToPool();
            }
            
            MazeGenerator.Instance.ResetTraps();
            
            foreach (var command in _plannedCommands)
            {
                command.Execute();
                
                ToggleTraps();
                
                yield return new WaitForSeconds(0.2f);
            }
            
            ClearList();
        }

        public void ClearList()
        {
            remove = true;

            for (var i = 0; i < _plannedCommands.Count; i++)
            {
                NotifyObservers();
            }
            
            NewCommand = null;
            _plannedCommands.Clear();
            
            remove = false;
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