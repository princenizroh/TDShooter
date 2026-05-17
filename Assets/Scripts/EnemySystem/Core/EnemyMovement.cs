using UnityEngine;

namespace TDShooter.EnemySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private SO_EnemyConfig _config;
        [SerializeField] private GameObject _muzzle;
        [SerializeField] private string _playerTag = "Player";

        [HideInInspector] public bool facingUp;
        [HideInInspector] public bool facingDown;
        [HideInInspector] public bool facingLeft;
        [HideInInspector] public bool facingRight;

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _muzzleRenderer;
        private SpriteRenderer _enemyRenderer;
        private Transform _target;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _enemyRenderer = GetComponent<SpriteRenderer>();
            _muzzleRenderer = _muzzle.GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            ResolveTarget();
        }

        private void FixedUpdate()
        {
            if (_target == null || _config == null)
            {
                _rigidbody2D.linearVelocity = Vector2.zero;
                return;
            }

            Vector2 toTarget = _target.position - transform.position;

            Vector2 direction = toTarget.normalized;
            _rigidbody2D.linearVelocity = direction * _config.MoveSpeed;

            bool useHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
            float hRaw = useHorizontal ? Mathf.Sign(direction.x) : 0f;
            float vRaw = useHorizontal ? 0f : Mathf.Sign(direction.y);

            if (_animator != null)
            {
                _animator.SetFloat("moveX", _rigidbody2D.linearVelocity.x);
                _animator.SetFloat("moveY", _rigidbody2D.linearVelocity.y);
                _animator.SetFloat("lastMoveX", hRaw);
                _animator.SetFloat("lastMoveY", vRaw);
            }

            if (vRaw > 0f)
            {
                SetMuzzleTransform(new Vector3(.45f, -.2f, 0f), 180f, -90f);
                UpdateMuzzleSorting(true);
                facingUp = true;
                facingDown = false;
                facingLeft = false;
                facingRight = false;
            }
            else if (vRaw < 0f)
            {
                SetMuzzleTransform(new Vector3(-0.2f, -.85f, 0f), 0f, -90f);
                UpdateMuzzleSorting(false);
                facingDown = true;
                facingUp = false;
                facingLeft = false;
                facingRight = false;
            }
            else if (hRaw > 0f)
            {
                SetMuzzleTransform(new Vector3(.1f, -.5f, 0f), 0f, 0f);
                UpdateMuzzleSorting(false);
                facingRight = true;
                facingUp = false;
                facingDown = false;
                facingLeft = false;
            }
            else if (hRaw < 0f)
            {
                SetMuzzleTransform(new Vector3(-.25f, -.5f, 0f), 180f, 180f);
                UpdateMuzzleSorting(true);
                facingLeft = true;
                facingUp = false;
                facingDown = false;
                facingRight = false;
            }
        }

        private void ResolveTarget()
        {
            if (_target != null)
            {
                return;
            }

            GameObject playerObject = GameObject.FindWithTag(_playerTag);
            if (playerObject != null)
            {
                _target = playerObject.transform;
            }
        }

        private void SetMuzzleTransform(Vector3 localPosition, float xAngle, float zAngle)
        {
            _muzzle.transform.localPosition = localPosition;
            _muzzle.transform.localRotation = Quaternion.Euler(xAngle, 0f, zAngle);
        }

        private void UpdateMuzzleSorting(bool facingUp)
        {
            _muzzleRenderer.sortingLayerID = _enemyRenderer.sortingLayerID;
            _muzzleRenderer.sortingOrder = _enemyRenderer.sortingOrder + (facingUp ? -1 : 1);
        }
    }
}
