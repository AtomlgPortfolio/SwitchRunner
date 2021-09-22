using DG.Tweening;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private ActiveElement[] _activatetedObstacles;
    [SerializeField] private ActiveElement[] _deactivatedObstacles;
    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private  float _pressedButtonYPosition = 0.09f;
    [SerializeField] private  float _pressedAnimationDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        PlayPressAnimation();
        DeactivateObstacles();
        ActivateObstacles();
    }

    private void ActivateObstacles()
    {
        foreach (var deactivatedObstacle in _deactivatedObstacles)
        {
            deactivatedObstacle.Deactivate();
        }
    }

    private void DeactivateObstacles()
    {
        foreach (var activatetedObstacle in _activatetedObstacles)
        {
            activatetedObstacle.Activate();
        }
    }

    private void PlayPressAnimation()
    {
        _buttonTransform.DOLocalMoveY(_pressedButtonYPosition,_pressedAnimationDuration);
    }
}
