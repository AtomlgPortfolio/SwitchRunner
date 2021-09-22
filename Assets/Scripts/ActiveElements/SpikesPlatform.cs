using DG.Tweening;
using UnityEngine;

public class SpikesPlatform : ActiveElement
{
    [SerializeField] private float _activatedYPosition;
    [SerializeField] private float _deactivatedYPosition;
    [SerializeField] private Transform _spikesContainerTransofrm;
    [SerializeField] private float _activatedAnimationDuraction;
    [SerializeField] private float _deactivatedAnimationDuraction;

    public override void Deactivate()
    {
        _spikesContainerTransofrm.DOLocalMoveY(_deactivatedYPosition,_deactivatedAnimationDuraction);
        base.Deactivate();
    }

    public override void Activate()
    {
        _spikesContainerTransofrm.DOLocalMoveY(_activatedYPosition,_activatedAnimationDuraction);
    }
}