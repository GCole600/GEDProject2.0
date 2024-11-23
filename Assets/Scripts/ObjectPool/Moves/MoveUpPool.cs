using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveUpPool : MonoBehaviour
    {
        [SerializeField] private GameObject upPrefab;
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject tempHolder;
        
        public int maxPoolSize = 15;
        public int stackDefaultCapacity = 15;

        public IObjectPool<MoveUpObject> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<MoveUpObject>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }

        private IObjectPool<MoveUpObject> _pool;
        
        // Gets an entity instance from the pool
        public void Spawn(Vector3 pos)
        {
            var obj = Pool.Get();
            obj.transform.SetParent(contentHolder.transform);
            obj.transform.localPosition = pos;
        }
        
        // Callback implementation
        private MoveUpObject CreatedPooledItem()
        {
            GameObject go = Instantiate(upPrefab, contentHolder.transform);

            MoveUpObject obj = go.GetComponent<MoveUpObject>();
            
            obj.Pool = Pool;
            
            return obj;
        }

        // Object gets deactivated and removed from scene
        private void OnReturnedToPool(MoveUpObject obj)
        {
            obj.transform.SetParent(tempHolder.transform);
            obj.gameObject.SetActive(false);
        }

        // Requests an instance from the pool
        private void OnTakeFromPool(MoveUpObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        // Method called when there is no more space in the pool,
        // Destroying the returned instance to free memory
        private void OnDestroyPoolObject(MoveUpObject obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
