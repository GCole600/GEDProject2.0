using System;
using System.Collections.Generic;
using Maze;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SingletonPattern
{
    public class MazeGenerator : Singleton<MazeGenerator>
    {
        [SerializeField] private MazeNode nodePrefab;
        public Vector2Int mazeSize;

        [SerializeField] private GameObject character;
        [SerializeField] private GameObject key;

        private List<MazeNode> _nodes = new List<MazeNode>();
        private List<MazeNode> _currentPath = new List<MazeNode>();
        private List<MazeNode> _completedNodes = new List<MazeNode>();

        private bool _generationDone;

        private void Start()
        {
            character.GetComponent<Renderer>().enabled = false;
            key.GetComponent<Renderer>().enabled = false;
        }

        private void Update()
        {
            if (_generationDone)
            {
                // Check if player has reached end
                if (Math.Abs(character.transform.position.x - _completedNodes[^1].transform.position.x) < 0.2f 
                    && Math.Abs(character.transform.position.z - _completedNodes[^1].transform.position.z) < 0.2f)
                {
                    GameManager.Instance.EndGame();
                }
            }
        }

        public void GenerateMaze()
        {
            // Create nodes
            for (int x = 0; x < mazeSize.x; x++)
            {
                for (int y = 0; y < mazeSize.y; y++)
                {
                    Vector3 nodePos = new Vector3(x - (mazeSize.x / 2f), 0, y - (mazeSize.y / 2f));
                    MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                    _nodes.Add(newNode);
                }
            }

            // Choose starting node
            _currentPath.Add(_nodes[Random.Range(0, _nodes.Count)]);

            while (_completedNodes.Count < _nodes.Count)
            {
                // Check nodes next to the current node
                List<int> possibleNextNodes = new List<int>();
                List<int> possibleDirections = new List<int>();

                int currentNodeIndex = _nodes.IndexOf(_currentPath[^1]);
                int currentNodeX = currentNodeIndex / mazeSize.y;
                int currentNodeY = currentNodeIndex % mazeSize.y;

                if (currentNodeX < mazeSize.x - 1)
                {
                    // Check node to the right of the current node
                    if (!_completedNodes.Contains(_nodes[currentNodeIndex + mazeSize.y]) &&
                        !_currentPath.Contains(_nodes[currentNodeIndex + mazeSize.y]))
                    {
                        possibleDirections.Add(1);
                        possibleNextNodes.Add(currentNodeIndex + mazeSize.y);
                    }
                }

                if (currentNodeX > 0)
                {
                    // Check node to the left of the current node
                    if (!_completedNodes.Contains(_nodes[currentNodeIndex - mazeSize.y]) &&
                        !_currentPath.Contains(_nodes[currentNodeIndex - mazeSize.y]))
                    {
                        possibleDirections.Add(2);
                        possibleNextNodes.Add(currentNodeIndex - mazeSize.y);
                    }
                }

                if (currentNodeY < mazeSize.y - 1)
                {
                    // Check node above the current node
                    if (!_completedNodes.Contains(_nodes[currentNodeIndex + 1]) &&
                        !_currentPath.Contains(_nodes[currentNodeIndex + 1]))
                    {
                        possibleDirections.Add(3);
                        possibleNextNodes.Add(currentNodeIndex + 1);
                    }
                }

                if (currentNodeY > 0)
                {
                    // Check node below the current node
                    if (!_completedNodes.Contains(_nodes[currentNodeIndex - 1]) &&
                        !_currentPath.Contains(_nodes[currentNodeIndex - 1]))
                    {
                        possibleDirections.Add(4);
                        possibleNextNodes.Add(currentNodeIndex - 1);
                    }
                }

                // Choose next node
                if (possibleDirections.Count > 0)
                {
                    int chosenDirection = Random.Range(0, possibleDirections.Count);
                    MazeNode chosenNode = _nodes[possibleNextNodes[chosenDirection]];

                    switch (possibleDirections[chosenDirection])
                    {
                        case 1:
                            chosenNode.RemoveWall(1);
                            _currentPath[^1].RemoveWall(0);
                            break;
                        case 2:
                            chosenNode.RemoveWall(0);
                            _currentPath[^1].RemoveWall(1);
                            break;
                        case 3:
                            chosenNode.RemoveWall(3);
                            _currentPath[^1].RemoveWall(2);
                            break;
                        case 4:
                            chosenNode.RemoveWall(2);
                            _currentPath[^1].RemoveWall(3);
                            break;
                    }

                    _currentPath.Add(chosenNode);
                }
                else
                {
                    _completedNodes.Add(_currentPath[^1]);

                    _currentPath.RemoveAt(_currentPath.Count - 1);
                }
            }

            // Set character & key position
            character.transform.position = _completedNodes[0].transform.position;
            key.transform.position = _completedNodes[Random.Range(1, _completedNodes.Count - 2)].transform.position;
            character.GetComponent<Renderer>().enabled = true;
            key.GetComponent<Renderer>().enabled = true;
            key.SetActive(true);
            
            // Color starting and end nodes
            _completedNodes[0].SetState(NodeState.Start);
            _completedNodes[^1].SetState(NodeState.End);

            _generationDone = true;
        }

        public void ColorNode()
        {
            // Check if player position matches node position
            for (int i = 0; i < _completedNodes.Count; i++)
            {
                if (Math.Abs(character.transform.position.x - _completedNodes[i].transform.position.x) < 0.2f 
                    && Math.Abs(character.transform.position.z - _completedNodes[i].transform.position.z) < 0.2f)
                {
                    _completedNodes[i].SetState(NodeState.Current);
                }
            }
        }
        
        public void ReloadVars()
        {
            _generationDone = false;
            character.GetComponent<Renderer>().enabled = false;
            key.GetComponent<Renderer>().enabled = false;

            _nodes.Clear();
            _currentPath.Clear();
            _completedNodes.Clear();
            
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}