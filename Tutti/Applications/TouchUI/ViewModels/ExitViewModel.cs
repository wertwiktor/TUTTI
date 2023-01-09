using DataService.Models;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchUI.Commands;
using TouchUI.Models.Enums;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class ExitViewModel : NavigationViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<HomeViewModel>();
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;

        private User _currentUser;
        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget>{
            new NavigationTarget(typeof(HomeViewModel), "Home", true) };
        private ICommand _confirmExitCommand;

        public ExitViewModel(IDataService dataService,
            IIdentificationDeviceService idDeviceService,
            INavigationService navigationService,
            ILoginService loginService)
            : base(navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _loginService = loginService;
            _currentUser = _loginService.GetCurrentUser();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            _confirmExitCommand = new RelayCommand(ConfirmExit);
        }

        private void ConfirmExit()
        {
            _loginService.Logout();
            if (_currentUser != null)
            {
                var timeStamp = new TimeStamp() { DateTime = DateTime.Now, Direction = (int)TimeStampDirection.Exit, UserId = _currentUser.Id };
                _dataService.AddTimeStamp(timeStamp);
            }
            else
            {
                _logger.Error("Current user was null while arrempting to register user exit. This exit has not been saved in the database.");
            }
            NavigationService.Navigate<HomeViewModel>();
        }

        public override ObservableCollection<NavigationTarget> NavigatableViewModels
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

        public ICommand ConfirmExitCommand
        {
            get
            {
                return _confirmExitCommand;
            }
            set
            {
                _confirmExitCommand = value;
                OnPropertyChanged();
            }
        }

        public override void Uninitialize()
        {

        }
    }
}
