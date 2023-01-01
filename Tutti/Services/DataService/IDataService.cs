using DataService.Models;

namespace Services.DataService
{
    public interface IDataService
    {
        List<User> GetAllUsers();
    }
}