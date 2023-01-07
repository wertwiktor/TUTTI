using DataService.Models;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceService.DataContracts;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;
using TouchUI.Commands;
using TouchUI.Models.Enums;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class HomeViewModel : NavigationViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<HomeViewModel>();
        private readonly IDataService _dataService;
        private readonly IIdentificationDeviceService _idDeviceService;
        private readonly INavigationService _navigationService;

        private DateTime _currentDateTime = DateTime.Now;
        private string _mainMessage;
        private DispatcherTimer _mainMessageTimer = new DispatcherTimer();

        private ICommand _navigationCommand;

        public HomeViewModel(IDataService dataService, IIdentificationDeviceService idDeviceService, INavigationService navigationService)
            : base()
        {
            _logger.Debug("Creating main view model.");
            _dataService = dataService;
            _idDeviceService = idDeviceService;
            _navigationService = navigationService;
            InitializeSubscribtions();
            InitializeCommands();
            InitializeClockDisplayTimer();
            InitializeMainMessageTimer();
        }

        protected override void InitializeNavigatableViewModels()
        {
            NavigatableViewModels.Clear();
            NavigatableViewModels.Add(new NavigationTarget(typeof(RegisterViewModel), "Register", true));
        }


        private void InitializeCommands()
        {
            _navigationCommand = new NavigationCommand(_navigationService);
        }

        private void InitializeClockDisplayTimer()
        {
            var clockDisplayTimer = new DispatcherTimer();
            clockDisplayTimer.Interval = TimeSpan.FromSeconds(1);
            clockDisplayTimer.Tick += OnClockDisplayTimerElapsed;
            clockDisplayTimer.Start();
        }

        private void InitializeMainMessageTimer()
        {
            _mainMessageTimer.Interval = TimeSpan.FromSeconds(1);
            _mainMessageTimer.Tick += OnMainMessageTimerElapsed;
        }

        private void InitializeSubscribtions()
        {
            _idDeviceService.IdentificationOccured += OnIdServiceIdentificationOccured;
        }

        private void OnClockDisplayTimerElapsed(object? sender, EventArgs e)
        {
            CurrentDateTime = DateTime.Now;
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
                _logger.Error("Received IdentificationOccured event with null event arguments.");
                return;
            }

            if (string.IsNullOrEmpty(eventArgs.Identifier))
            {
                _logger.Error("Received IdentificationOccured event with identifier string null or empty.");
                return;
            }

            _logger.Information("Received IdentificationOccured event with identifier {identifier}.");

            ProcessUserIdentification(eventArgs.Identifier);
        }

        private void ProcessUserIdentification(string identifier)
        {
            User user;
            if (!TryGetUserFromDatabaseByIdentifier(identifier, out user))
            {
                _logger.Information("Attempted user identifiation for unknown user with identifier {identifier}. This use case is not implemented yet.", identifier);
                return;
            }

            var lastTimeStamp = _dataService.GetLastTimeStampByUserId(user.Id);
            if (lastTimeStamp == null || lastTimeStamp.Direction == (int)TimeStampDirection.Exit)
            {
                ProcessUserEntry(user);
            }
            else
            {
                ProcessUserExit(user);
            }
        }

        private void ProcessUserEntry(User user)
        {
            var timeStamp = new TimeStamp() { DateTime = DateTime.Now, Direction = (int)TimeStampDirection.Entry, UserId = user.Id };
            _dataService.AddTimeStamp(timeStamp);
            MainMessage = $"Hello, {user.Name}";
        }

        private void ProcessUserExit(User user)
        {
            var timeStamp = new TimeStamp() { DateTime = DateTime.Now, Direction = (int)TimeStampDirection.Exit, UserId = user.Id };
            _dataService.AddTimeStamp(timeStamp);
            MainMessage = $"Goodbye, {user.Name}";
        }

        private bool TryGetUserFromDatabaseByIdentifier(string identifier, out User user)
        {
            user = _dataService.GetUserByIdentifier(identifier);
            return user != null;
        }

        private void StartMainMessageTimer()
        {
            _mainMessageTimer.Start();
        }

        public DateTime CurrentDateTime
        {
            get
            {
                return _currentDateTime;
            }
            set
            {
                _currentDateTime = value;
                OnPropertyChanged();
            }
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
    }
}
