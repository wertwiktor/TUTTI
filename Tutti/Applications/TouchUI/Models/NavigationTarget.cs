using Framework.MVVM;
using System;

namespace TouchUI.Models
{
    public class NavigationTarget : ModelBase
    {
        private Type _type;
        private string _name;
        private bool _isEnabled;

        public NavigationTarget(Type type, string name, bool isEnabled)
        {
            _type = type;
            _name = name;
            _isEnabled = isEnabled;
        }

        public Type Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; OnPropertyChanged(); }
        }
    }
}
