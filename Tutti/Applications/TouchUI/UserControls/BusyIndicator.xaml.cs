using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for BusyIndicator.xaml
    /// </summary>
    public partial class BusyIndicator : UserControl
    {

        public BusyIndicator()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(BusyIndicator));

        public static readonly DependencyProperty BusyMessageProperty = DependencyProperty.Register(
            "BusyMessage", typeof(string), typeof(BusyIndicator));

        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }
            set
            {
                SetValue(IsBusyProperty, value);
            }
        }

        public string BusyMessage
        {
            get
            {
                return (string)GetValue(BusyMessageProperty);
            }
            set
            {
                SetValue(BusyMessageProperty, value);
            }
        }
    }
}
