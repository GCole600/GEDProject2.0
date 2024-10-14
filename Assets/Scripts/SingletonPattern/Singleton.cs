using UnityEngine;

namespace SingletonPattern
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // Checks if instance is null
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    // if it's null, creates a new instance of the GameObject
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                    }
                }

                // Returns instance
                return _instance;
            }
        }
        
        public virtual void Awake()
        {
            // Creates new instance if one doesn't exist
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            // Destroys object if an instance already exists
            else
            {
                Destroy(gameObject);
            }
        }
    }
}