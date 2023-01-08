using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Services.Navigation;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public class RegisterViewModel : NavigationViewModelBase
    {
        public RegisterViewModel(INavigationService navigationService) : base(navigationService) 
        { 
        
        }

        public override void Uninitialize()
        {
            
        }

        protected override void InitializeNavigatableViewModels()
        {
            NavigatableViewModels.Clear();
            NavigatableViewModels.Add(new NavigationTarget(typeof(HomeViewModel), "Home", true));
        }
    }
}
