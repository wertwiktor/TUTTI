using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TouchUI.Services.Navigation;
using TouchUI.Commands;
using TouchUI.Tools.Navigation;

namespace TouchUI.UserControls
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget>();
        private ICommand _navigationCommand;
        public NavigationBar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NavigatableViewModelsProperty = DependencyProperty.Register(
            "NavigatableViewModels", typeof(ObservableCollection<NavigationTarget>), typeof(NavigationBar));
        public static readonly DependencyProperty NavigationCommandProperty = DependencyProperty.Register(
            "NavigationCommand", typeof(ICommand), typeof(NavigationBar));

        public ObservableCollection<NavigationTarget> NavigatableViewModels
        {
            get
            {
                return (ObservableCollection<NavigationTarget>)GetValue(NavigatableViewModelsProperty);
            }
            set
            {
                SetValue(NavigatableViewModelsProperty, value);
            }
        }

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
