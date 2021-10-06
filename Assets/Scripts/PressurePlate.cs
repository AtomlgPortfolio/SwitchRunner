using ActiveElements;
using DG.Tweening;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private ActiveElement[] _activatetedObstacles;
    [SerializeField] private ActiveElement[] _deactivatedObstacles;
    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private  float _pressedButtonYPosition = 0.09f;
    [SerializeField] private  float _pressedAnimationDuration = 1f;
    [SerializeField] private ParticleSystem _runeEffect;
    [SerializeField] private ParticleSystem _sparksEffect;

    private void OnTriggerEnter(Collider other)
    {
        _runeEffect.Stop();
        _sparksEffect.Play();
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
        foreach (var activatedObstacle in _activatetedObstacles)
        {
            activatedObstacle.Activate();
        }
    }

    private void PlayPressAnimation()
    {
        _buttonTransform.DOLocalMoveY(_pressedButtonYPosition,_pressedAnimationDuration);
    }
}
