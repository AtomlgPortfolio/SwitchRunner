using Infrastructure.Extensions;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerCollisionObserver))]
    public class PlayerGravity : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private PlayerCollisionObserver _playerCollisionObserver;

        private void Awake()
        {
            _rigidbody = gameObject.GetRigidbody();
            _playerCollisionObserver = GetComponent<PlayerCollisionObserver>();
        }

        private void OnEnable() => 
            _playerCollisionObserver.NoPlatformJumpZoneEnter += EnableGravity;

        private void OnDisable() => 
            _playerCollisionObserver.NoPlatformJumpZoneEnter -= EnableGravity;

        private void EnableGravity() => 
            _rigidbody.useGravity = true;

        private void DisableGravity() => 
            _rigidbody.useGravity = false;
    }
}
