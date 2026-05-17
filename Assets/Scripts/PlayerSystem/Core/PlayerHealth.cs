using System;
using UnityEngine;
using TDShooter.Core.Interfaces;
using TDShooter.CombatSystem.UI;

namespace TDShooter.PlayerSystem
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private SO_PlayerConfig _config;
        [SerializeField] private DamagePopupSpawner _damagePopup;

        public static event Action<PlayerHealth> PlayerSpawned;
        public static event Action<PlayerHealth> PlayerDamaged;
        public static event Action<PlayerHealth> PlayerDied;

        public int CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        private void Awake()
        {
            CurrentHealth = _config != null ? _config.MaxHealth : 0;
            PlayerSpawned?.Invoke(this);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            if (_damagePopup != null)
            {
                _damagePopup.Spawn(amount);
            }

            PlayerDamaged?.Invoke(this);

            if (CurrentHealth == 0)
            {
                IsDead = true;
                PlayerDied?.Invoke(this);
                Destroy(gameObject);
            }
        }

    }
}
