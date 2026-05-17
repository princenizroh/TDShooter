using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TDShooter.WaveSystem;
using TDShooter.Core;
using TDShooter.PlayerSystem;

namespace TDShooter.ObjectiveSystem
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private WaveManager _waveManager;

        public event Action GameStarted;
        public event Action<bool> GameEnded;

        private GameObject _playerInstance;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayerHealth.PlayerDied += OnPlayerDied;

            _waveManager.WavesCompleted += OnWavesCompleted;
            
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PlayerHealth.PlayerDied -= OnPlayerDied;
            _waveManager.WavesCompleted -= OnWavesCompleted;
            
        }

        public void StartGame()
        {
            if (_playerInstance == null)
            {
                _playerInstance = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                _playerInstance.SetActive(true);
                _playerInstance.transform.position = Vector3.zero;
            }

            if (_waveManager != null)
            {
                _waveManager.StartWaves();
            }
            Time.timeScale = 1f;
            GameStarted?.Invoke();
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }

        private void OnPlayerDied(PlayerHealth player)
        {
            _waveManager.StopWaves();
            GameEnded?.Invoke(false);
        }

        private void OnWavesCompleted()
        {
            GameEnded?.Invoke(true);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _playerInstance = null;
        }
    }
}
