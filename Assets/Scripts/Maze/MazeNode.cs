using UnityEngine;

namespace Maze
{
    public enum NodeState
    {
        Start,
        Current,
        End
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
                case NodeState.End:
                    floor.material.color = Color.green;
                    break;
            }
        }
    }
}