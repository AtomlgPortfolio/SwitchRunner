using System;
using LevelLogic.ActiveElements;
using UnityEngine;

namespace LevelLogic.JumpZones
{
    public class JumpZone : MonoBehaviour
    {
        [SerializeField] protected JumpZoneType _jumpZoneType;
        
        public JumpZoneType JumpZoneType => _jumpZoneType;
    }
}