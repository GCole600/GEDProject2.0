using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveDownObject : MonoBehaviour
    {
        public IObjectPool<MoveDownObject> Pool { get; set; }
        
        public void ReturnToPool()
        {
            Pool.Release(this);
        }
    }
}