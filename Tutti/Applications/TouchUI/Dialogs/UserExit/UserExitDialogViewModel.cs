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

        public override void Initialize(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return;
            }

            UserName = parameters[0] as string;
        }
    }
}
