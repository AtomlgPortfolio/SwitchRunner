using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
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
        _currentSceneName = SceneManager.GetActiveScene().name.ToUpper();
        _gamePanelLevelText.text = _currentSceneName;
    }

    private void OnRetryButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnNextButtonClick()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        _currentSceneName = sceneName.Substring(5);
        int.TryParse(_currentSceneName, out int result);
        int nextLevel = result + 1;
        string nextScene = $"Level {nextLevel}";
        

        Scene scene = SceneManager.GetSceneByName(nextScene);
        if (scene.IsValid())
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogError("Нет следующей сцены");
        }
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
