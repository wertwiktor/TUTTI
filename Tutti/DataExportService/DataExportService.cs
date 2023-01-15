using DataService.Models;
using Serilog;
using Services.DataService;
using System;
using System.Collections.Generic;

namespace DataExportService
{
    public class DataExportService
    {
        private readonly ILogger _logger = Log.Logger.ForContext<DataExportService>();
        private readonly IDataService _dataService;

        DataExportService()
        {

        }

        public void ExportWorkingTimeByUser(long userId)
        {
            User user = _dataService.GetUserByIdentifier("1234");
            var minDateTime = new DateTime(2023, 01, 01);
            var maxDateTime = new DateTime(2023, 01, 31);
            var timeStamps = _dataService.GetTimeStamps(user.Id, minDateTime, maxDateTime);

            List<TimeStamp> entries = new(timeStamps.FindAll(x => x.Direction == TimeStampDirection.Entry));
            List<TimeStamp> exits = new(timeStamps.FindAll(x => x.Direction == TimeStampDirection.Exit));

            foreach (var entry in entries)
            {
                //find coresponding exit to selected entry
                TimeStamp exit = exits.FindAll(x => x.DateTime > entry.DateTime).OrderBy(a => a.DateTime).First();
                
            }

        }


    }
}
