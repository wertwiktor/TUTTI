using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Models;

namespace TouchUI.Tools.FileExport
{
    public class ExportContent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateOnly ReportingDatesMinimum { get; set; }
        public DateOnly ReportingDatesMaximum { get; set; }
        public List<User> Users { get; set; }
    }
}
