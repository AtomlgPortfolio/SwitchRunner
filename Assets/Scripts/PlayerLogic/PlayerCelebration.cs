using System;
using UnityEngine;
using Zenject;

namespace PlayerLogic
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
        private PlayerAudio _playerAudio;

        public event Action Celebrate;

        [Inject]
        private void Construct(Player player)
        {
            _playerCollisionObserver = player.PlayerCollisionObserver;
            _playerAnimator = player.PlayerAnimator;
            _playerEffects = player.PlayerEffects;
            _playerAudio = player.PlayerAudio;
        }

        private void OnEnable() => 
            _playerCollisionObserver.FinishZoneEnter += OnFinishZoneEnter;

        private void OnDisable() => 
            _playerCollisionObserver.FinishZoneEnter -= OnFinishZoneEnter;

        private void OnFinishZoneEnter()
        {
            _playerAnimator.PlayCelebrate();
            _playerEffects.Stop();
            _playerAudio.Celebrate();
            Celebrate?.Invoke();
        }
    }
}
