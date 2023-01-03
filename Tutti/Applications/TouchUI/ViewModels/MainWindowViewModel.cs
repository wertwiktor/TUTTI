using DataService.Models;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceService.DataContracts;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Windows.Input;
using System.Windows.Threading;
using TouchUI.Models.Enums;

namespace TouchUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<MainWindowViewModel>();
        private IDataService _dataService;
        private IIdentificationDeviceService _idDeviceService;

        private DateTime _currentDateTime = DateTime.Now;
        private IdentificationMode _identificationMode = IdentificationMode.Entry;
        private string _mainMessage;
        private DispatcherTimer _mainMessageTimer = new DispatcherTimer();

        public MainWindowViewModel(IDataService dataService, IIdentificationDeviceService idDeviceService)
        {
            _logger.Debug("Creating main view model.");
            _dataService = dataService;
            _idDeviceService = idDeviceService;
            InitializeSubscribtions();
            InitializeCommands();
            InitializeClockDisplayTimer();
            InitializeMainMessageTimer();
        }

        private void InitializeCommands() 
        {
            SetIdentificationModeToEntryCommand = new RelayCommand(() => IdentificationMode = IdentificationMode.Entry);
            SetIdentificationModeToExitCommand = new RelayCommand(() => IdentificationMode = IdentificationMode.Exit);
            SetIdentificationModeToInfoCommand = new RelayCommand(() => IdentificationMode = IdentificationMode.Info);
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
            if(eventArgs == null)
            {
                _logger.Error("Received IdentificationOccured event with null event arguments.");
                return; 
            }

            if (string.IsNullOrEmpty(eventArgs.Identifier))
            {
                _logger.Error("Received IdentificationOccured event with identifier string null or empty.");
                return;
            }

            _logger.Information("Received IdentificationOccured event with identifier {identifier}. Current application mode: {idMode}", eventArgs.Identifier, IdentificationMode);

            ProcessUserIdentification(eventArgs.Identifier);           
        }

        private void ProcessUserIdentification(string identifier)
        {
            User user;
            if (TryGetUserFromDatabaseByIdentifier(identifier, out user))
            {
                switch (IdentificationMode)
                {
                    case IdentificationMode.Entry:
                        ProcessUserEntry(user);
                        break;
                    case IdentificationMode.Exit:
                        ProcessUserExit(user);
                        break;
                    case IdentificationMode.Info:
                        ProcessUserInfo(user);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _logger.Information("Attempted user identifiation for unknown user with id {identifier}. This use case is not implemented yet.", identifier);
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

        private void ProcessUserInfo(User user)
        {
            MainMessage = $"This is not implemented.";
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

        public IdentificationMode IdentificationMode
        {
            get
            {
                return _identificationMode;
            }
            set
            {               
                _identificationMode = value;
                _logger.Information("Changed identification mode to {identificationMode}", value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsIdentificationModeEntry));
                OnPropertyChanged(nameof(IsIdentificationModeExit));
                OnPropertyChanged(nameof(IsIdentificationModeInfo));
            }
        }

        public bool IsIdentificationModeEntry
        {
            get
            {
                return IdentificationMode == IdentificationMode.Entry;
            }
        }

        public bool IsIdentificationModeExit
        {
            get
            {
                return IdentificationMode == IdentificationMode.Exit;
            }
        }

        public bool IsIdentificationModeInfo
        {
            get
            {
                return IdentificationMode == IdentificationMode.Info;
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
                if(!string.IsNullOrEmpty(_mainMessage))
                {
                    StartMainMessageTimer();
                }
                OnPropertyChanged();
            }
        }

        public ICommand SetIdentificationModeToEntryCommand { get; set; }
        public ICommand SetIdentificationModeToExitCommand { get; set; }
        public ICommand SetIdentificationModeToInfoCommand { get; set; }
    }
}
