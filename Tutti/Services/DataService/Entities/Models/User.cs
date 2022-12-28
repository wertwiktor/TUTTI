using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataService.Entities.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TimeStamp> TimeStamps { get; set; }
    }
}
