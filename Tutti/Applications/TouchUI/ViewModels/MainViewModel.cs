using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Services.Navigation;

namespace TouchUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private ViewModelBase _currentViewModel;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.NavigationChanged += OnNavigationServiceNavigationChanged;
        }

        private void OnNavigationServiceNavigationChanged(ViewModelBase navigatedViewModel)
        {
            CurrentViewModel= navigatedViewModel;
        }

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
