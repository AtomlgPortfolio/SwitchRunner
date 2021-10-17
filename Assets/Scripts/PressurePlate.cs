using ActiveElements;
using DG.Tweening;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private DynamicElement[] _dynamicElements;
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
        ChangeElementActivity();
    }

    private void ChangeElementActivity()
    {
        for (int i = 0; i < _dynamicElements.Length; i++)
        {
            var dynamicElement = _dynamicElements[i];
            if (dynamicElement.Activated)
                dynamicElement.Deactivate();
            else
                dynamicElement.Activate();
        }
    }

    private void PlayPressAnimation()
    {
        _buttonTransform.DOLocalMoveY(_pressedButtonYPosition,_pressedAnimationDuration);
    }
}
