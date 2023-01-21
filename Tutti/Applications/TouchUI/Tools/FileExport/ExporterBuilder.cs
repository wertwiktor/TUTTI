using DataService.Models;
using Services.DataService;
using System;
using System.Collections.Generic;
using TouchUI.Tools.FileExport.Strategies;

namespace TouchUI.Tools.FileExport
{
    public class ExporterBuilder
    {
        private Exporter _exporter = new Exporter();

        public ExporterBuilder(List<User> users, ExportFormat exportFormat, IDataService dataService)
        {
            SetUsers(users);
            SetFormat(exportFormat);
            _exporter.DataService = dataService;
        }

        public ExporterBuilder SetTimeRange(DateTime dateTimeMinimum, DateTime dateTimeMaximum)
        {
            _exporter.DateTimeMinimum = dateTimeMinimum;
            _exporter.DateTimeMaximum = dateTimeMaximum;
            return this;
        }

        public ExporterBuilder SetExportDirectory(string exportDirectory)
        {
            _exporter.ExportDirectory = exportDirectory;
            return this;
        }

        public ExporterBuilder SetFileName(string fileName)
        {
            _exporter.FileName = fileName;
            return this;
        }

        public Exporter Build()
        {
            return _exporter;
        }

        private void SetFormat(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Csv:
                    _exporter.ExportFormatStrategy = new ExportCsvStrategy();
                    break;
                case
                    ExportFormat.Pdf:
                    _exporter.ExportFormatStrategy = new ExportPdfStrategy();
                    break;
            }
        }

        private void SetUsers(List<User> users)
        {
            if (users == null)
            {
                return;
            }

            _exporter.SelectedUsers = users;
        }
    }
}
