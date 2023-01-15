using DataService.Models;
using Framework.ExtensionMethods;
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
            if(user == null)
            {
                _logger.Error("Attempted to login null user.".Here());
                return;
            }
            _currentUser = user;
            _logger.Information("Logged in user {name} {surname}".Here(), user.Name, user.Surname);
            UserChanged?.Invoke(_currentUser);
        }

        public void Logout()
        {
            _logger.Information("Logging out current user.".Here());
            _currentUser = null;
            UserChanged?.Invoke(_currentUser);
        }
    }
}
