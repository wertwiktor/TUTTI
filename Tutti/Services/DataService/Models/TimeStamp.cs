using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models
{
    public class TimeStamp
    {
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool Orphan { get; set; } = true;
        public bool RecordValid { get; set; } = false;
        public bool EditedManually { get; set; } = false;
        public long UserId { get; set; }
        public User User { get; set; }
    }
}