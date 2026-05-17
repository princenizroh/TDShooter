using UnityEngine;

namespace TDShooter.Core
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual bool PersistAcrossScenes => true;

        protected virtual void OnSingletonAwake()
        {
        }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this as T)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;

            if (PersistAcrossScenes)
            {
                DontDestroyOnLoad(gameObject);
            }

            OnSingletonAwake();
        }
    }
}
