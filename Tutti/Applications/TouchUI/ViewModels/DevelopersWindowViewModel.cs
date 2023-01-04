using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchUI.Dialogs.UserExit;
using TouchUI.Models.Enums;

namespace TouchUI.ViewModels
{
    public class DevelopersWindowViewModel : ViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<DevelopersWindowViewModel>();
        private readonly IDataService _dataService;
        private readonly IIdentificationDeviceService _idDeviceService;

        private string _cardIdentifcatorToBeSimulated;

        public DevelopersWindowViewModel(IDataService dataService, IIdentificationDeviceService idDeviceService, IUserExitDialogController userExitDialogController)
        {
            _logger.Debug("Creating developers window view model.");
            _dataService = dataService;
            _idDeviceService = idDeviceService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SimulateCardIdentificationCommand = new RelayCommand(SimulateCardIdentification);
        }

        private async void SimulateCardIdentification()
        {
            _idDeviceService.SimulateIdentificationEvent(CardIdentifcatorToBeSimulated);
        }

        public string CardIdentifcatorToBeSimulated
        {
            get
            {
                return _cardIdentifcatorToBeSimulated;
            }
            set
            {
                _cardIdentifcatorToBeSimulated = value;
                OnPropertyChanged();
            }
        }

        public ICommand SimulateCardIdentificationCommand { get; set; }
    }
}
