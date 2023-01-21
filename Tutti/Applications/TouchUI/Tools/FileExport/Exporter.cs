using DataService.Models;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.IO;
using TouchUI.Tools.FileExport.Strategies;

namespace TouchUI.Tools.FileExport
{
    public class Exporter
    {
        public Exporter()
        {
            SetDefaultValues();
        }

        public string ExportDirectory { get; set; }
        public string FileName { get; set; }
        public DateTime DateTimeMinimum { get; set; }
        public DateTime DateTimeMaximum { get; set; }

        public List<User> SelectedUsers { get; set; }

        public IExportFormatStrategy ExportFormatStrategy { get; set; }

        public IDataService DataService { get; set; }

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
                ReportingDatesMinimum = DateTimeMinimum,
                ReportingDatesMaximum = DateTimeMaximum,
            };
            //TODO:
            // Implement query to the database
            // based on DateMinimum, DateMaximum and SelectedUsers
            // Populate List of Users (With their list of timestamps) in exportContent.
            return exportContent;
        }

        private void SetDefaultValues()
        {
            SelectedUsers = new List<User>();
            ExportFormatStrategy = new ExportCsvStrategy();
            DateTimeMinimum = DateTime.MinValue;
            DateTimeMaximum = DateTime.MaxValue;
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
