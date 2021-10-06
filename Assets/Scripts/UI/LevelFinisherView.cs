using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LevelFinisherView : MonoBehaviour
    {
        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private GameObject _loseGamePanel;
        [SerializeField] private GameObject _winGamePanel;
        [SerializeField] private TextMeshProUGUI _gamePanelLevelText;
        [SerializeField] private TextMeshProUGUI _winGamePanelLevelText;
        [SerializeField] private TextMeshProUGUI _loseGamePanelLevelText;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerCelebration _playerCelebration;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _nextButton;
    
        private string _currentSceneName;

        public event Action NextButtonClick;
        public event Action RetryButtonClick;

        [Inject]
        private void Construct(PlayerHealth playerHealth, PlayerCelebration playerCelebration)
        {
            _playerHealth = playerHealth;
            _playerCelebration = playerCelebration;
        }
    
        private void OnEnable()
        {
            _playerHealth.Died += OnLose;
            _playerCelebration.Celebrate += Win;
            _nextButton.onClick.AddListener(OnNextButtonClick);
            _retryButton.onClick.AddListener(OnRetryButtonClick);
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnLose;
            _playerCelebration.Celebrate -= Win;
            _nextButton.onClick.RemoveListener(OnNextButtonClick);
            _retryButton.onClick.RemoveListener(OnRetryButtonClick);
        }

        private void Start()
        {
            _currentSceneName = SceneManager.GetActiveScene().name;
            _gamePanelLevelText.text = _currentSceneName.ToUpper();
        }

        private void OnRetryButtonClick()
        {
            RetryButtonClick?.Invoke();
        }

        private void OnNextButtonClick()
        {
            NextButtonClick?.Invoke();
        }

        private void OnLose()
        {
            _gamePanel.SetActive(false);
            _loseGamePanelLevelText.text = _currentSceneName;
            _loseGamePanel.SetActive(true);
        }

        private void Win()
        {
            _gamePanel.SetActive(false);
            _winGamePanelLevelText.text = _currentSceneName;
            _winGamePanel.SetActive(true);
        }
    }
}
