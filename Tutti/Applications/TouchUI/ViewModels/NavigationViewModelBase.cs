using DataService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using TouchUI.Commands;
using TouchUI.Models;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;

namespace TouchUI.ViewModels
{
    public abstract class NavigationViewModelBase : ViewModelBase
    {
        protected readonly INavigationService NavigationService;
        protected readonly ILoginService LoginService;
        private ICommand _navigationCommand;
        private User _currentUser;
        private bool _isBusy;
        private string _busyMessage;

        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget> {
            new NavigationTarget(typeof(HomeViewModel), "Home", true),
            new NavigationTarget(typeof(RegisterViewModel), "Register", true),
            new NavigationTarget(typeof(HistoryViewModel), "History", false),
            new NavigationTarget(typeof(ExportViewModel), "Export", false)};


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

        

        public ObservableCollection<NavigationTarget> NavigatableViewModels
        {
            get
            {
                return _navigatableViewModels;
            }
            set
            {
                _navigatableViewModels = value;
                OnPropertyChanged();
            }
        }

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

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        
        public string BusyMessage
        {
            get
            {
                return _busyMessage;
            }
            set
            {
                _busyMessage = value;
                OnPropertyChanged();
            }
        }

        protected virtual void UpdateNavigationBar()
        {
            bool userLoggedIn = _currentUser != null;

            var historyNavigationTarget = NavigatableViewModels.FirstOrDefault(x => x.Type == typeof(HistoryViewModel));
            if (historyNavigationTarget != null)
            {
                historyNavigationTarget.IsEnabled = userLoggedIn;
            }

            var exportNavigationTarget = NavigatableViewModels.FirstOrDefault(x => x.Type == typeof(ExportViewModel));
            if (exportNavigationTarget != null)
            {
                exportNavigationTarget.IsEnabled = userLoggedIn;
            }
        }

        public virtual void Uninitialize()
        {
            LoginService.UserChanged -= OnLoginServiceUserChanged;
        }
    }
}
