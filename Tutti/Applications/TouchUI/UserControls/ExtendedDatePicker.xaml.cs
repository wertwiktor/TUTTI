using System;
using System.Windows;
using System.Windows.Controls;

namespace TouchUI.UserControls
{
    /// <summary>
    /// Interaction logic for ExtendedDatePicker.xaml
    /// </summary>
    public partial class ExtendedDatePicker : UserControl
    {
        public TimeSpan? Time;   
        public ExtendedDatePicker()
        {
            InitializeComponent();
        }

        public void OnDatePickerButtonClick(object sender, RoutedEventArgs e)
        {
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
    }
}
