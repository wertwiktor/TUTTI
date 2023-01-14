using DataService.Models;
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
    /// Interaction logic for ExtendedDatePicker.xaml
    /// </summary>
    public partial class ExtendedDatePicker : UserControl
    {
        
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
