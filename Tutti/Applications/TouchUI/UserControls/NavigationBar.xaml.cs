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
using DataService.Models;
using TouchUI.Models;

namespace TouchUI.UserControls
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        private ObservableCollection<NavigationTarget> _navigatableViewModels = new ObservableCollection<NavigationTarget>();
        private ICommand _navigationCommand;
        private User _user;
        public NavigationBar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NavigatableViewModelsProperty = DependencyProperty.Register(
            "NavigatableViewModels", typeof(ObservableCollection<NavigationTarget>), typeof(NavigationBar));
        public static readonly DependencyProperty NavigationCommandProperty = DependencyProperty.Register(
            "NavigationCommand", typeof(ICommand), typeof(NavigationBar));
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register(
            "User", typeof(User), typeof(NavigationBar));

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
                return (ICommand)GetValue(NavigationCommandProperty);
            }
            set
            {
                SetValue(NavigationCommandProperty, value);
            }
        }

        public User User
        {
            get
            {
                return (User)GetValue(UserProperty);
            }
            set
            {
                SetValue(UserProperty, value);
            }
        }
    }
}
