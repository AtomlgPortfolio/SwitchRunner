using System;
using DG.Tweening;
using UnityEngine;

namespace LevelLogic.ActiveElements
{
    public class DynamicElement : MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private bool _activated = false;
        [SerializeField] protected float ActivatedAnimationDuraction = 0.5f;
        [SerializeField] protected float DeactivatedAnimationDuration = 0.5f;

        public event Action<bool> ActivityChanged;
        public bool Activated => _activated;
        
        public virtual void Deactivate()
        {
            _activated = false;
            ActivityChanged?.Invoke(_activated);
            for (int i = 0; i < _colliders.Length; i++) 
                _colliders[i].enabled = false;
        }

        public virtual void Activate()
        {
            _activated = true;
            ActivityChanged?.Invoke(_activated);
            for (int i = 0; i < _colliders.Length; i++) 
                _colliders[i].enabled = true;
        }
    }
}
