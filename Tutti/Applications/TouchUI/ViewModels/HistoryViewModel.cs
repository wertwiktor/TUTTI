using DataService.Models;
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

        public HistoryViewModel(IDataService dataService,
            IIdentificationDeviceService idDeviceService,
            INavigationService navigationService,
            ILoginService loginService)
            : base(navigationService, loginService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            InitializeHistory();
        }

        private void InitializeHistory()
        {
            if (CurrentUser == null)
            {
                _logger.Error("Current user was null when trying to initialize timestamps history.");
                return;
            }
            _timeStampsHistory.Clear();
            var oldestTimeStampDate = DateTime.Now - TimeSpan.FromDays(30);
            var newestTimeStampDate = DateTime.Now;
            var timeStampHistory = _dataService.GetTimeStamps(CurrentUser.Id, oldestTimeStampDate, newestTimeStampDate).OrderByDescending(x => x.DateTime);
            TimeStampsHistory = new ObservableCollection<TimeStamp>(timeStampHistory);
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

        public override void Uninitialize()
        {
            base.Uninitialize();
        }
    }
}
