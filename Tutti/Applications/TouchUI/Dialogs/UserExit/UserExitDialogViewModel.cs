using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Dialogs.UserExit
{
    public class UserExitDialogViewModel : DialogViewModelBase
    {
        private string _userName;
        private TimeSpan _workTime;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan WorkTime
        {
            get
            {
                return _workTime;
            }
            set
            {
                _workTime = value;
                OnPropertyChanged();
            }
        }

        public void Initialize(string userName, TimeSpan estimatedRecordedTime)
        {
            UserName = userName;
            WorkTime = estimatedRecordedTime;
        }
    }
}
