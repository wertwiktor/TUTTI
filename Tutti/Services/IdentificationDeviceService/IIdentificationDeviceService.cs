using Services.IdentificationDeviceService.DataContracts;

namespace Services.IdentificationDeviceService
{
    public interface IIdentificationDeviceService
    {
        void SimulateIdentificationEvent(string identifier);

        event EventHandler<IdentificationOccuredEventArgs> IdentificationOccured;
    }
}