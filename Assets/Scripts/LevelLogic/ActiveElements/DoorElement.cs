using System;
using DG.Tweening;
using LevelLogic.ActiveElements;
using UnityEngine;

public class DoorElement : DynamicElement
{
    [SerializeField] private Transform _leftDoorRotator;
    [SerializeField] private Transform _rightDoorRotator;
    [SerializeField] private Vector3 _leftRotationDeactivationValue;
    [SerializeField] private Vector3 _rightRotationDeactivationValue;
    [SerializeField] private Vector3 _leftRotationActivationValue;
    [SerializeField] private Vector3 _rightRotationActivationValue;

    public override void Deactivate()
    {
        base.Deactivate();
        _leftDoorRotator.DORotate(_leftRotationDeactivationValue, DeactivatedAnimationDuration);
        _rightDoorRotator.DORotate(_rightRotationDeactivationValue, DeactivatedAnimationDuration);
    }

    public override void Activate()
    {
      base.Activate();
      _leftDoorRotator.DORotate(_leftRotationActivationValue, ActivatedAnimationDuraction);
       _rightDoorRotator.DORotate(_rightRotationActivationValue, ActivatedAnimationDuraction);
    }
}
