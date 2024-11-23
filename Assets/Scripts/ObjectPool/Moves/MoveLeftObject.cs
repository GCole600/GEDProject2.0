using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveLeftObject : MonoBehaviour
    {
        public IObjectPool<MoveLeftObject> Pool { get; set; }
        
        public void ReturnToPool()
        {
            Pool.Release(this);
        }
    }
}