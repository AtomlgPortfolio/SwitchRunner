using LevelLogic.UI;
using LevelLogic.Zones;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace LevelLogic
{
    public class LevelProgressHandler : MonoBehaviour
    {
        private float _originDistance;
        private float _time;
        private float _fillAmount;

        private Transform _finishZoneTransform;
        private Transform _playerTransform;
        private LevelProgressView _levelProgressView;

        [Inject]
        private void Construct(LevelProgressView levelProgressView, Player player, FinishZone finishZone)
        {
            _levelProgressView = levelProgressView;
            _playerTransform = player.transform;
            _finishZoneTransform = finishZone.transform;
        }

        private void Start() => 
            _originDistance = Vector3.Distance(_playerTransform.position, _finishZoneTransform.transform.position);

        private void Update()
        {
            var distance = Vector3.Distance(_playerTransform.position, _finishZoneTransform.transform.position);
            if (distance > Mathf.Epsilon)
            {
                var value = Mathf.Lerp(_fillAmount, _playerTransform.position.z / _originDistance, _time);
                _levelProgressView.SetFillAmount(value);
                _time += Time.deltaTime;
            }
        }
    }
}