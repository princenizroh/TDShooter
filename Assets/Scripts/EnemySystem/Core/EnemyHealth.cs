using System;
using UnityEngine;
using TDShooter.Core.Interfaces;
using TDShooter.CombatSystem.UI;

namespace TDShooter.EnemySystem
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private SO_EnemyConfig _config;
        [SerializeField] private DamagePopupSpawner _damagePopup;

        public static event Action<EnemyHealth> EnemySpawned;
        public static event Action<EnemyHealth> EnemyDied;

        public int CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        private void Awake()
        {
            CurrentHealth = _config != null ? _config.MaxHealth : 0;
            EnemySpawned?.Invoke(this);
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

            Debug.Log($"[EnemyHealth] TakeDamage: {amount}. Before={CurrentHealth}");
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            Debug.Log($"[EnemyHealth] After={CurrentHealth}");

            if (_damagePopup != null)
            {
                _damagePopup.Spawn(amount);
            }

            if (CurrentHealth == 0)
            {
                IsDead = true;
                EnemyDied?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
