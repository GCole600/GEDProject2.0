using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeState
{
    Start,
    Current,
    Completed
}

public class MazeNode : MonoBehaviour
{
    [SerializeField] private GameObject[] walls;
    [SerializeField] private MeshRenderer floor;

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }
    
    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Start:
                floor.material.color = Color.yellow;
                break;
            case NodeState.Current:
                floor.material.color = Color.blue;
                break;
            case NodeState.Completed:
                floor.material.color = Color.green;
                break;
        }
    }
}