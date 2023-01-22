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

        private int _selectedMinute;
        private int _selectedHour;
        public object TimeUpdateLock = new object();
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

        private void UpdateTime()
        {
            Time = Time.Date + new TimeSpan(SelectedHour, SelectedMinute, 0);
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(DateTime), typeof(TimePicker), new FrameworkPropertyMetadata(DateTime.MinValue, new PropertyChangedCallback(OnTimePropertyChanged)));

        private static void OnTimePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = sender as TimePicker;
            thisControl.InitializeSelectedHourAndMinute(thisControl.Time.Hour, thisControl.Time.Minute);
        }

        public void InitializeSelectedHourAndMinute(int selectedHour, int selectedMinute)
        {
            _selectedHour = selectedHour;
            OnPropertyChanged(nameof(SelectedHour));
            _selectedMinute = selectedMinute;
            OnPropertyChanged(nameof(SelectedMinute));
        }

        public DateTime Time
        {
            get
            {
                return (DateTime)GetValue(TimeProperty);
            }
            set
            {
                SetValue(TimeProperty, value);
            }
        }

        public int SelectedHour
        {
            get
            {
                return _selectedHour;
            }
            set
            {
                if (value == _selectedHour)
                {
                    return;
                }
                _selectedHour = value;
                OnPropertyChanged();
                UpdateTime();
            }
        }

        public int SelectedMinute
        {
            get
            {
                return _selectedMinute;
            }
            set
            {
                if (value == _selectedMinute)
                {
                    return;
                }
                _selectedMinute = value;
                OnPropertyChanged();
                UpdateTime();
            }
        }



        public List<int> Hours { get; private set; }
        public List<int> Minutes { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ComboBoxHourSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxMinuteSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
