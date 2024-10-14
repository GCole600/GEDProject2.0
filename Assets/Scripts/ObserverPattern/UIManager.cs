using System.Collections.Generic;
using CommandPattern;
using SingletonPattern;
using TMPro;
using UnityEngine;

namespace ObserverPattern
{
    public class UIManager : Observer
    {
        [SerializeField] private TMP_Text listText;

        public override void Notify(Subject subject)
        {
            List<Command> plannedCommands = subject.GetComponent<Invoker>().GetPlannedCommands();
            
            // Clear list text
            listText.text = "";
            
            // Add commands to UI list text
            for (int i = 0; i < plannedCommands.Count; i++)
            {
                switch (plannedCommands[i])
                {
                    case MoveUp:
                        listText.text += "\nUp";
                        break;
                    case MoveLeft:
                        listText.text += "\nLeft";
                        break;
                    case MoveDown:
                        listText.text += "\nDown";
                        break;
                    case MoveRight:
                        listText.text += "\nRight";
                        break;
                }
            }
        }
    }
}