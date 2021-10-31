using Infrastructure.Services.Vibration;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Handlers
{
    public class VibrationHandler : MonoBehaviour
    {
        private IVibrationService _vibrationService;
        private PlayerHealth _playerHealth;
        private PlayerCollisionObserver _playerCollisionObserver;

        [Inject]
        private void Construct(IVibrationService vibrationService, Player player)
        {
            _playerCollisionObserver = player.PlayerCollisionObserver;
            _playerHealth = player.PlayerHealth;
            _vibrationService = vibrationService;
        }

        private void OnEnable()
        {
            _playerCollisionObserver.PleasurePressed += _vibrationService.Vibrate;
            _playerCollisionObserver.FinishJumpZoneEnter += _vibrationService.Vibrate;
            _playerHealth.Died += _vibrationService.Vibrate;
        }

        private void OnDisable()
        {
            _playerCollisionObserver.PleasurePressed -= _vibrationService.Vibrate;
            _playerCollisionObserver.FinishJumpZoneEnter -= _vibrationService.Vibrate;
            _playerHealth.Died -= _vibrationService.Vibrate;
        }
    }
}
