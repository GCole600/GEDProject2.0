using System.Collections.Generic;
using CommandPattern;
using SingletonPattern;
using TMPro;
using UnityEngine;

namespace ObserverPattern
{
    public class UIManager : Observer
    {
        [SerializeField] public TMP_Text listText;

        private List<Command> _plannedCommands = new List<Command>();

        public override void Notify(Subject subject)
        {
            _plannedCommands = Invoker.Instance.GetPlannedCommands();
            
            if (!Invoker.Instance.remove)
            {
                switch (Invoker.Instance.NewCommand)
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
            else
            {
                listText.text = ""; // Clear list text
                
                // Add commands to UI list text
                foreach (var command in _plannedCommands)
                {
                    switch (command)
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
}