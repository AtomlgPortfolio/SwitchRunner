using System;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class PlayerHealth : MonoBehaviour
    {
        private PlayerCollisionObserver _playerCollisionObserver;
        private PlayerAnimator _playerAnimator;
        private PlayerEffects _playerEffects;
        private PlayerShadow _playerShadow;
        private PlayerAudio _playerAudio;

        public event Action Died;

        [Inject]
        private void Construct(Player player)
        {
            _playerCollisionObserver = player.PlayerCollisionObserver;
            _playerAnimator = player.PlayerAnimator;
            _playerEffects = player.PlayerEffects;
            _playerShadow = player.PlayerShadow;
            _playerAudio = player.PlayerAudio;
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
            _playerAudio.PlayCollide();
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
