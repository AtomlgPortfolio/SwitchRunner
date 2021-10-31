using System;
using DG.Tweening;
using LevelLogic.ActiveElements;
using UnityEngine;

namespace LevelLogic
{
    [RequireComponent(typeof(AudioSource))]
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private DynamicElement[] _dynamicElements;
        [SerializeField] private Transform _buttonTransform;
        [SerializeField] private float _pressedButtonYPosition = 0.09f;
        [SerializeField] private float _pressedAnimationDuration = 1f;
        [SerializeField] private ParticleSystem _runeEffect;
        [SerializeField] private ParticleSystem _sparksEffect;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _runeEffect.Stop();
            _sparksEffect.Play();
            _audioSource.Play();
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
            _buttonTransform.DOLocalMoveY(_pressedButtonYPosition, _pressedAnimationDuration);
        }
    }
}