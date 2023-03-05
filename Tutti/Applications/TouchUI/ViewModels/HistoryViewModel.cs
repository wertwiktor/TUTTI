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
using TouchUI.Models;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;

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
        private DateTime _editedDateEntry;
        private DateTime _editedDateExit;
        private TimeSpanComponent _editedTimeEntry;
        private TimeSpanComponent _editedTimeExit;
        private bool _editingErrorOccured;
        private string _editingErrorMessage;

        public HistoryViewModel(IDataService dataService,
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
            _editedTimeStamp = parameter as TimeStamp;
            if(_editedTimeStamp == null)
            {
                return;
            }

            if (_editedTimeStamp.ResultantEntryDate.HasValue)
            {
                EditedDateEntry = _editedTimeStamp.ResultantEntryDate.Value.Date;
                EditedTimeEntry = new TimeSpanComponent(_editedTimeStamp.ResultantEntryDate.Value.TimeOfDay);
            }
            else
            {
                EditedDateEntry = DateTime.MinValue;
                EditedTimeEntry = new TimeSpanComponent();
            }

            if (_editedTimeStamp.ResultantExitDate.HasValue)
            {
                EditedDateExit = _editedTimeStamp.ResultantExitDate.Value.Date;
                EditedTimeExit = new TimeSpanComponent(_editedTimeStamp.ResultantExitDate.Value.TimeOfDay);
            }
            else
            {
                EditedDateExit = DateTime.MinValue;
                EditedTimeExit = new TimeSpanComponent();
            }

            IsEditingActive = true;
        }

        private void SaveEditing(object parameter)
        {
            if(IsEditValid())
            {
                _editedTimeStamp.EditedEntryDate = EditedDateEntry + EditedTimeEntry.GetTimeSpan();
                _editedTimeStamp.EditedExitDate = EditedDateExit + EditedTimeExit.GetTimeSpan();
                _dataService.UpdateTimeStamp(_editedTimeStamp);
                IsEditingActive = false;
            }
        }

        private void CancelEditing(object paramerer)
        {
            IsEditingActive = false;
        }

        private bool IsEditValid()
        {
            var editedEntry = EditedDateEntry + EditedTimeEntry.GetTimeSpan();
            var editedExit = EditedDateExit + EditedTimeExit.GetTimeSpan();

            if (editedEntry > DateTime.Now || editedExit > DateTime.Now)
            {
                EditingErrorOccured = true;
                EditingErrorMessage = "Can't edit timestmaps to future dates.";
                return false;
            }

            if (editedExit < editedEntry)
            {
                EditingErrorOccured = true;
                EditingErrorMessage = "Exit date has to be later than entry date.";
                return false;
            }

            var maxAllowedDifference = new TimeSpan(24, 0, 0);
            if(editedExit - editedEntry > maxAllowedDifference) 
            {
                EditingErrorOccured = true;
                EditingErrorMessage = "Specified work time is too long.";
                return false;
            }

            return true;
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

        public DateTime EditedDateEntry
        {
            get
            {
                return _editedDateEntry;
            }
            set
            {
                _editedDateEntry = value;
                OnPropertyChanged();
            }
        }

        public DateTime EditedDateExit
        {
            get
            {
                return _editedDateExit;
            }
            set
            {
                _editedDateExit = value;
                OnPropertyChanged();
            }
        }

        public TimeSpanComponent EditedTimeEntry
        {
            get
            {
                return _editedTimeEntry;
            }
            set
            {
                _editedTimeEntry = value;
                OnPropertyChanged();
            }
        }

        public TimeSpanComponent EditedTimeExit
        {
            get
            {
                return _editedTimeExit;
            }
            set
            {
                _editedTimeExit = value;
                OnPropertyChanged();
            }
        }

        public bool EditingErrorOccured
        {
            get => _editingErrorOccured;
            set
            {
                _editingErrorOccured = value;
                OnPropertyChanged();
            }
        }

        public string EditingErrorMessage
        {
            get => _editingErrorMessage;
            set
            {
                _editingErrorMessage = value;
                OnPropertyChanged();
            }
        }

        public override void Uninitialize()
        {
            base.Uninitialize();
        }
    }
}
