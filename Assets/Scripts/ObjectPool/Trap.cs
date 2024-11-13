using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class Trap : MonoBehaviour
    {
        public IObjectPool<Trap> Pool { get; set; }
        private int _toggleCounter;
        private int _movesToRemove;

        private void Awake()
        {
            _movesToRemove = Random.Range(1, 4);
            _toggleCounter = _movesToRemove;
        }

        public void ToggleTrap()
        {
            _toggleCounter -= 1;
            
            if (_toggleCounter == 0)
                ReturnToPool();
        }
        
        public void ReturnToPool()
        {
            _toggleCounter = _movesToRemove;
            Pool.Release(this);
        }
    }
}