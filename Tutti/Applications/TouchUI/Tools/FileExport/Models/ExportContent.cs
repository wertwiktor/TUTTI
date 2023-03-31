using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Tools.FileExport.Models
{
    public class ExportContent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateOnly ReportingDatesMinimum { get; set; }
        public DateOnly ReportingDatesMaximum { get; set; }

    }
}
