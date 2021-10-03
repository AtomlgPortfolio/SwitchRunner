using UnityEngine;

public class IPhoneVibrationService : IVibrationService
{
    public void Vibrate() => 
        Handheld.Vibrate();

    public void Cancel() {}
}
