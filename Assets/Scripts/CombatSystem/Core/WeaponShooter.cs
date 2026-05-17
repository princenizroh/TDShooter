using UnityEngine;
using TDShooter.Core.Patterns;

namespace TDShooter.CombatSystem
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private SO_WeaponData _weaponData;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private ObjectPool _projectilePool;
        [SerializeField] private string _projectilePoolId;

        private float _cooldown;

        private void Update()
        {
            if (_cooldown > 0f)
            {
                _cooldown -= Time.deltaTime;
            }
        }

        private void Awake()
        {
            if (_projectilePool == null && !string.IsNullOrWhiteSpace(_projectilePoolId))
            {
                _projectilePool = ObjectPool.GetPool(_projectilePoolId);
            }
        }

        public void TryShoot(Vector2 aimDirection)
        {

            if (_cooldown > 0f)
            {
                return;
            }

            Vector2 dir = aimDirection.normalized;
            GameObject instanceObject = _projectilePool.Get(_muzzle.position, Quaternion.identity);
            Projectile instance = instanceObject.GetComponent<Projectile>();
            instance.SetPool(_projectilePool);
            instance.Launch(dir);

            _cooldown = 1f / Mathf.Max(0.01f, _weaponData.FireRate);
        }
    }
}
