using System;
using UnityEngine;

[RequireComponent(typeof(PlayerCollisionObserver))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerEffects))]
[RequireComponent(typeof(PlayerShadow))]
public class PlayerCelebration : MonoBehaviour
{
    [SerializeField] private Vector3 _capsuleColliderCelebratePosition;
    [SerializeField] private Vector3 _shadowPosition;
    
    private PlayerCollisionObserver _playerCollisionObserver;
    private PlayerAnimator _playerAnimator;
    private PlayerEffects _playerEffects;
    private PlayerShadow _playerShadow;

    public event Action Celebrate;

    private void Awake()
    {
        _playerCollisionObserver = GetComponent<PlayerCollisionObserver>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerEffects = GetComponent<PlayerEffects>();
        _playerShadow = GetComponent<PlayerShadow>();
    }

    private void OnEnable()
    {
        _playerCollisionObserver.FinishZoneEnter += OnFinishZoneEnter;
    }

    private void OnDisable()
    {
        _playerCollisionObserver.FinishZoneEnter -= OnFinishZoneEnter;
    }
    
    private void OnFinishZoneEnter()
    {
        _playerAnimator.PlayCelebrate();
        _playerShadow.SetPosition(_shadowPosition);
        Celebrate?.Invoke();
        _playerEffects.Stop();
    }
}
