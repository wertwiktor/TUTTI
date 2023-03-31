using DataService.Models;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TouchUI.Tools.FileExport.Models;
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
            GetTimestampsFromDatabase();
            var detailedExportContent = CreateDetailedExportContent();
            ExportFormatStrategy.Export(ExportDirectory, FileName, detailedExportContent);
            var summarizedExportContent = CreateSummarizedExportcontent();
            ExportFormatStrategy.Export(ExportDirectory, FileName, summarizedExportContent);
        }

        private void GetTimestampsFromDatabase()
        {
            if (Users == null)
            {
                return;
            }

            foreach (var user in Users)
            {
                var timeStamps = _dataService.GetTimeStamps(user.Id, DateMinimum.ToDateTime(new TimeOnly()), DateMaximum.ToDateTime(new TimeOnly(23, 59, 59)));
                _worktimeHelper.CalculateWorktimesInTimeStamps(timeStamps);
                user.TimeStamps = timeStamps;
            }
        }

        private DetailedExportContent CreateDetailedExportContent()
        {
            var detailedExportContent = new DetailedExportContent()
            {
                CreationDate = DateTime.Now,
                ReportingDatesMinimum = DateMinimum,
                ReportingDatesMaximum = DateMaximum,
                Users = Users
            };

            return detailedExportContent;
        }

        private SummarizedExportContent CreateSummarizedExportcontent()
        {
            var summarizedExportContent = new SummarizedExportContent()
            {
                CreationDate = DateTime.Now,
                ReportingDatesMinimum = DateMinimum,
                ReportingDatesMaximum = DateMaximum,
                Content = new List<WorkdaySummary>()
            };

            foreach (var user in Users)
            {
                var filteredTimeStamps = user.TimeStamps.Where(x => x.ResultantEntryDate.HasValue && x.ResultantExitDate.HasValue);
                var groupedTimeStamps = filteredTimeStamps.GroupBy(x => x.ResultantEntryDate.Value.Date);
                foreach(var group in groupedTimeStamps)
                {
                    var summary = _worktimeHelper.CalculateWorkdaySummarryFromTimestamps(group.ToList());
                    summary.User = user;
                    summarizedExportContent.Content.Add(summary);
                }
            }

            return summarizedExportContent;
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
