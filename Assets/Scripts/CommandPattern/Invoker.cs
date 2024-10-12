using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using ObserverPattern;
using Unity.VisualScripting;

namespace CommandPattern
{
    public class Invoker : Subject
    {
        private List<Command> _plannedCommands = new List<Command>();

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
            foreach (Command command in _plannedCommands)
            {
                command.Execute();
                yield return new WaitForSeconds(0.2f);
            }

            _plannedCommands.Clear();
        }

        public List<Command> GetPlannedCommands()
        {
            return _plannedCommands;
        }
    }
}