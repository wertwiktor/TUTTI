using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models
{
    public class TimeStamp
    {
        public long Id { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}