using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Dialogs.UserExit
{
    public interface IUserExitDialogController
    {
        Task<bool> ConfirmUserExitAsync(string userName, TimeSpan estimatedRecordedTime);
    }
}
