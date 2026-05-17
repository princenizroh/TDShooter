using UnityEngine;

namespace TDShooter.CombatSystem
{
    [CreateAssetMenu(menuName = "TDShooter/Combat/Projectile Data")]
    public class SO_ProjectileData : ScriptableObject
    {
        public float Speed = 12f;
        public int Damage = 1;
        public float LifeTime  = 0.3f;
        public LayerMask HitMask;

    }
}
