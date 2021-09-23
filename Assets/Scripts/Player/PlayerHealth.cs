using System;
using UnityEngine;

[RequireComponent(typeof(PlayerObserver))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerEffects))]
public class PlayerHealth : MonoBehaviour
{
    private PlayerObserver _playerObserver;
    private PlayerAnimator _playerAnimator;
    private PlayerEffects _playerEffects;

    public event Action Died;

    private void Awake()
    {
        _playerObserver = GetComponent<PlayerObserver>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerEffects = GetComponent<PlayerEffects>();
    }

    private void OnEnable()
    {
        _playerObserver.ObstacleCollided += OnObstacleCollided;
    }

    private void OnDisable()
    {
        _playerObserver.ObstacleCollided -= OnObstacleCollided;
    }

    private void OnObstacleCollided()
    {
        Die();
    }

    private void Die()
    {
        _playerEffects.Stop();
        _playerAnimator.PlayDie();
        Died?.Invoke();
    }
}
