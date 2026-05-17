using UnityEngine;
using TMPro;

namespace TDShooter.CombatSystem.UI
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _lifeTime = 0.6f;
        [SerializeField] private float _floatSpeed = 1f;

        private float _timer;
        private Core.Patterns.ObjectPool _pool;

        private void Awake()
        {
            
            _text = GetComponentInChildren<TextMeshProUGUI>();
            
        }

        public void Setup(int amount)
        {
            _text.text = amount.ToString();
            _timer = _lifeTime;
        }

        public void SetPool(Core.Patterns.ObjectPool pool)
        {
            _pool = pool;
        }

        private void Update()
        {
            transform.position += Vector3.up * _floatSpeed * Time.deltaTime;
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            if (_pool == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _pool.Return(gameObject);
        }
    }
}
