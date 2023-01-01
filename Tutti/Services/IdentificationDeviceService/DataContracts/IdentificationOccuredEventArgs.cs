using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IdentificationDeviceService.DataContracts
{
    public class IdentificationOccuredEventArgs : EventArgs
    {
        public IdentificationOccuredEventArgs() { }
        public IdentificationOccuredEventArgs(string identifier) { Identifier = identifier; }

        public string Identifier { get; set; }

    }
}
