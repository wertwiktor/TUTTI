
namespace TouchUI.Tools.FileExport.Strategies
{
    public interface IExportFormatStrategy
    {
        public void Export(string directory, string fileName, ExportContent exportContent);
    }
}
