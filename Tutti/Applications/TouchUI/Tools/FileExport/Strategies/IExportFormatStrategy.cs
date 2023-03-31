
using TouchUI.Tools.FileExport.Models;

namespace TouchUI.Tools.FileExport.Strategies
{
    public interface IExportFormatStrategy
    {
        public void Export(string directory, string fileName, DetailedExportContent detailedExportContent);
        public void Export(string directory, string fileName, SummarizedExportContent summarizedExportContent);
    }
}
