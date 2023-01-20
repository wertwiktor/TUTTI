using Serilog;
using System;
using System.IO;

namespace TouchUI.Tools.FileExport.Strategies
{
    public abstract class ExportStrategyBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<ExportStrategyBase>();

        protected string FilePath;
        protected string FileName;
        public void CreateExportDirectory(string directoryPath)
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                _logger.Error("Exception occured while creating the export directory.", e);
            }
        }
    }
}
