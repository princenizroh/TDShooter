using UnityEngine;
using TDShooter.Core;

namespace TDShooter.PlayerSystem
{
    public class PlayerMovement : SingletonBehaviour<PlayerMovement>
    {
        [Header("References")]
        [SerializeField] private GameObject _muzzle;
  
        [Header("Settings")]
        [SerializeField] private SO_PlayerConfig _config;

        [HideInInspector] public bool facingUp;
        [HideInInspector] public bool facingDown;
        [HideInInspector] public bool facingLeft;
        [HideInInspector] public bool facingRight;

        private Rigidbody2D rigidBody;
        private Animator animator;
        private SpriteRenderer _muzzleRenderer;
        private SpriteRenderer _playerRenderer;
        private Vector2 _lastAimDir = Vector2.down;

        private Vector3 _boundary1;
        private Vector3 _boundary2;
        private bool _hasBounds;

        public bool canMove = true;
        public GameObject Muzzle => _muzzle;

        protected override void OnSingletonAwake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            _playerRenderer = GetComponent<SpriteRenderer>();
            _muzzleRenderer = _muzzle.GetComponentInChildren<SpriteRenderer>();
            animator.SetFloat("lastMoveY", -1f);
        }

        private void Update()
        {
            float hRaw = Input.GetAxisRaw("Horizontal");
            float vRaw = Input.GetAxisRaw("Vertical");

            if (canMove && _config != null)
            {
                rigidBody.linearVelocity = new Vector2(hRaw, vRaw).normalized * _config.MoveSpeed;
            }
            else
            {
                rigidBody.linearVelocity = Vector2.zero;
            }

            Vector2 velocity = rigidBody.linearVelocity;
            if (velocity.sqrMagnitude > 0.0001f)
            {
                bool useHorizontal = Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y);
                _lastAimDir = useHorizontal
                    ? new Vector2(Mathf.Sign(velocity.x), 0f)
                    : new Vector2(0f, Mathf.Sign(velocity.y));

                if (hRaw < 0f && vRaw > 0f)
                {
                    _lastAimDir = Vector2.up;
                }

                animator.SetFloat("lastMoveX", _lastAimDir.x);
                animator.SetFloat("lastMoveY", _lastAimDir.y);
            }

            float dirX = _lastAimDir.x;
            float dirY = _lastAimDir.y;

            if (dirY > 0f)
            {
                SetMuzzleTransform(new Vector3(.45f, -.2f, 0f), 180f, -90f);
                UpdateMuzzleSorting(true);
                facingUp = true;
                facingDown = false;
                facingLeft = false;
                facingRight = false;
            }
            else if (dirY < 0f)
            {
                SetMuzzleTransform(new Vector3(-0.2f, -.85f, 0f), 0f, -90f);
                UpdateMuzzleSorting(false);
                facingDown = true;
                facingUp = false;
                facingLeft = false;
                facingRight = false;
            }
            else if (dirX > 0f)
            {
                SetMuzzleTransform(new Vector3(.1f, -.5f, 0f), 0f, 0f);
                UpdateMuzzleSorting(false);
                facingRight = true;
                facingUp = false;
                facingDown = false;
                facingLeft = false;
            }
            else if (dirX < 0f)
            {
                SetMuzzleTransform(new Vector3(-.25f, -.5f, 0f), 180f, 180f);
                UpdateMuzzleSorting(true);
                facingLeft = true;
                facingUp = false;
                facingDown = false;
                facingRight = false;
            }


            animator.SetFloat("moveX", rigidBody.linearVelocity.x);
            animator.SetFloat("moveY", rigidBody.linearVelocity.y);


            if (_hasBounds)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, _boundary1.x, _boundary2.x),
                    Mathf.Clamp(transform.position.y, _boundary1.y, _boundary2.y),
                    transform.position.z);
            }
        }

        public void SetBounds(Vector3 bound1, Vector3 bound2)
        {
            _boundary1 = bound1 + new Vector3(.5f, 1f, 0f);
            _boundary2 = bound2 + new Vector3(-.5f, -1f, 0f);
            _hasBounds = true;
        }

        private void SetMuzzleTransform(Vector3 localPosition, float xAngle, float zAngle)
        {
            _muzzle.transform.localPosition = localPosition;
            _muzzle.transform.localRotation = Quaternion.Euler(xAngle, 0f, zAngle);
        }

        private void UpdateMuzzleSorting(bool facingUp)
        {
            _muzzleRenderer.sortingLayerID = _playerRenderer.sortingLayerID;
            _muzzleRenderer.sortingOrder = _playerRenderer.sortingOrder + (facingUp ? -1 : 1);
        }

        
    }
}
