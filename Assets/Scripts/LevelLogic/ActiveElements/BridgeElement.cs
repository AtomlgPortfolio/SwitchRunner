using DG.Tweening;
using LevelLogic.ActiveElements;
using UnityEngine;

public class BridgeElement : DynamicElement
{
    [SerializeField] private Quaternion _rotationActivationValue;
    [SerializeField] private Quaternion _rotationDeactivationValue;
    [SerializeField] private Transform _bridgeRotator;
    
    public override void Deactivate()
    {
        base.Deactivate();
        _bridgeRotator.DORotateQuaternion(_rotationDeactivationValue, DeactivatedAnimationDuration);
        

    }

    public override void Activate()
    {
        base.Activate();
        _bridgeRotator.DORotateQuaternion(_rotationActivationValue, DeactivatedAnimationDuration);
    }
}
