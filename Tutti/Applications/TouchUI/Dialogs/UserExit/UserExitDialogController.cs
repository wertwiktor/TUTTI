using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouchUI.Views;

namespace TouchUI.Dialogs.UserExit
{
    public  class UserExitDialogController : IUserExitDialogController
    {
        public UserExitDialogController() 
        { 
        
        }

        public async Task<bool> ShowDialog(string userName)
        {
            var dialog = new UserExitDialog();
            dialog.Owner = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            dialog.Initialize(userName);
            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
    }
}
