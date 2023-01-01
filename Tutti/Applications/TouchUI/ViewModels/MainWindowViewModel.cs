using DataService.Models;
using Services.DataService;
using System.Collections.ObjectModel;

namespace TouchUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private ObservableCollection<User> _availableUsers;
        public MainWindowViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InitializeAvailableUsers();   
        }

        private void InitializeAvailableUsers()
        {
            if (_dataService == null)
            {
                return;
            }

            AvailableUsers = new ObservableCollection<User>(_dataService.GetAllUsers());
        }

        public ObservableCollection<User> AvailableUsers
        {
            set
            {
                _availableUsers = value;
                OnPropertyChanged();
            }
            get
            {
                return _availableUsers;
            }
        }
    }
}
