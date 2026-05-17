using UnityEngine;
using TDShooter.Core.Interfaces;
using TDShooter.Core.Patterns;

namespace TDShooter.CombatSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SO_ProjectileData _data;

        private Rigidbody2D _rigidbody2D;
        private float _timeRemaining;
        private ObjectPool _pool;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _timeRemaining = _data != null ? _data.LifeTime : 0f;
        }

        private void Update()
        {
            if (_timeRemaining <= 0f)
            {
                ReturnToPool();
                return;
            }

            _timeRemaining -= Time.deltaTime;
        }

        public void Launch(Vector2 direction)
        {
            _rigidbody2D.linearVelocity = direction.normalized * _data.Speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_data.HitMask.value & (1 << other.gameObject.layer)) == 0)
            {
                return;
            }

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_data.Damage);
            }

            ReturnToPool();
        }

        public void SetPool(ObjectPool pool)
        {
            _pool = pool;
        }

        private void ReturnToPool()
        {
            if (_pool == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _pool.Return(gameObject);
        }
    }
}
