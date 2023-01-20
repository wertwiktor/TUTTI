using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Tools.FileExport
{
    public class ExportContent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ReportingDatesMinimum { get; set; }
        public DateTime ReportingDatesMaximum { get; set; }
        List<User> Users { get; set; }

    }
}
