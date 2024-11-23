using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveUpObject : MonoBehaviour
    {
        public IObjectPool<MoveUpObject> Pool { get; set; }
        
        public void ReturnToPool()
        {
            Pool.Release(this);
        }
    }
}