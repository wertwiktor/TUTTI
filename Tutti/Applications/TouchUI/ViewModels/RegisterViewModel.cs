using System.Collections.ObjectModel;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class RegisterViewModel : NavigationViewModelBase
    {
        public RegisterViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService, loginService)
        {

        }

        public override void Uninitialize()
        {
            base.Uninitialize();
        }
    }
}
