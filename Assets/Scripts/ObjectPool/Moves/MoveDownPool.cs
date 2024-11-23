using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveDownPool : MonoBehaviour
    {
        [SerializeField] private GameObject downPrefab;
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject tempHolder;
        
        public int maxPoolSize = 15;
        public int stackDefaultCapacity = 15;

        public IObjectPool<MoveDownObject> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<MoveDownObject>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }

        private IObjectPool<MoveDownObject> _pool;
        
        // Gets an entity instance from the pool
        public void Spawn(Vector3 pos)
        {
            var obj = Pool.Get();
            obj.transform.SetParent(contentHolder.transform);
            obj.transform.localPosition = pos;
        }
        
        // Callback implementation
        private MoveDownObject CreatedPooledItem()
        {
            GameObject go = Instantiate(downPrefab, contentHolder.transform);

            MoveDownObject obj = go.GetComponent<MoveDownObject>();
            
            obj.Pool = Pool;
            
            return obj;
        }

        // Object gets deactivated and removed from scene
        private void OnReturnedToPool(MoveDownObject obj)
        {
            obj.transform.SetParent(tempHolder.transform);
            obj.gameObject.SetActive(false);
        }

        // Requests an instance from the pool
        private void OnTakeFromPool(MoveDownObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        // Method called when there is no more space in the pool,
        // Destroying the returned instance to free memory
        private void OnDestroyPoolObject(MoveDownObject obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
