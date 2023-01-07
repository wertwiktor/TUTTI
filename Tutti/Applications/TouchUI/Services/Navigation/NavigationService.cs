using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.ViewModels;

namespace TouchUI.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ILogger _logger = Log.Logger.ForContext<NavigationService>();

        private Dictionary<Type, ViewModelBase> _viewModels = new Dictionary<Type, ViewModelBase>();

        public event Action<ViewModelBase> NavigationChanged;

        public void Navigate<TViewModel>()
        {
            ViewModelBase viewModel; 
                
            if(_viewModels.TryGetValue(typeof(TViewModel), out viewModel))
            {
                NavigationChanged?.Invoke(viewModel);
            }
            else
            {
                _logger.Error("Attempted to navigate to an unknown ViewModel: {viewModel}", nameof(TViewModel));
            }
        }

        public void Register<TViewModel>(ViewModelBase viewModel)
            where TViewModel : ViewModelBase
        {
            _viewModels[typeof(TViewModel)] = viewModel;
        }
    }
}
