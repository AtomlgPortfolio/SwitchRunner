using DG.Tweening;
using UnityEngine;

namespace LevelLogic.ActiveElements
{
    public class JumpPlatformElement : DynamicElement
    {
        [SerializeField] private float _activatedYPosition;
        [SerializeField] private float _deactivatedYPosition;

        public override void Activate()
        {
            base.Activate();
            gameObject.SetActive(true);
            transform.DOLocalMoveY(_activatedYPosition,ActivatedAnimationDuraction);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            transform
                .DOLocalMoveY(_deactivatedYPosition,DeactivatedAnimationDuration)
                .OnComplete(()=>gameObject.SetActive(false));
        }
    }
}
