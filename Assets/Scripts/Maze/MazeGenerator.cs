using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeNode nodePrefab;
    [SerializeField] private Vector2Int mazeSize;

    [SerializeField] private GameObject character;
    
    List<MazeNode> _nodes = new List<MazeNode>();
    List<MazeNode> _currentPath = new List<MazeNode>();
    List<MazeNode> _completedNodes = new List<MazeNode>();

    private void Start()
    {
        GenerateMaze(mazeSize);
    }

    void GenerateMaze(Vector2Int size)
    {
        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
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
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!_completedNodes.Contains(_nodes[currentNodeIndex + size.y]) &&
                    !_currentPath.Contains(_nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!_completedNodes.Contains(_nodes[currentNodeIndex - size.y]) &&
                    !_currentPath.Contains(_nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
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
        
        // Set character start position & color starting and end nodes
        _completedNodes[0].SetState(NodeState.Start);
        character.transform.position = _completedNodes[0].transform.position;
        _completedNodes[^1].SetState(NodeState.Completed);
    }

    public void ColorNode()
    {
        // Check if player position matches node position
        for (int i = 0; i < _completedNodes.Count; i++)
        {
            if (character.transform.position == _completedNodes[i].transform.position)
            {
                _completedNodes[i].SetState(NodeState.Current);
            }
        }
    }
}