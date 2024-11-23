using SingletonPattern;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class TrapSpawner : Singleton<TrapSpawner>
    {
        public GameObject trapPrefab;

        public int maxPoolSize;
        public int stackDefaultCapacity;
        
        private IObjectPool<Trap> _pool;
        private IObjectPool<Trap> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<Trap>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }

        public void SpawnTrap(Vector3 pos)
        {
            var trap = Pool.Get();
            trap.transform.position = pos;
        }
        
        
        // Callback implementation
        private Trap CreatedPooledItem()
        {
            GameObject go = Instantiate(trapPrefab, Vector3.zero, Quaternion.identity);

            Trap trap = go.GetComponent<Trap>();
            
            trap.Pool = Pool;
            
            return trap;
        }

        // Object gets deactivated and removed from scene
        private void OnReturnedToPool(Trap trap)
        {
            trap.gameObject.SetActive(false);
        }

        // Requests an instance from the pool
        private void OnTakeFromPool(Trap trap)
        {
            trap.gameObject.SetActive(true);
        }

        // Method called when there is no more space in the pool,
        // Destroying the returned instance to free memory
        private void OnDestroyPoolObject(Trap trap)
        {
            Destroy(trap.gameObject);
        }
    }
}
