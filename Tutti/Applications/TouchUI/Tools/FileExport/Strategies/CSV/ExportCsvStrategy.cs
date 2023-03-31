using CsvHelper;
using DataService.Models;
using System.Globalization;
using System.IO;
using TouchUI.Tools.FileExport.Models;
using TouchUI.Tools.FileExport.Strategies.CSV.CsvMaps;

namespace TouchUI.Tools.FileExport.Strategies.CSV
{
    public class ExportCsvStrategy : ExportStrategyBase, IExportFormatStrategy
    {

        public void Export(string directory, string fileName, DetailedExportContent exportContent)
        {
            SetExportTarget(directory, fileName + " Details");
            ExportCsv(exportContent);
        }

        public void Export(string directory, string fileName, SummarizedExportContent summarizedExportContent)
        {
            SetExportTarget(directory, fileName + " Summary");
            ExportCsv(summarizedExportContent);
        }

        private void SetExportTarget(string directory, string fileName)
        {
            CreateExportDirectory(directory);
            FileName = string.Concat(fileName, ".csv");
            FilePath = Path.Combine(directory, FileName);
        }

        private void ExportCsv(DetailedExportContent exportContent)
        {
            if (exportContent == null)
            {
                return;
            }
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TimeStampMap>();
                //Initial information
                csv.WriteField($"Includes all records within: {exportContent.ReportingDatesMinimum} - {exportContent.ReportingDatesMaximum}");
                csv.NextRecord();
                csv.WriteField($"Created on: {exportContent.CreationDate}");
                csv.NextRecord();
                csv.NextRecord();
                //Header
                csv.WriteField("Full Name");
                csv.WriteHeader(typeof(TimeStamp));
                csv.NextRecord();
                //Records
                foreach (var user in exportContent.Users)
                {
                    foreach (var timestamp in user.TimeStamps)
                    {
                        csv.WriteField(user.FullName);
                        csv.WriteRecord(timestamp);
                        csv.NextRecord();
                    }
                }
            }
        }

        private void ExportCsv(SummarizedExportContent exportContent)
        {
            if (exportContent == null)
            {
                return;
            }
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<WorkdaySummaryMap>();
                //Initial information
                csv.WriteField($"Includes summary from dates: {exportContent.ReportingDatesMinimum} - {exportContent.ReportingDatesMaximum}");
                csv.NextRecord();
                csv.WriteField($"Created on: {exportContent.CreationDate}");
                csv.NextRecord();
                csv.NextRecord();
                //Header
                csv.WriteHeader(typeof(WorkdaySummary));
                csv.NextRecord();
                //Records
                foreach (var workdaySummary in exportContent.Content)
                {
                        csv.WriteRecord(workdaySummary);
                        csv.NextRecord();
                }
            }
        }
    }
}
