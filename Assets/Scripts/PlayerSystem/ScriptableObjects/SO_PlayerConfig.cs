using UnityEngine;

namespace TDShooter.PlayerSystem
{
    [CreateAssetMenu(menuName = "TDShooter/Player/Player Config")]
    public class SO_PlayerConfig : ScriptableObject
    {
        public float MoveSpeed = 3f;
        public int MaxHealth = 5;
    }
}
