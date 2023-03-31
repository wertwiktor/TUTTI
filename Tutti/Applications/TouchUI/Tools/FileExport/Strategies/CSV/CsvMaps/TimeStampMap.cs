using CsvHelper.Configuration;
using DataService.Models;

namespace TouchUI.Tools.FileExport.Strategies.CSV.CsvMaps
{
    public sealed class TimeStampMap : ClassMap<TimeStamp>
    {
        public TimeStampMap()
        {
            var dateFormat = "dd.MM.yyyy HH:mm:ss";
            var timeFormat = @"hh\:mm\:ss";
            Map(m => m.ResultantEntryDate).TypeConverterOption.Format(dateFormat);
            Map(m => m.ResultantExitDate).TypeConverterOption.Format(dateFormat);
            Map(m => m.WorkTime).TypeConverterOption.Format(timeFormat);
            Map(m => m.BreakTime).TypeConverterOption.Format(timeFormat);
            Map(m => m.EntryDate).TypeConverterOption.Format(dateFormat);
            Map(m => m.ExitDate).TypeConverterOption.Format(dateFormat);
            Map(m => m.EditedEntryDate).TypeConverterOption.Format(dateFormat);
            Map(m => m.EditedExitDate).TypeConverterOption.Format(dateFormat);
        }
    }
}
