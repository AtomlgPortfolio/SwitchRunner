using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Transform _finishZone;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Image _progressImage;

    private float _originDistance;
    private float _time;
    
    private void Start()
    {
        _originDistance = Vector3.Distance(_playerTransform.position, _finishZone.transform.position);
    }

    private void Update()
    {
        if (_progressImage.fillAmount < 1)
        {
            _progressImage.fillAmount = Mathf.Lerp(_progressImage.fillAmount,_playerTransform.position.z / _originDistance,_time);
            _time += Time.deltaTime;
        }
        
    }
}
