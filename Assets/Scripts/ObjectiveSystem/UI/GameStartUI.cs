using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TDShooter.PlayerSystem;

namespace TDShooter.ObjectiveSystem.UI
{
    public class GameStartUI : MonoBehaviour
    {
        [SerializeField] private GameObject _Panel;
        [SerializeField] private TMP_Text _endTitle;
        [SerializeField] private Button _actionButton;
        [SerializeField] private TMP_Text _actionLabel;

        private bool _isGameOver;
        
        

        private void Awake()
        {
            _Panel.SetActive(true);
            _actionLabel.text = "Play";
            _isGameOver = false;
        }

        private void Start()
        {
            _actionButton.onClick.AddListener(OnActionClicked);
        }

        private void OnEnable()
        {
            GameManager.Instance.GameStarted += OnGameStarted;
            GameManager.Instance.GameEnded += OnGameEnded;
        }

        private void OnDisable()
        {
            GameManager.Instance.GameStarted -= OnGameStarted;
            GameManager.Instance.GameEnded -= OnGameEnded;
        }

        public void OnActionClicked()
        {
            if (_isGameOver)
            {
                if (PlayerMovement.Instance != null)
                {
                    Destroy(PlayerMovement.Instance.gameObject);
                }

                GameManager.Instance.RestartGame();
            }
            else
            {
                GameManager.Instance.StartGame();
            }
        }

        private void OnGameStarted()
        {
            _Panel.SetActive(false);
            _actionLabel.text = "Play";
            _isGameOver = false;
        }

        private void OnGameEnded(bool win)
        {
            _Panel.SetActive(true);
            _endTitle.text = win ? "Win" : "Lose";
            _actionLabel.text = win ? "Play Again" : "Restart";
            _isGameOver = true;
            Time.timeScale = 0f;
        }
    }
}
