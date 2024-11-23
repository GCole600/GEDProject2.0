using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveRightPool : MonoBehaviour
    {
        [SerializeField] private GameObject rightPrefab;
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject tempHolder;
        
        public int maxPoolSize = 15;
        public int stackDefaultCapacity = 15;

        public IObjectPool<MoveRightObject> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<MoveRightObject>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }

        private IObjectPool<MoveRightObject> _pool;
        
        // Gets an entity instance from the pool
        public void Spawn(Vector3 pos)
        {
            var obj = Pool.Get();
            obj.transform.SetParent(contentHolder.transform);
            obj.transform.localPosition = pos;
        }
        
        // Callback implementation
        private MoveRightObject CreatedPooledItem()
        {
            GameObject go = Instantiate(rightPrefab, contentHolder.transform);

            MoveRightObject obj = go.GetComponent<MoveRightObject>();
            
            obj.Pool = Pool;
            
            return obj;
        }

        // Object gets deactivated and removed from scene
        private void OnReturnedToPool(MoveRightObject obj)
        {
            obj.transform.SetParent(tempHolder.transform);
            obj.gameObject.SetActive(false);
        }

        // Requests an instance from the pool
        private void OnTakeFromPool(MoveRightObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        // Method called when there is no more space in the pool,
        // Destroying the returned instance to free memory
        private void OnDestroyPoolObject(MoveRightObject obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
