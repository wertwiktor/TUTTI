using Services.IdentificationDeviceService;
using Services.IdentificationDeviceService.DataContracts;

namespace Services.IdentificationDeviceServiceBaltech
{
    public class IdentificationDeviceServiceBaltech : IIdentificationDeviceService
    {
        public event EventHandler<IdentificationOccuredEventArgs> IdentificationOccured;

        public void SimulateIdentificationEvent(string identifier)
        {
            string simulatedId;

            if (string.IsNullOrEmpty(identifier))
            {
                simulatedId = Guid.NewGuid().ToString().Replace("-",string.Empty).Substring(0,10);
            }
            else
            { 
                simulatedId= identifier;
            }

            IdentificationOccured?.Invoke(this, new IdentificationOccuredEventArgs(simulatedId));
        }
    }
}