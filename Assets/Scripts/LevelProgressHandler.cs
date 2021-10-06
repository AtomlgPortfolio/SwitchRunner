using UI;
using UnityEngine;
using Zenject;

public class LevelProgressHandler : MonoBehaviour
{
    [SerializeField] private Transform _finishZone;
    [SerializeField] private Transform _playerTransform;

    private float _originDistance;
    private float _time;
    private LevelProgressView _levelProgressView;
    private float fillAmount;

    [Inject]
    private void Construct(LevelProgressView levelProgressView) => 
        _levelProgressView = levelProgressView;

    private void Start() => 
        _originDistance = Vector3.Distance(_playerTransform.position, _finishZone.transform.position);

    private void Update()
    {
        var distance = Vector3.Distance(_playerTransform.position, _finishZone.transform.position);
        if (distance > Mathf.Epsilon)
        {
            var value = Mathf.Lerp(fillAmount, _playerTransform.position.z / _originDistance, _time);
            _levelProgressView.SetFillAmount(value);
            _time += Time.deltaTime;
        }
    }
}