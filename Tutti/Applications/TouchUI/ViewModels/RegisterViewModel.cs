using System.Collections.ObjectModel;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class RegisterViewModel : NavigationViewModelBase
    {
        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget> { new NavigationTarget(typeof(HomeViewModel), "Home", true) };

        public RegisterViewModel(INavigationService navigationService) : base(navigationService) 
        { 
        
        }

        public override ObservableCollection<NavigationTarget> NavigatableViewModels
        {
            get
            {
                return _navigatableViewModels;
            }
            set
            {
                _navigatableViewModels = value;
                OnPropertyChanged();
            }
        }

        public override void Uninitialize()
        {
            
        }
    }
}
