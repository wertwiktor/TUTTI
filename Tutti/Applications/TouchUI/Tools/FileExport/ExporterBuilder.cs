using DataService.Models;
using Services.DataService;
using System;
using System.Collections.Generic;
using TouchUI.Tools.FileExport.Strategies;
using TouchUI.Tools.FileExport.Strategies.CSV;
using TouchUI.Tools.FileExport.Strategies.PDF;

namespace TouchUI.Tools.FileExport
{
    public class ExporterBuilder
    {
        private readonly IDataService _dataService;
        private Exporter _exporter;

        public ExporterBuilder(long userId, ExportFormat exportFormat, IDataService dataService)
        {
            _dataService = dataService;
            _exporter = new Exporter(dataService);
            SetUser(userId);
            SetFormat(exportFormat);
        }

        public ExporterBuilder SetTimeRange(DateOnly dateMinimum, DateOnly dateMaximum)
        {
            _exporter.DateMinimum = dateMinimum;
            _exporter.DateMaximum = dateMaximum;
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

        private void SetUser(long userId)
        {
            var user = _dataService.GetUser(userId); 
            if (user == null)
            {
                return;
            }

            _exporter.User = user;
        }
    }
}
