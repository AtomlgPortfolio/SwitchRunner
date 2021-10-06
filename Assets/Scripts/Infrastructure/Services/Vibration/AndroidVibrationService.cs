using UnityEngine;

namespace Infrastructure.Services.Vibration
{
    public class AndroidVibrationService : IVibrationService
    {
        private const string VibrateMethodName = "vibrate";
        private const string CancelVibrateMethodName = "cancel";
        private const long VibrationTime = 25;

        private AndroidJavaClass _unityPlayer;
        private AndroidJavaObject _currentActivity;
        private AndroidJavaObject _vibrator;

        public AndroidVibrationService()
        {
            _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _vibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        }

        public void Vibrate() => 
            _vibrator.Call(VibrateMethodName, VibrationTime);

        public void Cancel() => 
            _vibrator.Call(CancelVibrateMethodName);
    }
}