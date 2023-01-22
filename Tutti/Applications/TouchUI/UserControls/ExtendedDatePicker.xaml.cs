using DataService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
            "SelectedDate", typeof(DateTime), typeof(ExtendedDatePicker), new FrameworkPropertyMetadata(DateTime.MinValue, new PropertyChangedCallback(OnSelectedDatePropertyChanged)));

        private static void OnSelectedDatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = sender as ExtendedDatePicker;
            if(thisControl.Time == null)
            {
                thisControl.Time = thisControl.SelectedDate.TimeOfDay;
            }
            else if(thisControl.SelectedDate.TimeOfDay != thisControl.Time)
            {
                thisControl.SelectedDate = thisControl.SelectedDate + thisControl.Time.Value;
            }
        }

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
