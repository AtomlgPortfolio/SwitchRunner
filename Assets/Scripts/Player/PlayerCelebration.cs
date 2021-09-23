using System;
using UnityEngine;

[RequireComponent(typeof(PlayerObserver))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerEffects))]
public class PlayerCelebration : MonoBehaviour
{
    private PlayerObserver _playerObserver;
    private PlayerAnimator _playerAnimator;
    private PlayerEffects _playerEffects;

    public event Action Celebrate;

    private void Awake()
    {
        _playerObserver = GetComponent<PlayerObserver>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerEffects = GetComponent<PlayerEffects>();
        
    }

    private void OnEnable()
    {
        _playerObserver.FinishZoneEnter += OnFinishZoneEnter;
    }

    private void OnDisable()
    {
        _playerObserver.FinishZoneEnter -= OnFinishZoneEnter;
    }
    
    private void OnFinishZoneEnter()
    {
        _playerAnimator.PlayCelebrate();
        Celebrate?.Invoke();
        _playerEffects.Stop();
    }
}
