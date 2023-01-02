using Serilog;
using Services.DataService;
using Services.DataServiceSql.DataModels;
using System.Data.Entity.Infrastructure.Interception;

namespace Services.DataServiceSql
{
    public partial class DataServiceSql : IDataService
    {
        private readonly ILogger _logger = Log.Logger.ForContext<DataServiceSql>();
        private TuttiDbContext GetDbContext()
        {
            TuttiDbContext result = null;

            try
            {
                result = new TuttiDbContext();
                result.Configuration.LazyLoadingEnabled = false;
                result.Configuration.AutoDetectChangesEnabled = false;
                result.Configuration.ProxyCreationEnabled = false;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error while creating Database context.");
            }

            return result;
        }
    }
}