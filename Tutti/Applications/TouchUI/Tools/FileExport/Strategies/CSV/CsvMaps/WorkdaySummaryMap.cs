using CsvHelper.Configuration;
using TouchUI.Tools.FileExport.Models;

namespace TouchUI.Tools.FileExport.Strategies.CSV.CsvMaps
{
    public class WorkdaySummaryMap : ClassMap<WorkdaySummary>
    {
        public WorkdaySummaryMap()
        {
            var dateFormat = "dd.MM.yyyy";
            var timeFormat = @"hh\:mm\:ss";
            Map(m => m.User.FullName);
            Map(m => m.Date).TypeConverterOption.Format(dateFormat);
            Map(m => m.WorkTime).TypeConverterOption.Format(timeFormat); ;
            Map(m => m.BreakTime).TypeConverterOption.Format(timeFormat); ;
        }
    }
}
