using System.Collections.Generic;
using UnityEngine;

namespace TDShooter.Core.Patterns
{
    public class ObjectPool : MonoBehaviour
    {
        private static readonly Dictionary<string, ObjectPool> Pools = new Dictionary<string, ObjectPool>();

        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize = 12;
        [SerializeField] private string poolId;

        private readonly Queue<GameObject> pool = new Queue<GameObject>();

        private void Awake()
        {
            RegisterPool();
            Prewarm();
        }

        private void OnDestroy()
        {
            UnregisterPool();
        }

        public static ObjectPool GetPool(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            Pools.TryGetValue(id, out ObjectPool pool);
            return pool;
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            if (pool.Count == 0)
            {
                CreateInstance();
            }

            var instance = pool.Dequeue();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.SetActive(true);
            return instance;
        }

        public void Return(GameObject instance)
        {
            if (instance == null)
            {
                return;
            }

            instance.SetActive(false);
            pool.Enqueue(instance);
        }

        private void Prewarm()
        {
            for (var i = 0; i < initialSize; i++)
            {
                CreateInstance();
            }
        }

        private void RegisterPool()
        {
            if (string.IsNullOrWhiteSpace(poolId))
            {
                return;
            }

            Pools[poolId] = this;
        }

        private void UnregisterPool()
        {
            if (string.IsNullOrWhiteSpace(poolId))
            {
                return;
            }

            if (Pools.TryGetValue(poolId, out ObjectPool pool) && pool == this)
            {
                Pools.Remove(poolId);
            }
        }

        private void CreateInstance()
        {
            if (prefab == null)
            {
                return;
            }

            var instance = Instantiate(prefab, transform);
            instance.SetActive(false);
            pool.Enqueue(instance);
        }
    }
}
