using DataService.Models;

namespace Services.DataServiceSql
{
    public partial class DataServiceSql
    {
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var context = GetDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void DeleteUser(long id)
        {
            var user = new User() { Id = id };
            using (var context = GetDbContext())
            {
                context.Users.Attach(user);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public User GetUser(long id)
        {
            using (var context = GetDbContext())
            {
                return context.Users.FirstOrDefault(user => user.Id == id);
            }
        }

        public User GetUserByIdentifier(string identifier)
        {
            using (var context = GetDbContext())
            {
                return context.Users.FirstOrDefault(user => user.Identifier == identifier);
            }
        }

        public List<User> GetAllLoggedInUsers()
        {
            var users = new List<User>();
            var loginTimeoutHours = 16;
            var oldestDate = DateTime.Now - TimeSpan.FromHours(loginTimeoutHours);

            using (var context = GetDbContext())
            {
                users.AddRange(context.Users.Where(user => user.TimeStamps.Any(x => x.EntryDate != null
                                                                                 && x.EntryDate >= oldestDate
                                                                                 && x.ExitDate == null)));
            }
            return users;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var context = GetDbContext())
            {
                users.AddRange(context.Users);
            }

            return users;
        }
    }
}
