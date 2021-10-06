namespace Infrastructure.Services.Vibration
{
    public interface IVibrationService
    {
        void Vibrate();
        void Cancel();
    }
}
