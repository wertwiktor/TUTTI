﻿using Framework.ExtensionMethods;
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

        private Dictionary<Type, Func<NavigationViewModelBase>> _viewModels = new Dictionary<Type, Func<NavigationViewModelBase>>();

        public NavigationService(Func<HomeViewModel> homeViewModelConstructor, 
            Func<RegisterViewModel> registerViewModelConstructor, 
            Func<HistoryViewModel> historyViewModelConstructor,
            Func<ExportViewModel> exportViewModelConstructor)
        {
            _viewModels[typeof(HomeViewModel)] = homeViewModelConstructor;
            _viewModels[typeof(RegisterViewModel)] = registerViewModelConstructor;
            _viewModels[typeof(HistoryViewModel)] = historyViewModelConstructor;
            _viewModels[typeof(ExportViewModel)] = exportViewModelConstructor;
        }

        public event Action<NavigationViewModelBase> NavigationChanged;

        public void Navigate(Type viewModelType)
        {
            if (viewModelType == null)
            {
                _logger.Error("Attempted to navigate to a null view model.".Here());
                return;
            }

            Func<NavigationViewModelBase> viewModelConstructor; 

            if (_viewModels.TryGetValue(viewModelType, out viewModelConstructor))
            {
                var viewModel = viewModelConstructor.Invoke();
                NavigationChanged?.Invoke(viewModel);
            }
            else
            {
                _logger.Error("Attempted to navigate to an unknown ViewModel {vmType}. Make sure that this ViewModel is registered in the dependency injection container and is known to NavigationService.".Here(), viewModelType.FullName);
            }
        }

        public void Navigate<TViewModel>()
        {
            Func<NavigationViewModelBase> viewModelConstructor;

            if (_viewModels.TryGetValue(typeof(TViewModel), out viewModelConstructor))
            {
                NavigationChanged?.Invoke(viewModelConstructor.Invoke());
            }
            else
            {
                _logger.Error("Attempted to navigate to an unknown ViewModel {vmType}.  Make sure that this ViewModel is registered in the dependency injection container and is known to NavigationService.".Here(), typeof(TViewModel).FullName);
            }
        }
    }
}
