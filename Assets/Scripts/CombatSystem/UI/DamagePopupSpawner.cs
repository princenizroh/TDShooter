using UnityEngine;

namespace TDShooter.CombatSystem.UI
{
    public class DamagePopupSpawner : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Core.Patterns.ObjectPool _popupPool;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 0.5f, 0f);

        public void Spawn(int amount)
        {
            DamagePopup instance = CreateInstance();
            Vector3 worldPos = transform.position + _offset;

            if (_canvas.renderMode == RenderMode.WorldSpace)
            {
                instance.transform.position = worldPos;
            }

            instance.Setup(amount);
        }

        private DamagePopup CreateInstance()
        {
            GameObject pooledObject = _popupPool.Get(Vector3.zero, Quaternion.identity);
            DamagePopup instance = pooledObject.GetComponent<DamagePopup>();
            instance.SetPool(_popupPool);
            instance.transform.SetParent(_canvas.transform, false);
            return instance;
        }
    }
}
