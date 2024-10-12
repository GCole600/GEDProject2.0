using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace CommandPattern
{
    public class Invoker : MonoBehaviour
    {
        private List<Command> _plannedCommands = new List<Command>();
        
        public void ExecuteCommand(Command command)
        {
            _plannedCommands.Add(command);
        }

        public void PlayCommands()
        {
            StartCoroutine(PlaySteps());
        }

        public void Undo()
        {
            _plannedCommands.RemoveAt(_plannedCommands.Count - 1);
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
    }
}