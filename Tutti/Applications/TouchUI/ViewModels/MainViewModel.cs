using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using TouchUI.Services.Navigation;

namespace TouchUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private NavigationViewModelBase _currentViewModel;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.NavigationChanged += OnNavigationServiceNavigationChanged;
        }

        private void OnNavigationServiceNavigationChanged(NavigationViewModelBase navigatedViewModel)
        {
            CurrentViewModel= navigatedViewModel;
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
    }
}
