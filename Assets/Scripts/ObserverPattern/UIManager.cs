using CommandPattern;
using ObjectPool.Moves;
using UnityEngine;

namespace ObserverPattern
{
    public class UIManager : Observer
    {
        [SerializeField] private GameObject contentHolder;

        private Vector3 _spawnPos = new Vector3(-25, -10, 0);

        private MoveRightPool _rightPool;
        private MoveLeftPool _leftPool;
        private MoveUpPool _upPool;
        private MoveDownPool _downPool;

        private void Start()
        {
            _rightPool = gameObject.GetComponent<MoveRightPool>();
            _leftPool = gameObject.GetComponent<MoveLeftPool>();
            _upPool = gameObject.GetComponent<MoveUpPool>();
            _downPool = gameObject.GetComponent<MoveDownPool>();
        }

        public override void Notify(Subject subject)
        {
            if (!Invoker.Instance.remove)
            {
                switch (Invoker.Instance.NewCommand)
                {
                    case MoveUp:
                        _upPool.Spawn(_spawnPos);
                        break;
                    case MoveLeft:
                        _leftPool.Spawn(_spawnPos);
                        break;
                    case MoveDown:
                        _downPool.Spawn(_spawnPos);
                        break;
                    case MoveRight:
                        _rightPool.Spawn(_spawnPos);
                        break;
                }
                
                _spawnPos += new Vector3(0, -20, 0);
            }
            else
            {
                var obj = contentHolder.transform.GetChild(contentHolder.transform.childCount - 1);

                if (obj.GetComponent<MoveRightObject>())
                    obj.GetComponent<MoveRightObject>().ReturnToPool();
                
                else if (obj.GetComponent<MoveLeftObject>())
                    obj.GetComponent<MoveLeftObject>().ReturnToPool();
                
                else if (obj.GetComponent<MoveUpObject>())
                    obj.GetComponent<MoveUpObject>().ReturnToPool();
                
                else if (obj.GetComponent<MoveDownObject>())
                    obj.GetComponent<MoveDownObject>().ReturnToPool();
                
                _spawnPos -= new Vector3(0, -20, 0);
            }
        }
    }
}