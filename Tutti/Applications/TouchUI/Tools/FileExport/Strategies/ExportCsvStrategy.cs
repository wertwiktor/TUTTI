using CsvHelper;
using DataService.Models;
using Microsoft.Xaml.Behaviors.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace TouchUI.Tools.FileExport.Strategies
{
    public class ExportCsvStrategy : ExportStrategyBase, IExportFormatStrategy
    {
        public void Export(string directory, string fileName, ExportContent exportContent)
        {
            SetExportTarget(directory, fileName);
            DummyTestExport();
        }

        private void SetExportTarget(string directory, string fileName)
        {
            CreateExportDirectory(directory);
            FileName = string.Concat(fileName, ".csv");
            FilePath = Path.Combine(directory, FileName);
        }

        private void DummyTestExport()
        {
            var dummyInts = new List<int>()
            {
                1, 2, 50, 100
            };
            
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(dummyInts);
            }
        }
    }
}
