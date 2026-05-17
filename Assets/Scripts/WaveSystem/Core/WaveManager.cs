using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDShooter.EnemySystem;
using TDShooter.AudioSystem;
using System;
using TDShooter.Core;

namespace TDShooter.WaveSystem
{
    [DefaultExecutionOrder(-100)]
    public class WaveManager : SingletonBehaviour<WaveManager>
    {
        [SerializeField] private List<SO_WaveConfig> _waves = new List<SO_WaveConfig>();
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _betweenWavesDelay = 1f;
        public event Action<string, int> WaveStarted;
        private readonly HashSet<int> _waveEnemyIds = new HashSet<int>();
        private int _aliveEnemies;
        private Coroutine _waveRoutine;

        public event Action WavesCompleted;

        private void OnEnable()
        {
            EnemyHealth.EnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            EnemyHealth.EnemyDied -= OnEnemyDied;
        }

        public void StartWaves()
        {
            if (_waveRoutine != null)
            {
                StopCoroutine(_waveRoutine);
            }

            _waveRoutine = StartCoroutine(RunWaves());
        }

        public void StopWaves()
        {
            if (_waveRoutine != null)
            {
                StopCoroutine(_waveRoutine);
                _waveRoutine = null;
            }
        }

        private IEnumerator RunWaves()
        {
            for (int i = 0; i < _waves.Count; i++)
            {
                SO_WaveConfig wave = _waves[i];
                if (wave == null)
                {
                    continue;
                }

                _aliveEnemies = 0;
                _waveEnemyIds.Clear();


                WaveStarted?.Invoke(wave.WaveName, i + 1);

                if (!string.IsNullOrWhiteSpace(wave.MusicId))
                {
                    AudioManager.Instance.PlayMusicById(wave.MusicId, true);
                }
                

                if (wave.StartDelay > 0f)
                {
                    yield return new WaitForSeconds(wave.StartDelay);
                }

                foreach (WaveSpawnEntry entry in wave.Spawns)
                {
                    if (entry == null || entry.EnemyPrefab == null)
                    {
                        continue;
                    }

                    int count = Mathf.Max(0, entry.Count);
                    for (int j = 0; j < count; j++)
                    {
                        SpawnEnemy(entry.EnemyPrefab);
                        if (entry.SpawnInterval > 0f)
                        {
                            yield return new WaitForSeconds(entry.SpawnInterval);
                        }
                    }
                }

                while (_aliveEnemies > 0)
                {
                    yield return null;
                }

                if (_betweenWavesDelay > 0f)
                {
                    yield return new WaitForSeconds(_betweenWavesDelay);
                }
            }

            WavesCompleted?.Invoke();
        }

        private void SpawnEnemy(GameObject prefab)
        {
            Vector3 spawnPos = transform.position;
            if (_spawnPoints != null && _spawnPoints.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, _spawnPoints.Length);
                spawnPos = _spawnPoints[index].position;
            }

            GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity);
            EnemyHealth health = instance.GetComponent<EnemyHealth>();
            if (health != null)
            {
                int id = instance.GetInstanceID();
                _waveEnemyIds.Add(id);
                _aliveEnemies += 1;
            }
        }

        private void OnEnemyDied(EnemyHealth enemy)
        {
            if (enemy == null)
            {
                return;
            }

            int id = enemy.gameObject.GetInstanceID();
            if (_waveEnemyIds.Remove(id))
            {
                _aliveEnemies = Mathf.Max(0, _aliveEnemies - 1);
            }
        }
    }
}
