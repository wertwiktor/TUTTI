using Framework.MVVM;
using System;
using System.Collections.Generic;
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

namespace TouchUI.UserControls
{
    /// <summary>
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl, INotifyPropertyChanged
    {
        private int _selectedHour;
        private int _selectedMinute;

        public TimePicker()
        {
            InitializeComponent();
            InitializeHours();
            InitializeMinutes();
        }

        private void InitializeHours()
        {
            Hours = Enumerable.Range(0, 24).ToList();

        }
        private void InitializeMinutes()
        {
            Minutes = Enumerable.Range(0, 60).ToList();
        }

        public static readonly DependencyProperty SelectedHourProperty = DependencyProperty.Register(
            "SelectedHour", typeof(int), typeof(TimePicker));

        public static readonly DependencyProperty SelectedMinuteProperty = DependencyProperty.Register(
            "SelectedMinute", typeof(int), typeof(TimePicker));


        public List<int> Hours { get; private set; }
        public List<int> Minutes { get; private set; }

        public int SelectedHour
        {
            get { return _selectedHour; }
            set
            {
                _selectedHour = value;
                OnPropertyChanged();
            }
        }

        public int SelectedMinute
        {
            get { return _selectedMinute; }
            set
            {
                _selectedMinute = value;
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
