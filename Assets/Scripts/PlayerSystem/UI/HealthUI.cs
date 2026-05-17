using UnityEngine;
using TMPro;
using TDShooter.PlayerSystem;
namespace TDShooter
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private PlayerHealth _playerHealth;

        private void OnEnable()
        {
            PlayerHealth.PlayerSpawned += OnPlayerSpawned;
            PlayerHealth.PlayerDamaged += OnPlayerDamaged;
            PlayerHealth.PlayerDied += OnPlayerDied;

            if (_playerHealth == null)
            {
                _playerHealth = FindFirstObjectByType<PlayerHealth>();
            }

            if (_playerHealth != null)
            {
                UpdateText();
            }
        }

        private void OnDisable()
        {
            PlayerHealth.PlayerSpawned -= OnPlayerSpawned;
            PlayerHealth.PlayerDamaged -= OnPlayerDamaged;
            PlayerHealth.PlayerDied -= OnPlayerDied;
        }

        private void OnPlayerSpawned(PlayerHealth player)
        {
            _playerHealth = player;
            UpdateText();
        }

        private void OnPlayerDamaged(PlayerHealth player)
        {
            if (player != _playerHealth)
            {
                return;
            }

            UpdateText();
        }

        private void OnPlayerDied(PlayerHealth player)
        {
            if (player != _playerHealth)
            {
                return;
            }

            UpdateText();
        }

        private void UpdateText()
        {
            _healthText.text = _playerHealth.CurrentHealth.ToString();
        }
    }
}
