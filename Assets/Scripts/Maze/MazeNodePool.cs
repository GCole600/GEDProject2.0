using UnityEngine;
using UnityEngine.Pool;

namespace Maze
{
    public class MazeNodePool : MonoBehaviour
    {
        public GameObject nodePrefab;
        
        public int maxPoolSize = 400;
        public int stackDefaultCapacity = 400;
        
        // Object pool initialization with passing callback methods in the constructor for logic implementation
        public IObjectPool<MazeNode> NodePool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<MazeNode>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }
        
        private IObjectPool<MazeNode> _pool;
        
        public MazeNode Spawn(Vector3 pos)
        {
            MazeNode obj = NodePool.Get();
            obj.transform.position = pos;
            return obj;
        }
        
        // Callback implementation
        private MazeNode CreatedPooledItem()
        {
            var newNode = Instantiate(nodePrefab, transform.position, Quaternion.identity);

            MazeNode mazeNode = newNode.GetComponent<MazeNode>();
            
            mazeNode.NodePool = NodePool;
            
            return mazeNode;
        }

        // Object gets deactivated and removed from scene
        private void OnReturnedToPool(MazeNode obj)
        {
            obj.gameObject.SetActive(false);
        }

        // Requests an instance from the pool
        private void OnTakeFromPool(MazeNode obj)
        {
            obj.gameObject.SetActive(true);
        }

        // Method called when there is no more space in the pool,
        // Destroying the returned instance to free memory
        private void OnDestroyPoolObject(MazeNode obj)
        {
            Destroy(obj.gameObject);
        }
    }
}