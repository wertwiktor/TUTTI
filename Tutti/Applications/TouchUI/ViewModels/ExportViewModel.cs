using Serilog;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchUI.Commands;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.FileExport;

namespace TouchUI.ViewModels
{
    public class ExportViewModel : NavigationViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<ExportViewModel>();
        private readonly IDataService _dataService;

        private DateTime _exportStartDate;
        private DateTime _exportEndDate;
        private bool _exportForAllUsers;
        private ICommand _exportCommand;
        public ExportViewModel(INavigationService navigationService, ILoginService loginService, IDataService dataService) : base(navigationService, loginService)
        {
            _dataService = dataService;

            InitializeDates();
            InitializeCommands();
            //var exporter = new ExporterBuilder(new List<long>() { loginService.GetCurrentUser().Id, 8 }, ExportFormat.Csv, _dataService).Build();
            //exporter.Export();
        }

        private void InitializeDates()
        {
            var today = DateTime.Now.Date;
            var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfCurrentMonth = firstDayOfCurrentMonth.AddMonths(1).AddSeconds(-1);
            ExportStartDate = new DateTime(today.Year, today.Month, 1);
            ExportEndDate = lastDayOfCurrentMonth;
        }

        private void InitializeCommands()
        {
            _exportCommand = new RelayCommand(Export);
        }

        private void Export(object parameter)
        {
            var exporterBuilder = new ExporterBuilder(ExportFormat.Csv, _dataService)
                .SetTimeRange(DateOnly.FromDateTime(ExportStartDate), DateOnly.FromDateTime(ExportEndDate));
            if (ExportForAllUsers)
            {
                var allUsers = _dataService.GetAllUsers();
                var allUserIds = allUsers.Select(x => x.Id).ToList();
                exporterBuilder.SetUsers(allUserIds);

                var fileName = AddTimeStampToString("AllUsersExport");
                exporterBuilder.SetFileName(fileName);
            }
            else
            {
                exporterBuilder.SetUser(CurrentUser.Id);

                var fileName = AddTimeStampToString(CurrentUser.FullName);
                exporterBuilder.SetFileName(fileName);
            }

            BusyMessage = "Exporting data";
            IsBusy = true;
            Task.Factory.StartNew(() =>
            {
                exporterBuilder.Build().Export();
                IsBusy = false;
            });
        }

        private string AddTimeStampToString(string input)
        {
            return string.Join(" ", input, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        public DateTime ExportStartDate
        {
            get
            {
                return _exportStartDate;
            }
            set
            {
                _exportStartDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExportEndDate
        {
            get
            {
                return _exportEndDate;
            }
            set
            {
                _exportEndDate = value;
                OnPropertyChanged();
            }
        }

        public bool ExportForAllUsers
        {
            get
            {
                return _exportForAllUsers;
            }
            set
            {
                _exportForAllUsers = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                return _exportCommand;
            }
            set
            {
                _exportCommand = value;
                OnPropertyChanged();
            }
        }
    }
}
