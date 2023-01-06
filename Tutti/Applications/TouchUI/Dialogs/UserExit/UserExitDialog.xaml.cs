using ModernWpf.Controls;
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

namespace TouchUI.Dialogs.UserExit
{
    /// <summary>
    /// Interaction logic for UserExitDialog.xaml
    /// </summary>
    public partial class UserExitDialog : ContentDialog
    {
        public UserExitDialog(string userName, TimeSpan estimatedRecordedTime)
        {
            InitializeComponent();
            (DataContext as UserExitDialogViewModel)?.Initialize(userName, estimatedRecordedTime);
        }
    }
}
