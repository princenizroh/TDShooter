using UnityEngine;
using UnityEngine.Tilemaps;

namespace TDShooter.PlayerSystem
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Tilemap _tilemap;

        private Transform _player;
        private Vector3 _boundary1;
        private Vector3 _boundary2;
        private float _halfHeight;
        private float _halfWidth;

        private void Start()
        {
            float targetAspect = 16f / 9f;
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float scaleHeight = windowAspect / targetAspect;

            Camera cam = GetComponent<Camera>();

            if (scaleHeight < 1f)
            {
                Rect rect = cam.rect;
                rect.width = 1f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1f - scaleHeight) / 2f;
                cam.rect = rect;
            }
            else
            {
                float scaleWidth = 1f / scaleHeight;
                Rect rect = cam.rect;
                rect.width = scaleWidth;
                rect.height = 1f;
                rect.x = (1f - scaleWidth) / 2f;
                rect.y = 0;
                cam.rect = rect;
            }

            TryResolvePlayer();

            _halfHeight = cam.orthographicSize;
            _halfWidth = _halfHeight * cam.aspect;

            if (_tilemap != null)
            {
                _tilemap.CompressBounds();
                _boundary1 = _tilemap.localBounds.min + new Vector3(_halfWidth, _halfHeight, 0f);
                _boundary2 = _tilemap.localBounds.max + new Vector3(-_halfWidth, -_halfHeight, 0f);

                if (PlayerMovement.Instance != null)
                {
                    PlayerMovement.Instance.SetBounds(_tilemap.localBounds.min, _tilemap.localBounds.max);
                }
            }
        }

        private void LateUpdate()
        {
            if (_player == null)
            {
                TryResolvePlayer();
                return;
            }

            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _boundary1.x, _boundary2.x),
                Mathf.Clamp(transform.position.y, _boundary1.y, _boundary2.y),
                transform.position.z
            );
        }

        private void TryResolvePlayer()
        {
            PlayerMovement movement = FindFirstObjectByType<PlayerMovement>();
            if (movement == null)
            {
                return;
            }

            _player = movement.transform;

            if (_tilemap != null)
            {
                _tilemap.CompressBounds();
                _boundary1 = _tilemap.localBounds.min + new Vector3(_halfWidth, _halfHeight, 0f);
                _boundary2 = _tilemap.localBounds.max + new Vector3(-_halfWidth, -_halfHeight, 0f);
                movement.SetBounds(_tilemap.localBounds.min, _tilemap.localBounds.max);
            }
        }
    }
}
