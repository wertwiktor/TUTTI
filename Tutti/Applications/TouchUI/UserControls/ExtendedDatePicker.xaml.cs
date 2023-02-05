using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace TouchUI.UserControls
{
    /// <summary>
    /// Interaction logic for ExtendedDatePicker.xaml
    /// </summary>
    public partial class ExtendedDatePicker : UserControl, INotifyPropertyChanged
    {
        private DateTime _displayDate;
        public ExtendedDatePicker()
        {
            InitializeComponent();
        }

        public void OnDatePickerButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedDate == DateTime.MinValue) 
            {
                DisplayDate = DateTime.Now;            
            }

            DatePicker.IsDropDownOpen = true;
        }

        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register(
            "SelectedDate", typeof(DateTime), typeof(ExtendedDatePicker));
        public DateTime SelectedDate
        {
            get
            {
                return (DateTime)GetValue(SelectedDateProperty);
            }
            set
            {
                SetValue(SelectedDateProperty, value);
            }
        }

        public DateTime DisplayDate
        {
            get => _displayDate;
            set
            {
                _displayDate = value;
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
