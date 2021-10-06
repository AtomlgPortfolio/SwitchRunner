using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerCollisionObserver))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerEffects))]
    [RequireComponent(typeof(PlayerShadow))]
    public class PlayerCelebration : MonoBehaviour
    {
        private PlayerCollisionObserver _playerCollisionObserver;
        private PlayerAnimator _playerAnimator;
        private PlayerEffects _playerEffects;

        public event Action Celebrate;

        private void Awake()
        {
            _playerCollisionObserver = GetComponent<PlayerCollisionObserver>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerEffects = GetComponent<PlayerEffects>();
        }

        private void OnEnable() => 
            _playerCollisionObserver.FinishZoneEnter += OnFinishZoneEnter;

        private void OnDisable() => 
            _playerCollisionObserver.FinishZoneEnter -= OnFinishZoneEnter;

        private void OnFinishZoneEnter()
        {
            _playerAnimator.PlayCelebrate();
            _playerEffects.Stop();
            Celebrate?.Invoke();
        }
    }
}
