using System;
using TouchUI.Tools.FileExport.Models;

namespace TouchUI.Tools.FileExport.Strategies.PDF
{
    public class ExportPdfStrategy : ExportStrategyBase, IExportFormatStrategy
    {
        public void Export(string directory, string fileName, DetailedExportContent exportContent)
        {
            throw new NotImplementedException();
        }

        public void Export(string directory, string fileName, SummarizedExportContent summarizedExportContent)
        {
            throw new NotImplementedException();
        }
    }
}
