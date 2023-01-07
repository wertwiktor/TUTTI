using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouchUI.Services.Navigation;
using TouchUI.ViewModels;

namespace TouchUI.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public NavigationCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if(_navigationService != null && parameter != null && parameter is Type viewModelType)
            {
                _navigationService.Navigate(viewModelType);
            }        
        }
    }
}
