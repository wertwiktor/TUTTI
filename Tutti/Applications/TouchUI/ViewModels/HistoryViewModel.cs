using DataService.Models;
using Framework.ExtensionMethods;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchUI.Commands;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class HistoryViewModel : NavigationViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<HomeViewModel>();
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<TimeStamp> _timeStampsHistory = new ObservableCollection<TimeStamp>();
        private ICommand _startEditingCommand;
        private ICommand _saveEditingCommand;
        private ICommand _cancelEditingCommand;
        private bool _isEditingActive;
        private TimeStamp _editedTimeStamp;

        public HistoryViewModel(IDataService dataService,
            IIdentificationDeviceService idDeviceService,
            INavigationService navigationService,
            ILoginService loginService)
            : base(navigationService, loginService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            InitializeCommands();
            InitializeHistory();
        }

        private void InitializeCommands()
        {
            _startEditingCommand = new RelayCommand(StartEditing);
            _saveEditingCommand = new RelayCommand(SaveEditing);
            _cancelEditingCommand = new RelayCommand(CancelEditing);
        }

        private void InitializeHistory()
        {
            if (CurrentUser == null)
            {
                _logger.Error("Current user was null when trying to initialize timestamps history.".Here());
                return;
            }
            _timeStampsHistory.Clear();
            var oldestTimeStampDate = DateTime.Now - TimeSpan.FromDays(30);
            var newestTimeStampDate = DateTime.Now;
            var timeStampHistory = _dataService.GetTimeStamps(CurrentUser.Id, oldestTimeStampDate, newestTimeStampDate).OrderByDescending(x => x.EntryDate);
            TimeStampsHistory = new ObservableCollection<TimeStamp>(timeStampHistory);
        }

        private void StartEditing(object parameter)
        {
            IsEditingActive = true;
            EditedTimeStamp = parameter as TimeStamp;
        }

        private void SaveEditing(object parameter)
        {
            IsEditingActive = false;
        }

        private void CancelEditing(object paramerer)
        {
            IsEditingActive = false;
        }

        public ObservableCollection<TimeStamp> TimeStampsHistory
        {
            get
            {
                return _timeStampsHistory;
            }
            set
            {
                _timeStampsHistory = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartEditingCommand
        {
            get { return _startEditingCommand; }
            set { _startEditingCommand = value; OnPropertyChanged(); }
        }

        public ICommand SaveEditingCommand
        {
            get { return _saveEditingCommand; }
            set { _saveEditingCommand = value; OnPropertyChanged(); }
        }

        public ICommand CancelEditingCommand
        {
            get { return _cancelEditingCommand; }
            set { _cancelEditingCommand = value; OnPropertyChanged(); }
        }

        public bool IsEditingActive
        {
            get
            {
                return _isEditingActive;
            }
            set
            {
                _isEditingActive = value;
                OnPropertyChanged();
            }
        }

        public TimeStamp EditedTimeStamp
        {
            get
            {
                return _editedTimeStamp;
            }
            set
            {
                _editedTimeStamp = value;
                OnPropertyChanged();
            }
        }

        public override void Uninitialize()
        {
            base.Uninitialize();
        }
    }
}
