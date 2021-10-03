using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCollisionObserver))]
public class PlayerGravity : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerMovement _playerMovement;
    private PlayerCollisionObserver _playerCollisionObserver;

    private void Awake()
    {
        _rigidbody = gameObject.GetRigidbody();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCollisionObserver = GetComponent<PlayerCollisionObserver>();
    }

    private void OnEnable()
    {
        _playerMovement.Jumped += EnableGravity;
        _playerCollisionObserver.StartJumpZoneEnter += DisableGravity;
        _playerCollisionObserver.FinishJumpZoneEnter += DisableGravity;
    }

    private void OnDisable()
    {
        _playerMovement.Jumped -= EnableGravity;
        _playerCollisionObserver.StartJumpZoneEnter -= DisableGravity;
        _playerCollisionObserver.FinishJumpZoneEnter -= DisableGravity;
    }

    private void EnableGravity()
    {
        _rigidbody.useGravity = true;
    }

    private void DisableGravity()
    {
        _rigidbody.useGravity = false;
    }
}
