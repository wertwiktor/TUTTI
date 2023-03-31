using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Tools.FileExport.Models
{
    public class SummarizedExportContent : ExportContent
    {
        public List<WorkdaySummary> Content { get; set; }
    }
}
