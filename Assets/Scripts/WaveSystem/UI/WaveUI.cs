using System.Collections;
using UnityEngine;
using TMPro;

namespace TDShooter.WaveSystem.UI
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _showDuration = 1.5f;

        private Coroutine _routine;

        private void Awake()
        {
            if (_label != null)
            {
                _label.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            WaveManager.Instance.WaveStarted += ShowWave;
        }

        private void OnDisable()
        {
            WaveManager.Instance.WaveStarted -= ShowWave;
        }

        public void ShowWave(string waveName, int waveNumber)
        {
            if (_label == null)
            {
                return;
            }

            string title = string.IsNullOrWhiteSpace(waveName)
                ? $"Wave {waveNumber}"
                : $"{waveName} {waveNumber}";

            _label.text = title;
            _label.gameObject.SetActive(true);

            if (_routine != null)
            {
                StopCoroutine(_routine);
            }

            _routine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_showDuration);
            _label.gameObject.SetActive(false);
            _routine = null;
        }
    }
}
