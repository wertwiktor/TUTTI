using Serilog;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceService.DataContracts;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Services.IdentificationDeviceServiceBaltech
{
    public class IdentificationDeviceServiceBaltech : IIdentificationDeviceService, IDisposable
    {
        private readonly ILogger _logger = Log.ForContext<IdentificationDeviceServiceBaltech>();

        private BaltechLib _baltechReader;

        public event EventHandler<IdentificationOccuredEventArgs> IdentificationOccured;

        public IdentificationDeviceServiceBaltech()
        {
            _baltechReader = new BaltechLib();
            _baltechReader.IdentificationOccuredInBaltechLib += OnBaltechReaderIdentificationOccured;
        }

        private void OnBaltechReaderIdentificationOccured(object? sender, IdentificationOccuredEventArgs e)
        {
            IdentificationOccured?.Invoke(this, e);
        }

        public void Dispose()
        {
            _baltechReader.IdentificationOccuredInBaltechLib -= OnBaltechReaderIdentificationOccured;
            _baltechReader.CloseReaderComPort();
        }

        public void SimulateIdentificationEvent(string identifier)
        {
            string simulatedId;

            if (string.IsNullOrEmpty(identifier))
            {
                simulatedId = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            }
            else
            {
                simulatedId = identifier;
            }

            _logger.Debug("Simulating identifier: {identifier}", simulatedId);

            IdentificationOccured?.Invoke(this, new IdentificationOccuredEventArgs(simulatedId));
        }
    }
}