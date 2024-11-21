using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

namespace ObserverPattern
{
    public class UIManager : Observer
    {
        [SerializeField] private GameObject contentHolder;
        
        [SerializeField] private GameObject rightPrefab;
        [SerializeField] private GameObject leftPrefab;
        [SerializeField] private GameObject upPrefab;
        [SerializeField] private GameObject downPrefab;

        private Vector3 _spawnPos = new Vector3(-25, 263, 0);

        private List<Command> _plannedCommands = new List<Command>();

        public override void Notify(Subject subject)
        {
            if (!Invoker.Instance.remove)
            {
                GameObject newText;
                switch (Invoker.Instance.NewCommand)
                {
                    case MoveUp:
                        newText = Instantiate(upPrefab, contentHolder.transform);
                        break;
                    case MoveLeft:
                        newText = Instantiate(leftPrefab, contentHolder.transform);
                        break;
                    case MoveDown:
                        newText = Instantiate(downPrefab, contentHolder.transform);
                        break;
                    case MoveRight:
                        newText = Instantiate(rightPrefab, contentHolder.transform);
                        break;
                    default:
                        newText = Instantiate(upPrefab, contentHolder.transform);
                        break;
                }

                newText.transform.localPosition = _spawnPos;
                _spawnPos += new Vector3(0, -20, 0);
            }
            else
            {
                Destroy(contentHolder.transform.GetChild(contentHolder.transform.childCount - 1));
            }
        }
    }
}