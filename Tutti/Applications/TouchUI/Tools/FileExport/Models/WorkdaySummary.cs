using DataService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Tools.FileExport.Models
{
    public class WorkdaySummary
    {
        public User User { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan WorkTime { get; set; }

        public TimeSpan BreakTime { get; set; }
    }
}
