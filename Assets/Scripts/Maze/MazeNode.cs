using UnityEngine;
using UnityEngine.Pool;

namespace Maze
{
    public enum NodeState
    {
        Start,
        Current,
        Default,
        End
    }

    public class MazeNode : MonoBehaviour
    {
        [SerializeField] private GameObject[] walls;
        [SerializeField] private MeshRenderer floor;

        public IObjectPool<MazeNode> NodePool { get; set; }

        // Resets the entity before returning it to the pool and disabling it
        private void OnDisable()
        {
            ResetNode();
        }
        
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
                case NodeState.Default:
                    floor.material.color = Color.white;
                    break;
                case NodeState.End:
                    floor.material.color = Color.green;
                    break;
            }
        }
        
        public void ReturnToPool()
        {
            NodePool.Release(this);
        }
        
        private void ResetNode()
        {
            SetState(NodeState.Default);

            foreach (var wall in walls)
            {
                wall.gameObject.SetActive(true);
            }
        }
    }
}