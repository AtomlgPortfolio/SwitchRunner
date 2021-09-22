using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Transform _finishZone;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Image _progressImage;

    private float _originDistance;
    
    private void Start()
    {
        _originDistance = Vector3.Distance(_playerTransform.position, _finishZone.transform.position);
    }

    private void Update()
    {
        _progressImage.fillAmount = _playerTransform.position.z / _originDistance;
    }
}
