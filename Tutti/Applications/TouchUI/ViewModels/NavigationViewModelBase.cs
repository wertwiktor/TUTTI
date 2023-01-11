using DataService.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TouchUI.Commands;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public abstract class NavigationViewModelBase : ViewModelBase
    {
        protected readonly INavigationService NavigationService;
        protected readonly ILoginService LoginService;
        private ICommand _navigationCommand;
        private User _currentUser;

        public NavigationViewModelBase(INavigationService navigationService, ILoginService loginService)
        {
            NavigationService = navigationService;
            LoginService = loginService;
            _navigationCommand = new NavigationCommand(NavigationService);
            CurrentUser = LoginService.GetCurrentUser();
            LoginService.UserChanged += OnLoginServiceUserChanged;
        }

        private void OnLoginServiceUserChanged(User user)
        {
            CurrentUser = user;
        }

        public abstract ObservableCollection<NavigationTarget> NavigatableViewModels { get; set; }

        public ICommand NavigationCommand
        {
            get
            {
                return _navigationCommand;
            }
            set
            {
                _navigationCommand = value;
                OnPropertyChanged();
            }
        }
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            protected set
            {
                _currentUser = value;
                OnPropertyChanged();
                UpdateNavigationBar();
            }
        }

        protected virtual void UpdateNavigationBar()
        {
            bool userLoggedIn = _currentUser != null;

            var registerNavigationTarget = NavigatableViewModels.FirstOrDefault(x => x.Type == typeof(RegisterViewModel));
            if(registerNavigationTarget!= null) 
            {
                registerNavigationTarget.IsEnabled = userLoggedIn;
            }

            var historyNavigationTarget = NavigatableViewModels.FirstOrDefault(x => x.Type == typeof(HistoryViewModel));
            if (historyNavigationTarget != null)
            {
                historyNavigationTarget.IsEnabled = userLoggedIn;
            }
        }

        public virtual void Uninitialize()
        {
            LoginService.UserChanged -= OnLoginServiceUserChanged;
        }
    }
}
