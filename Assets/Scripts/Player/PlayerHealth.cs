using System;
using UnityEngine;

[RequireComponent(typeof(PlayerObserver))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerHealth : MonoBehaviour
{
    private PlayerObserver _playerObserver;
    private PlayerAnimator _playerAnimator;

    public event Action Died;

    private void Awake()
    {
        _playerObserver = GetComponent<PlayerObserver>();
        _playerAnimator = GetComponent<PlayerAnimator>();
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
        _playerAnimator.PlayDie();
        Died?.Invoke();
    }
}
