using UnityEngine;
using TDShooter.CombatSystem;

namespace TDShooter.PlayerSystem
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private WeaponShooter _weaponShooter;

        private void Awake()
        {
            if (_weaponShooter == null)
            {
                _weaponShooter = GetComponent<WeaponShooter>();
            }
        }

        private void Update()
        {
            if (!Input.GetButton("Fire1"))
            {
                return;
            }

            Vector2 aimDirection = GetAimDirection();
            _weaponShooter.TryShoot(aimDirection);
        }

        private static Vector2 GetAimDirection()
        {
            PlayerMovement movement = PlayerMovement.Instance;

            if (movement.facingUp)
            {
                return Vector2.up;
            }

            if (movement.facingDown)
            {
                return Vector2.down;
            }

            if (movement.facingLeft)
            {
                return Vector2.left;
            }

            if (movement.facingRight)
            {
                return Vector2.right;
            }

            return Vector2.up;
        }
    }
}
