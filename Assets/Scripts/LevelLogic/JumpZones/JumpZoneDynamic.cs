using System;
using LevelLogic.ActiveElements;
using LevelLogic.JumpZones;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class JumpZoneDynamic : JumpZone
{

    [SerializeField] private DynamicElement _dynamicElement;
    
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _dynamicElement.ActivityChanged += OnDynamicElementActivityChanged;
    }

    private void OnDisable()
    {
        _dynamicElement.ActivityChanged -= OnDynamicElementActivityChanged;
    }

    private void OnDynamicElementActivityChanged(bool value)
    {
        _boxCollider.enabled = !value;
    }
}
