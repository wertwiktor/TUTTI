using DataService.Models;
using Serilog;
using System;

namespace TouchUI.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly ILogger _logger = Log.Logger.ForContext<LoginService>();

        private User _currentUser;

        public event Action<User> UserChanged;

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public void Login(User user)
        {
            _currentUser = user;
            UserChanged?.Invoke(_currentUser);
        }

        public void Logout()
        {
            _currentUser = null;
            UserChanged?.Invoke(_currentUser);
        }
    }
}
