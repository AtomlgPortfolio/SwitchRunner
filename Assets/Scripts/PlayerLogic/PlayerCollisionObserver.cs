using System;
using LevelLogic;
using LevelLogic.JumpZones;
using LevelLogic.Zones;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerCollisionObserver : MonoBehaviour
    {
        public event Action ObstacleCollided;
        public event Action StartJumpZoneEnter;
        public event Action StartJumpZoneExit;
        public event Action FinishZoneEnter;
        public event Action FinishZoneExit;
        public event Action FinishJumpZoneEnter;
        public event Action PlatformStartJumpZoneEnter;
        public event Action StartJumpZonePlatformExit;
        public event Action PlatformFinishJumpZoneEnter;
        public event Action FinishJumpZonePlatformExit;
        public event Action NoPlatformJumpZoneEnter;
        public event Action PleasurePressed;
        public event Action DeathZoneEnter;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                ObstacleCollided?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out JumpZone jumpZone))
            {
                switch (jumpZone.JumpZoneType)
                {
                    case JumpZoneType.StartJumpZone:
                        StartJumpZoneEnter?.Invoke();
                        break;
                    case JumpZoneType.FinishJumpZone:
                        FinishJumpZoneEnter?.Invoke();
                        break;
                    case JumpZoneType.PlatformStartJumpZone:
                        PlatformStartJumpZoneEnter?.Invoke();
                        break;
                    case JumpZoneType.PlatformFinishJumpZone:
                        PlatformFinishJumpZoneEnter?.Invoke();
                        break;
                    case JumpZoneType.NoPlatformJumpZone:
                        NoPlatformJumpZoneEnter?.Invoke();
                        break;
                }
            }
            else if (other.TryGetComponent(out FinishZone finishZone))
            {
                FinishZoneEnter?.Invoke();
            }
            else if (other.TryGetComponent(out PressurePlate pressurePlate))
            {
                PleasurePressed?.Invoke();
            }
            else if (other.TryGetComponent(out DeathZone deathZone))
            {
                DeathZoneEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out JumpZone jumpZone))
            {
                switch (jumpZone.JumpZoneType)
                {
                    case JumpZoneType.StartJumpZone:
                        StartJumpZoneExit?.Invoke();
                        break;
                    case JumpZoneType.FinishJumpZone:
                        FinishZoneExit?.Invoke();
                        break;
                    case JumpZoneType.PlatformStartJumpZone:
                        StartJumpZonePlatformExit?.Invoke();
                        break;
                    case JumpZoneType.PlatformFinishJumpZone:
                        FinishJumpZonePlatformExit?.Invoke();
                        break;
                }
            }
        }
    }
}