using Services.DataService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
