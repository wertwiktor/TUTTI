using Services.DataService.Entities.Models;

namespace Services.DataService
{
    public interface IDataService
    {
        List<User> GetAllUsers();
    }
}