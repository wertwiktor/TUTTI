using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Dialogs
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        public abstract void Initialize(params object[] parameters);
    }
}
