using System.Collections.ObjectModel;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class RegisterViewModel : NavigationViewModelBase
    {
        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget> {
            new NavigationTarget(typeof(HomeViewModel), "Home", true),
            new NavigationTarget(typeof(RegisterViewModel), "Register", true),
            new NavigationTarget(typeof(HistoryViewModel), "History", true)};

        public RegisterViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService, loginService)
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
            base.Uninitialize();
        }
    }
}
