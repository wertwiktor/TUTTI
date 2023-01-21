using DataService.Models;
using Framework.ExtensionMethods;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceService.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using TouchUI.Commands;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;

namespace TouchUI.ViewModels
{
    public class HomeViewModel : NavigationViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<HomeViewModel>();
        private readonly IDataService _dataService;
        private readonly IIdentificationDeviceService _idDeviceService;

        private ICommand _confirmExitCommand;
        private ICommand _resumeWorkCommand;
        private string _mainMessage;
        private DispatcherTimer _mainMessageTimer = new DispatcherTimer();
        private ObservableCollection<User> _activeUsers;

        public HomeViewModel(IDataService dataService,
            IIdentificationDeviceService idDeviceService,
            INavigationService navigationService,
            ILoginService loginService)
            : base(navigationService, loginService)
        {
            _logger.Debug("Creating main view model.".Here());
            _dataService = dataService;
            _idDeviceService = idDeviceService;
            InitializeSubscribtions();
            InitializeMainMessageTimer();
            InitializeCommands();
            RefreshActiveUsers();
        }

        public override void Uninitialize()
        {
            _mainMessageTimer.Stop();
            UninitializeSubscribtions();
            base.Uninitialize();
        }

        public string MainMessage
        {
            get
            {
                return _mainMessage;
            }
            set
            {
                _mainMessage = value;
                OnPropertyChanged();
                if (!string.IsNullOrEmpty(_mainMessage))
                {
                    StartMainMessageTimer();
                }
            }
        }

        public ObservableCollection<User> ActiveUsers
        {
            get
            {
                return _activeUsers;
            }
            set
            {
                _activeUsers = value;
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

        public ICommand ResumeWorkCommand
        {
            get
            {
                return _resumeWorkCommand;
            }
            set
            {
                _resumeWorkCommand = value;
                OnPropertyChanged();
            }
        }

        private void RefreshActiveUsers()
        {
            var activeUsers = _dataService.GetAllLoggedInUsers();
            foreach (var user in activeUsers)
            {
                var recentEntry = _dataService.GetLastTimeStampByUserId(user.Id);
                user.TimeStamps = new List<TimeStamp>();
                user.TimeStamps.Add(recentEntry);
            }

            activeUsers = activeUsers.OrderByDescending(x => x.TimeStamps.FirstOrDefault()?.EntryDate).ToList();
            ActiveUsers = new ObservableCollection<User>(activeUsers);            
        }
        private void InitializeCommands()
        {
            _confirmExitCommand = new RelayCommand(ConfirmExit);
            _resumeWorkCommand = new RelayCommand(ResumeWork);
        }

        private void InitializeSubscribtions()
        {
            _idDeviceService.IdentificationOccured += OnIdServiceIdentificationOccured;
        }

        private void UninitializeSubscribtions()
        {
            _idDeviceService.IdentificationOccured -= OnIdServiceIdentificationOccured;
        }

        private void InitializeMainMessageTimer()
        {
            _mainMessageTimer.Interval = TimeSpan.FromSeconds(3);
            _mainMessageTimer.Tick += OnMainMessageTimerElapsed;
        }

        private void OnMainMessageTimerElapsed(object? sender, EventArgs e)
        {
            _mainMessageTimer.Stop();
            MainMessage = string.Empty;
        }

        private void OnIdServiceIdentificationOccured(object sender, IdentificationOccuredEventArgs eventArgs)
        {

            if (eventArgs == null)
            {
                _logger.Error("Received IdentificationOccured event with null event arguments.".Here());
                return;
            }

            if (string.IsNullOrEmpty(eventArgs.Identifier))
            {
                _logger.Error("Received IdentificationOccured event with identifier string null or empty.".Here());
                return;
            }

            _logger.Information("Received IdentificationOccured event with identifier {identifier}.".Here(), eventArgs.Identifier);

            ProcessUserIdentification(eventArgs.Identifier);
        }

        private void ProcessUserIdentification(string identifier)
        {
            User user;
            if (!TryGetUserFromDatabaseByIdentifier(identifier, out user))
            {
                _logger.Information("Attempted user identifiation for unknown user with identifier {identifier}.".Here(), identifier);
                return;
            }

            if (CurrentUser != null)
            {
                _logger.Information("Received user identifiation of {receivedName} {receivedSurname} when the user {logedName} {loggedSurname} was already logged in.".Here(), user.Name, user.Surname, CurrentUser.Name, CurrentUser.Surname);
                return;
            }

            var lastTimeStamp = _dataService.GetLastTimeStampByUserId(user.Id);
            if (lastTimeStamp == null || lastTimeStamp.ExitDate != null)
            {
                ProcessUserEntry(user);
            }
            else
            {
                LoginService.Login(user);
            }
        }

        private void ProcessUserEntry(User user)
        {
            var timeStamp = new TimeStamp() { EntryDate = DateTime.Now, UserId = user.Id };
            _dataService.AddTimeStamp(timeStamp);
            MainMessage = $"Hello {user.Name}!";
            RefreshActiveUsers();
        }

        private void ConfirmExit()
        {
            if (CurrentUser != null)
            {
                var timeStamp = _dataService.GetLastTimeStampByUserId(CurrentUser.Id);
                timeStamp.ExitDate = DateTime.Now;
                _dataService.EditTimeStamp(timeStamp);
            }
            else
            {
                _logger.Error("Current user was null while arrempting to register user exit. This exit has not been saved in the database.".Here());
            }
            LoginService.Logout();
            RefreshActiveUsers();
        }

        private void ResumeWork()
        {
            LoginService.Logout();
        }

        private bool TryGetUserFromDatabaseByIdentifier(string identifier, out User user)
        {
            user = _dataService.GetUserByIdentifier(identifier);
            return user != null;
        }

        private void StartMainMessageTimer()
        {
            InitializeMainMessageTimer();
            _mainMessageTimer.Start();
        }
    }
}
