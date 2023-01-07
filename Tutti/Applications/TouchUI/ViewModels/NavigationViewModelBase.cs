using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Tools.Navigation;

namespace TouchUI.ViewModels
{
    public abstract class NavigationViewModelBase : ViewModelBase
    {
        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget>();

        public NavigationViewModelBase()
        {
            InitializeNavigatableViewModels();
        }

        public ObservableCollection<NavigationTarget> NavigatableViewModels
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

        protected abstract void InitializeNavigatableViewModels();
    }
}
