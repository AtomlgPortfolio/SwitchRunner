using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

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
        var currentSceneName = _currentSceneName.Replace(" ", "");
        int.TryParse(currentSceneName, out int result);
        int nextLevel = result + 1;
        string nextScene = $"Level {nextLevel}";
        
        Scene scene = SceneManager.GetSceneByName(nextScene);
        SceneManager.LoadScene(scene.IsValid() ? nextScene : _currentSceneName);
    }
}
