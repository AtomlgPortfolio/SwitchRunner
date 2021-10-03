using UnityEngine;
using Zenject;

public class VibrationHandler : MonoBehaviour
{
    private IVibrationService _vibrationService;
    private PlayerHealth _playerHealth;
    private PlayerCollisionObserver _playerCollisionObserver;

    [Inject]
    private void Construct(IVibrationService vibrationService, PlayerHealth playerHealth, PlayerCollisionObserver playerCollisionObserver)
    {
        _playerCollisionObserver = playerCollisionObserver;
        _playerHealth = playerHealth;
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
