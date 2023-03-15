using DataService.Models;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.IO;
using TouchUI.Tools.FileExport.Strategies;
using TouchUI.Tools.FileExport.Strategies.CSV;
using TouchUI.Tools.WorktimeCalculations;

namespace TouchUI.Tools.FileExport
{
    public class Exporter
    {
        private readonly IDataService _dataService;
        private readonly WorktimeHelper _worktimeHelper = new WorktimeHelper();

        public Exporter(IDataService dataService)
        {
            _dataService = dataService;
            SetDefaultValues();
        }

        public string ExportDirectory { get; set; }
        public string FileName { get; set; }
        public DateOnly DateMinimum { get; set; }
        public DateOnly DateMaximum { get; set; }
        public List<User> Users { get; set; }

        public IExportFormatStrategy ExportFormatStrategy { get; set; }

        public void Export()
        {
            var exportContent = CreateExportContent();
            ExportFormatStrategy.Export(ExportDirectory, FileName, exportContent);
        }

        private ExportContent CreateExportContent()
        {
            var exportContent = new ExportContent()
            {
                CreationDate = DateTime.Now,
                ReportingDatesMinimum = DateMinimum,
                ReportingDatesMaximum = DateMaximum,
                Users = Users
            };

            foreach(var user in exportContent.Users)
            {
                var timeStamps = _dataService.GetTimeStamps(user.Id, DateMinimum.ToDateTime(new TimeOnly()), DateMaximum.ToDateTime(new TimeOnly()));
                _worktimeHelper.CalculateWorktimesInTimeStamps(timeStamps);
                user.TimeStamps = timeStamps;
            } 

            return exportContent;
        }

        private void SetDefaultValues()
        {
            Users = new List<User>();
            ExportFormatStrategy = new ExportCsvStrategy();
            DateMinimum = DateOnly.MinValue;
            DateMaximum = DateOnly.MaxValue;
            ExportDirectory = CreateDefaultExportPath();
            FileName = CreateDefaultFileName();
        }

        private string CreateDefaultExportPath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TUTTI\\Exports");
            return path;
        }

        private string CreateDefaultFileName()
        {
            return string.Join("_", "Export", DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

    }
}
