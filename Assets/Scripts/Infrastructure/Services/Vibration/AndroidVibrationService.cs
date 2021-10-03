using UnityEngine;

public class AndroidVibrationService: IVibrationService
{
    private const string VibrateMethodName = "vibrate";
    private const string CancelVibrateMethodName = "cancel";
    private const long VibrationTime = 10;
    
    private AndroidJavaClass _unityPlayer;
    private AndroidJavaObject _currentActivity;
    private AndroidJavaObject _vibrator;

    public void Vibrate()
    {
        _vibrator.Call(VibrateMethodName, VibrationTime);
    }

    public void Cancel()
    {
        _vibrator.Call(CancelVibrateMethodName);
    }
}
