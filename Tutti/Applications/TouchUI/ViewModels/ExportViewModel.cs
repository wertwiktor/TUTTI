using Serilog;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.FileExport;

namespace TouchUI.ViewModels
{
    public class ExportViewModel : NavigationViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<ExportViewModel>();
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private readonly IDataService _dataService;
        public ExportViewModel(INavigationService navigationService, ILoginService loginService, IDataService dataService) : base(navigationService, loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _dataService = dataService;

            var exporter = new ExporterBuilder(new List<long>() { loginService.GetCurrentUser().Id, 8 }, ExportFormat.Csv, _dataService).Build();
            exporter.Export();
        }
    }
}
