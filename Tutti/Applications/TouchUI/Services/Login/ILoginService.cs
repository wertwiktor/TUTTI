using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Services.Login
{
    public interface ILoginService
    {
        void Logout();

        void Login(User user);

        User GetCurrentUser();

        event Action<User> UserChanged;
    }
}
