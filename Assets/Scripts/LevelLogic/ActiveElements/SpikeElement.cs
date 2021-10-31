using DG.Tweening;
using LevelLogic.ActiveElements;
using UnityEngine;

public class SpikeElement : DynamicElement
{
    [SerializeField] private float _activatedYPosition;
    [SerializeField] private float _deactivatedYPosition;
    
    public override void Activate()
    {
        base.Activate();
        transform.DOLocalMoveY(_activatedYPosition,ActivatedAnimationDuraction);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        transform.DOLocalMoveY(_deactivatedYPosition, DeactivatedAnimationDuration);
    }

}
