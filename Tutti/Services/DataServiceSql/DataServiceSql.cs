using Services.DataService;
using Services.DataServiceSql.DataModels;

namespace Services.DataServiceSql
{
    public partial class DataServiceSql : IDataService
    {
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
                //TODO: Implement error handling
            }

            return result;
        }
    }
}