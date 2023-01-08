using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TouchUI.Commands;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public abstract class NavigationViewModelBase : ViewModelBase
    {
        protected readonly INavigationService _navigationService;
        private ICommand _navigationCommand;

        public NavigationViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationCommand = new NavigationCommand(_navigationService);
        }

        public abstract ObservableCollection<NavigationTarget> NavigatableViewModels { get; set; }

        public ICommand NavigationCommand
        {
            get
            {
                return _navigationCommand;
            }
            set
            {
                _navigationCommand = value;
                OnPropertyChanged();
            }
        }

        public abstract void Uninitialize();
    }
}
