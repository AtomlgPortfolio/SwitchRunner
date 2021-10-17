using DG.Tweening;
using UnityEngine;

namespace ActiveElements
{
    public class DynamicElement : MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private float _activatedYPosition;
        [SerializeField] private float _deactivatedYPosition;
        [SerializeField] private float _activatedAnimationDuraction = 0.5f;
        [SerializeField] private float _deactivatedAnimationDuraction = 0.5f;
        [SerializeField] private bool _activated = false;
        
        public bool Activated => _activated;
        
        public virtual void Deactivate()
        {
            _activated = false;
            transform.DOLocalMoveY(_deactivatedYPosition,_deactivatedAnimationDuraction);
            for (int i = 0; i < _colliders.Length; i++) 
                _colliders[i].enabled = false;
        }

        public virtual void Activate()
        {
            _activated = true;
            for (int i = 0; i < _colliders.Length; i++) 
                _colliders[i].enabled = true;
            
            transform.DOLocalMoveY(_activatedYPosition,_activatedAnimationDuraction);
        }
    }
}
