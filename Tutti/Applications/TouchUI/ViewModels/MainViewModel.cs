using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using TouchUI.Services.Navigation;

namespace TouchUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private NavigationViewModelBase _currentViewModel;
        private DateTime _clockDateTime = DateTime.Now;
        private DispatcherTimer _clockDisplayTimer = new DispatcherTimer();
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.NavigationChanged += OnNavigationServiceNavigationChanged;
            InitializeClockDisplayTimer();
        }

        private void OnNavigationServiceNavigationChanged(NavigationViewModelBase navigatedViewModel)
        {
            CurrentViewModel= navigatedViewModel;
        }

        private void InitializeClockDisplayTimer()
        {
            _clockDisplayTimer.Interval = TimeSpan.FromSeconds(1);
            _clockDisplayTimer.Tick += OnClockDisplayTimerElapsed;
            _clockDisplayTimer.Start();
        }

        private void UninitializeClockDisplayTimer()
        {
            _clockDisplayTimer.Stop();
        }

        private void OnClockDisplayTimerElapsed(object? sender, EventArgs e)
        {
            ClockDateTime = DateTime.Now;
        }

        public NavigationViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel?.Uninitialize();
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public DateTime ClockDateTime
        {
            get
            {
                return _clockDateTime;
            }
            set
            {
                _clockDateTime = value;
                OnPropertyChanged();
            }
        }
    }
}
