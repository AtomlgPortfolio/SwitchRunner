using DG.Tweening;
using UnityEngine;

namespace ActiveElements
{
    public class Wall : ActiveElement
    {
        [SerializeField] private float _activatedYPosition;
        [SerializeField] private float _deactivatedYPosition;
        [SerializeField] private float _activatedAnimationDuraction;
        [SerializeField] private float _deactivatedAnimationDuraction;

        public override void Deactivate()
        {
            transform.DOLocalMoveY(_deactivatedYPosition,_deactivatedAnimationDuraction);
            base.Deactivate();
        }

        public override void Activate()
        {
            base.Activate();
            transform.DOLocalMoveY(_activatedYPosition,_activatedAnimationDuraction);
        }
    }
}
