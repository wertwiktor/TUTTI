using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Models;

namespace TouchUI.Tools.FileExport.Models
{
    public class DetailedExportContent : ExportContent
    {
        public List<User> Users { get; set; }
    }
}
