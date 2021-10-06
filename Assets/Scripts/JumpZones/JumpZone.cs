using UnityEngine;

namespace JumpZones
{
    public class JumpZone : MonoBehaviour
    {
        [SerializeField] private JumpZoneType _jumpZoneType;
    
        public JumpZoneType JumpZoneType => _jumpZoneType;
    }
}