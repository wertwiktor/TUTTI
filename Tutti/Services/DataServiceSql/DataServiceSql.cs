using DataService.Models;
using Framework.ExtensionMethods;
using Serilog;
using Services.DataService;
using Services.DataServiceSql.DataModels;
using System.Data.Entity.Infrastructure.Interception;

namespace Services.DataServiceSql
{
    public partial class DataServiceSql : IDataService
    {
        private readonly ILogger _logger = Log.Logger.ForContext<DataServiceSql>();

        public DataServiceSql()
        {
            //First EF query is slow, so a dummy query is performed at startup
            _logger.Information("Running EF startup query.".Here());
            RunStartupQuery();
            _logger.Information("EF startup query finished.".Here());
        }
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
                _logger.Error(e, "Error while creating Database context.".Here());
            }

            return result;
        }

        private void RunStartupQuery()
        {
            using (var context = GetDbContext())
            {
                context.Users.Count();
            }
        }
    }
}