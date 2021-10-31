using System;
using LevelLogic.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace LevelLogic
{
    public class LevelFinisher : MonoBehaviour
    {
        private string _currentSceneName;
        private LevelFinisherView _levelFinisherView;

        [Inject]
        private void Construct(LevelFinisherView levelFinisherView) => 
            _levelFinisherView = levelFinisherView;

        private void OnEnable()
        {
            _levelFinisherView.NextButtonClick += OnNextButtonClick;
            _levelFinisherView.RetryButtonClick += OnRetryButtonClick;
        }

        private void OnDisable()
        {
            _levelFinisherView.NextButtonClick -= OnNextButtonClick;
            _levelFinisherView.RetryButtonClick -= OnRetryButtonClick;
        }

        private void Start() => 
            _currentSceneName = SceneManager.GetActiveScene().name;

        private void OnRetryButtonClick() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void OnNextButtonClick()
        {
            int nextSceneNumber = GetNextSceneNumber();
            if (nextSceneNumber != -1)
            {
                string nextSceneName = GetNextSceneName(nextSceneNumber);
                LoadScene(nextSceneName);
            }
            else
            {
                throw new ArgumentException(nameof(nextSceneNumber));
            }
        }

        private static string GetNextSceneName(int nextSceneNumber)
        {
            int nextLevelNumber = nextSceneNumber + 1;
            string nextScene = $"Level {nextLevelNumber}";
            return nextScene;
        }

        private int GetNextSceneNumber()
        {
            var currentSceneName = _currentSceneName.Replace("Level ", "");
            int.TryParse(currentSceneName, out int result);
            return result;
        }

        private void LoadScene(string nextScene)
        {
            if (Application.CanStreamedLevelBeLoaded(nextScene))
                SceneManager.LoadScene(nextScene);
            else
                SceneManager.LoadScene(_currentSceneName);
        }
    }
}
