using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerCollisionObserver))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerEffects))]
    [RequireComponent(typeof(PlayerShadow))]
    public class PlayerHealth : MonoBehaviour
    {
        private PlayerCollisionObserver _playerCollisionObserver;
        private PlayerAnimator _playerAnimator;
        private PlayerEffects _playerEffects;
        private PlayerShadow _playerShadow;

        public event Action Died;

        private void Awake()
        {
            _playerCollisionObserver = GetComponent<PlayerCollisionObserver>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerEffects = GetComponent<PlayerEffects>();
            _playerShadow = GetComponent<PlayerShadow>();
        }

        private void OnEnable()
        {
            _playerCollisionObserver.ObstacleCollided += OnObstacleCollided;
            _playerCollisionObserver.DeathZoneEnter += OnDeathZoneEnter;
        }

        private void OnDisable()
        {
            _playerCollisionObserver.ObstacleCollided -= OnObstacleCollided;
            _playerCollisionObserver.DeathZoneEnter -= OnObstacleCollided;
        }

        private void OnObstacleCollided()
        {
            _playerCollisionObserver.ObstacleCollided -= OnObstacleCollided;
            _playerAnimator.PlayDie();
            Die();
        }

        private void OnDeathZoneEnter()
        {
            _playerCollisionObserver.ObstacleCollided -= OnDeathZoneEnter;
            Die();
        }

        private void Die()
        {
            _playerEffects.Stop();
            _playerShadow.Disable();
            Died?.Invoke();
        }
    }
}
