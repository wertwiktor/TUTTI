﻿using Framework.ExtensionMethods;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchUI.Commands;

namespace TouchUI.ViewModels
{
    public class DevelopersWindowViewModel : ViewModelBase
    {
        private readonly ILogger _logger = Log.Logger.ForContext<DevelopersWindowViewModel>();
        private readonly IDataService _dataService;
        private readonly IIdentificationDeviceService _idDeviceService;

        private string _cardIdentifcatorToBeSimulated;

        public DevelopersWindowViewModel(IDataService dataService, IIdentificationDeviceService idDeviceService)
        {
            _logger.Debug("Creating developers window view model.".Here());
            _dataService = dataService;
            _idDeviceService = idDeviceService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SimulateCardIdentificationCommand = new RelayCommand(SimulateCardIdentification);
        }

        private void SimulateCardIdentification(object parameter)
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
