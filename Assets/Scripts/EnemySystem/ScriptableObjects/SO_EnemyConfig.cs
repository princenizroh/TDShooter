using UnityEngine;

namespace TDShooter.EnemySystem
{
    [CreateAssetMenu(menuName = "TDShooter/Enemy/Enemy Config")]
    public class SO_EnemyConfig : ScriptableObject
    {
        public float MoveSpeed = 2.5f;
        public float ShootRange = 12f;
        public float ShootCooldown = 0.8f;
        public int MaxHealth = 3;
    }
}
