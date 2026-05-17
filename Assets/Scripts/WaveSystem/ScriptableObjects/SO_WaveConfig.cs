using System.Collections.Generic;
using UnityEngine;

namespace TDShooter.WaveSystem
{
    [CreateAssetMenu(menuName = "TDShooter/Wave/Wave Config")]
    public class SO_WaveConfig : ScriptableObject
    {
        public string WaveName = "Wave";
        public string MusicId;
        public float StartDelay = 1f;
        public List<WaveSpawnEntry> Spawns = new List<WaveSpawnEntry>();
    }

    [System.Serializable]
    public class WaveSpawnEntry
    {
        public GameObject EnemyPrefab;
        public int Count = 3;
        public float SpawnInterval = 0.5f;
    }
}
