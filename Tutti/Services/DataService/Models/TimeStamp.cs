using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class TimeStamp
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Direction { get; set; }
    }
}
