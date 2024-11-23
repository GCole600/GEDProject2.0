using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveLeftPool : MonoBehaviour
    {
        [SerializeField] private GameObject leftPrefab;
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject tempHolder;
        
        public int maxPoolSize = 15;
        public int stackDefaultCapacity = 15;

        public IObjectPool<MoveLeftObject> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<MoveLeftObject>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }

        private IObjectPool<MoveLeftObject> _pool;
        
        // Gets an entity instance from the pool
        public void Spawn(Vector3 pos)
        {
            var obj = Pool.Get();
            obj.transform.SetParent(contentHolder.transform);
            obj.transform.localPosition = pos;
        }
        
        // Callback implementation
        private MoveLeftObject CreatedPooledItem()
        {
            GameObject go = Instantiate(leftPrefab, contentHolder.transform);

            MoveLeftObject obj = go.GetComponent<MoveLeftObject>();
            
            obj.Pool = Pool;
            
            return obj;
        }

        // Object gets deactivated and removed from scene
        private void OnReturnedToPool(MoveLeftObject obj)
        {
            obj.transform.SetParent(tempHolder.transform);
            obj.gameObject.SetActive(false);
        }

        // Requests an instance from the pool
        private void OnTakeFromPool(MoveLeftObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        // Method called when there is no more space in the pool,
        // Destroying the returned instance to free memory
        private void OnDestroyPoolObject(MoveLeftObject obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
