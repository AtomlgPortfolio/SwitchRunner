using DG.Tweening;
using UnityEngine;

public class JumpPlatform : ActiveElement
{
    [SerializeField] private float _activatedYPosition;
    [SerializeField] private float _deactivatedYPosition;
    [SerializeField] private float _activatedAnimationDuraction;
    [SerializeField] private float _deactivatedAnimationDuraction;
    public override void Activate()
    {
        gameObject.SetActive(true);
        transform.DOLocalMoveY(_activatedYPosition,_activatedAnimationDuraction);
    }

    public override void Deactivate()
    {
        transform
            .DOLocalMoveY(_deactivatedYPosition,_deactivatedAnimationDuraction)
            .OnComplete(()=>gameObject.SetActive(false));
        
    }
}
