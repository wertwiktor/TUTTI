using DataService.Models;

namespace Services.DataService
{
    public interface IDataService
    {
        void AddUser(User user);
        void DeleteUser(long id);
        User GetUser(long id);
        User GetUserByIdentifier(string identifier);
        List<User> GetAllUsers();

        void AddTimeStamp(TimeStamp timeStamp);
        void EditTimeStamp(TimeStamp timeStamp);
        void DeleteTimeStamp(long id);
        List<TimeStamp> GetTimeStamps(long userId, DateTime minDateTime, DateTime maxDateTime);
        TimeStamp GetLastTimeStampByUserId(long userId);
    }
}