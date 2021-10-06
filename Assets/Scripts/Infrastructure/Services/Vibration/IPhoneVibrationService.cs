using UnityEngine;

namespace Infrastructure.Services.Vibration
{
    public class IPhoneVibrationService : IVibrationService
    {
        public void Vibrate() => 
            Handheld.Vibrate();

        public void Cancel() {}
    }
}
