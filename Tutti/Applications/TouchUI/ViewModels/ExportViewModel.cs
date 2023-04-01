using Serilog;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private ICommand _openExportDirectoryCommand;
        private string _exportPath;
        public ExportViewModel(INavigationService navigationService, ILoginService loginService, IDataService dataService) : base(navigationService, loginService)
        {
            _dataService = dataService;
            _exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TUTTI\\Exports");

            InitializeDates();
            InitializeCommands();
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
            _openExportDirectoryCommand = new RelayCommand(OpenExportDirectory);
        }

        private void Export(object parameter)
        {
            BusyMessage = "Exporting data";
            IsBusy = true;

            var exporterBuilder = new ExporterBuilder(ExportFormat.Csv, _dataService)
                .SetTimeRange(DateOnly.FromDateTime(ExportStartDate), DateOnly.FromDateTime(ExportEndDate));

            string fileName;

            if (ExportForAllUsers)
            {
                Task.Factory.StartNew(() =>
                {
                    fileName = AddTimeStampToString("_AllUsersExport");
                    exporterBuilder.SetFileName(fileName);

                    var subdirectory = $"AllUsersExport_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    exporterBuilder.SetExportSubdirectory(subdirectory);

                    var allUsers = _dataService.GetAllUsers();
                    var allUserIds = allUsers.Select(x => x.Id).ToList();
                    exporterBuilder.SetUsers(allUserIds);


                    exporterBuilder.Build().Export();

                    foreach(var user in allUsers ) 
                    {
                        exporterBuilder.SetUser(user.Id);

                        fileName = AddTimeStampToString(user.FullName);
                        exporterBuilder.SetFileName(fileName);

                        exporterBuilder.Build().Export();
                    }
                    IsBusy = false;
                });
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    exporterBuilder.SetUser(CurrentUser.Id);

                    fileName = AddTimeStampToString(CurrentUser.FullName);
                    exporterBuilder.SetFileName(fileName);

                    exporterBuilder.Build().Export();

                    IsBusy = false;
                });
            }
        }

        private string AddTimeStampToString(string input)
        {
            return string.Join(" ", input, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        private void OpenExportDirectory(object parameter)
        {
            Process.Start("explorer.exe", _exportPath);
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

        public ICommand OpenExportDirectoryCommand
        {
            get
            {
                return _openExportDirectoryCommand;
            }
            set
            {
                _openExportDirectoryCommand = value;
                OnPropertyChanged();
            }
        }
    }
}
