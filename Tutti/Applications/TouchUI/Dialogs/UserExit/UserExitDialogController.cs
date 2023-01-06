using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TouchUI.Views;

namespace TouchUI.Dialogs.UserExit
{
    public  class UserExitDialogController : IUserExitDialogController
    {
        public UserExitDialogController() 
        { 
        
        }

        //ShowDialogAsync is not awaited and instead, additional TaskCompletionSource is used 
        // as it allows faster return to the calling thread.
        public Task<bool> ConfirmUserExitAsync(string userName, TimeSpan estimatedRecordedTime)
        {
            var dialog = new UserExitDialog(userName, estimatedRecordedTime);
            dialog.Owner = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            var taskCompletionSource = new TaskCompletionSource<bool>();
            dialog.PrimaryButtonClick += (o, e) =>
            {
                taskCompletionSource.SetResult(true);
            };
            dialog.SecondaryButtonClick += (o, e) =>
            {
                taskCompletionSource.SetResult(false);
            };
            dialog.ShowAsync();
            return taskCompletionSource.Task;
        }        
    }
}
