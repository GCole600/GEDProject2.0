using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool.Moves
{
    public class MoveRightObject : MonoBehaviour
    {
        public IObjectPool<MoveRightObject> Pool { get; set; }
        
        public void ReturnToPool()
        {
            Pool.Release(this);
        }
    }
}