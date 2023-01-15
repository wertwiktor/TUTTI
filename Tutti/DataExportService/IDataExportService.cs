using DataService.Models;
using Serilog;
using Services.DataService;
using System;
using System.Collections.Generic;

namespace Services.DataExportService
{
    public interface IDataExportService
    {
        void ExportCSVDataByMonth(DateTime month);
        void ExportCSVDataByUser(long userId);

        void ExportPDFDataByMonth(DateTime month);
        void ExportPDFDataByUser(long userId);
    }
}