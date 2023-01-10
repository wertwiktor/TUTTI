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

        private const int BRP_OK = 0;

        private IntPtr _brpDev;

        [DllImport("brp_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr brp_create();

        [DllImport("brp_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr brp_create_usb_hid(long snr);

        [DllImport("brp_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint brp_set_io(IntPtr protocol, IntPtr io_protocol);

        [DllImport("brp_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint brp_open(IntPtr protocol);

        [DllImport("brp_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint brp_close(IntPtr protocol);

        [DllImport("brp_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint brp_exec_cmd(
          IntPtr protocol,
          int cmd_code,
          byte[] param_buf,
          UIntPtr param_len,
          byte[] resp_buf,
          UIntPtr max_resp_len,
          ref UIntPtr resp_len,
          uint cmd_timeout);

        public event EventHandler<IdentificationOccuredEventArgs> IdentificationOccured;

        public IdentificationDeviceServiceBaltech()
        {
            _brpDev = IdentificationDeviceServiceBaltech.brp_create();
            if (IdentificationDeviceServiceBaltech.brp_set_io(_brpDev, IdentificationDeviceServiceBaltech.brp_create_usb_hid(0L)) == 0U && IdentificationDeviceServiceBaltech.brp_open(_brpDev) == 0U)
            {
                UIntPtr resp_len = (UIntPtr)0UL;
                byte[] numArray = new byte[100];
                if (IdentificationDeviceServiceBaltech.brp_exec_cmd(_brpDev, 4, new byte[1], (UIntPtr)0UL, numArray, (UIntPtr)(ulong)numArray.Length, ref resp_len, 100U) == 0U && resp_len.ToUInt32() == 41U)
                {
                    _logger.Information("Baltech-Firmwarestring: " + Encoding.ASCII.GetString(numArray));
                    CreateWorker();
                }
            }
        }

        private void CreateWorker()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            backgroundWorker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            UIntPtr resp_len = (UIntPtr)0UL;
            byte[] numArray = new byte[100];
            var cmdCode = 1281;
            while (true)
            {
                if (IdentificationDeviceServiceBaltech.brp_exec_cmd(_brpDev, cmdCode, new byte[1], (UIntPtr)0UL, numArray, (UIntPtr)(ulong)numArray.Length, ref resp_len, 100U) == 0U && Encoding.ASCII.GetString(numArray).Length >= 16)
                    this.CardPresented(Encoding.ASCII.GetString(numArray).Substring(2, 14));
                Thread.Sleep(300);
            }
        }

        private void CardPresented(string cardNumber)
        {
            if (IdentificationOccured == null)
                return;
            _logger.Information("Presented identifier: {identifier}", cardNumber);
            IdentificationOccured(this, new IdentificationOccuredEventArgs(cardNumber));
        }

        public void Dispose()
        {
            _ = (int)IdentificationDeviceServiceBaltech.brp_close(_brpDev);
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