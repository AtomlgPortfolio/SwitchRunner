using System;
using UnityEngine;

[RequireComponent(typeof(PlayerObserver))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerCelebration : MonoBehaviour
{
    private PlayerObserver _playerObserver;
    private PlayerAnimator _playerAnimator;

    public event Action Celebrate;

    private void Awake()
    {
        _playerObserver = GetComponent<PlayerObserver>();
        _playerAnimator = GetComponent<PlayerAnimator>();
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
    }
}
