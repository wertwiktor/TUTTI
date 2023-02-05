using CsvHelper.Configuration;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Tools.FileExport.Strategies.CSV
{
    public sealed class TimeStampMap : ClassMap<TimeStamp>
    {
        public TimeStampMap() 
        {
            var dateTimeFormat = "dd.MM.yyyy HH:mm:ss";
            Map(m => m.EntryDate).TypeConverterOption.Format(dateTimeFormat);
            Map(m => m.ExitDate).TypeConverterOption.Format(dateTimeFormat);
            Map(m => m.Worktime);
        }
    }
}
