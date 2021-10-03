using UnityEngine;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().FromInstance(InputService()).AsSingle();
        Container.Bind<IVibrationService>().FromInstance(VibrationService()).AsSingle();
    }
    
    private IInputService InputService()
    {
        if (Application.isEditor)
            return new StandaloneInputService();
            
        return new MobileInputService();
    }
    
    private IVibrationService VibrationService()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return new AndroidVibrationService();
#else
        return new IPhoneVibrationService();
#endif
    }
}
