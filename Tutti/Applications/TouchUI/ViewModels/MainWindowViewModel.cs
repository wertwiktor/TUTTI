using DataService.Models;
using Services.DataService;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace TouchUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private DateTime _currentDateTime;
        public MainWindowViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InitializeDispatcherTimer();
        }

        private void InitializeDispatcherTimer()
        {
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += OnDispatcherTimerElapsed;
            dispatcherTimer.Start();
        }

        private void OnDispatcherTimerElapsed(object? sender, EventArgs e)
        {
            CurrentDateTime = DateTime.Now;
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
    }
}
