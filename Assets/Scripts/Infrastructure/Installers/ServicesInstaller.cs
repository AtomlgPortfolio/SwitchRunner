using Infrastructure.Services.Input;
using Infrastructure.Services.Vibration;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
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
            if (Application.platform == RuntimePlatform.Android)
                return new AndroidVibrationService();
        
            return new IPhoneVibrationService();
        }
    }
}
