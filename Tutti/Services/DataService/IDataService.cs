using DataService.Models;

namespace Services.DataService
{
    public interface IDataService
    {
        void AddUser(User user);
        void DeleteUser(long id);
        User GetUser(long id);
        List<User> GetAllUsers();

        void AddTimeStamp(TimeStamp timeStamp);
        void DeleteTimeStamp(long id);
        List<TimeStamp> GetTimeStamps(long userId, DateTime minDateTime, DateTime maxDateTime);
    }
}