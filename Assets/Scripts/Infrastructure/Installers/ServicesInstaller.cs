using Infrastructure.Services.Input;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().FromInstance(new InputService());
    }
}
