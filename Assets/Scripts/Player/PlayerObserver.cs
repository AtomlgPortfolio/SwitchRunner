using System;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    public event Action ObstacleCollided;
    public event Action StartJumpZoneEnter;
    public event Action PlatformJumpZoneEnter;
    public event Action PlatformJumpZoneExit;
    public event Action FinishJumpZoneEnter;
    public event Action StartJumpZoneExit;
    public event Action FinishZoneEnter;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            ObstacleCollided?.Invoke();
        }
        else if (other.gameObject.TryGetComponent(out JumpPlatformZone jumpPlatformZone))
        {
            PlatformJumpZoneEnter?.Invoke();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out JumpPlatformZone jumpPlatformZone))
        {
            PlatformJumpZoneExit?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StartJumpZone startJumpZone))
        {
            StartJumpZoneEnter?.Invoke();
        }
        else if (other.TryGetComponent(out FinishJumpZone finishJumpZone))
        {
            FinishJumpZoneEnter?.Invoke();
        }
        else if(other.TryGetComponent(out FinishZone finishZone))
        {
            FinishZoneEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out StartJumpZone startJumpZone))
        {
            StartJumpZoneExit?.Invoke();
        }
    }
}
