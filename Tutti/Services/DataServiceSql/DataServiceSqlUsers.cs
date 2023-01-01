using DataService.Models;

namespace Services.DataServiceSql
{
    public partial class DataServiceSql
    {
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
