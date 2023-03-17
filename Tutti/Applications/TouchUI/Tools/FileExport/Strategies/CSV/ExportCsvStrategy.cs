using CsvHelper;
using CsvHelper.TypeConversion;
using DataService.Models;
using Microsoft.Xaml.Behaviors.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TouchUI.Tools.FileExport.Strategies.CSV
{
    public class ExportCsvStrategy : ExportStrategyBase, IExportFormatStrategy
    {
        public void Export(string directory, string fileName, ExportContent exportContent)
        {
            SetExportTarget(directory, fileName);
            ExportCsv(exportContent);
        }

        private void SetExportTarget(string directory, string fileName)
        {
            CreateExportDirectory(directory);
            FileName = string.Concat(fileName, ".csv");
            FilePath = Path.Combine(directory, FileName);
        }

        private void ExportCsv(ExportContent exportContent)
        {
            if (exportContent == null)
            {
                return;
            }
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TimeStampMap>();
                csv.WriteField($"Includes all records within: {exportContent.ReportingDatesMinimum} - {exportContent.ReportingDatesMaximum}");
                csv.NextRecord();
                csv.WriteField($"Created on: {exportContent.CreationDate}");
                csv.NextRecord();
                csv.NextRecord();
                foreach (var user in exportContent.Users)
                {
                    foreach(var timestamp in user.TimeStamps)
                    {
                        csv.WriteField(user.FullName);
                        csv.WriteRecord(timestamp);
                        csv.NextRecord();
                    }
                }
            }
        }
    }
}
