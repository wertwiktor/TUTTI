﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.ViewModels;

namespace TouchUI.Services.Navigation
{
    public interface INavigationService
    {
        void Navigate(Type viewModelType);

        void Navigate<TViewModel>();

        public event Action<NavigationViewModelBase> NavigationChanged;
    }
}
