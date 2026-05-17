using UnityEngine;
using TDShooter.CombatSystem;

namespace TDShooter.EnemySystem
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private WeaponShooter _weaponShooter;
        [SerializeField] private SO_EnemyConfig _config;
        [SerializeField] private string _playerTag = "Player";

        private Transform _target;
        private float _cooldown;

        private void Awake()
        {
            if (_weaponShooter == null)
            {
                _weaponShooter = GetComponent<WeaponShooter>();
            }
        }

        private void Update()
        {
            if (_cooldown > 0f)
            {
                _cooldown -= Time.deltaTime;
            }

            ResolveTarget();
            if (_target == null)
            {
                return;
            }

            Vector2 direction = _target.position - transform.position;


            if (direction.sqrMagnitude > _config.ShootRange * _config.ShootRange)
            {
                return;
            }

            if (_cooldown > 0f)
            {
                return;
            }

            _weaponShooter.TryShoot(direction);
            if (_config != null)
            {
                _cooldown = Mathf.Max(0f, _config.ShootCooldown);
            }
        }

        private void ResolveTarget()
        {
            if (_target != null)
            {
                return;
            }

            GameObject playerObject = GameObject.FindWithTag(_playerTag);
            if (playerObject != null)
            {
                _target = playerObject.transform;
            }
        }
    }
}
