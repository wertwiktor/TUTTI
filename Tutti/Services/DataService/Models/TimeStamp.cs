using Framework.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataService.Models
{
    public class TimeStamp : ModelBase
    {
        private long _id;
        private DateTime? _entryDate;
        private DateTime? _exitDate;
        private DateTime? _editedEntryDate;
        private DateTime? _editedExitDate;
        private long _userId;
        private User _user;
        private TimeSpan _workTime;
        private TimeSpan _breakTime;

        public long Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EntryDate
        {
            get => _entryDate;
            set
            {
                _entryDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ResultantEntryDate));
            }
        }

        public DateTime? ExitDate
        {
            get => _exitDate;
            set
            {
                _exitDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ResultantExitDate));
            }
        }

        public DateTime? EditedEntryDate
        {
            get => _editedEntryDate;
            set
            {
                _editedEntryDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ResultantEntryDate));
            }
        }

        public DateTime? EditedExitDate
        {
            get => _editedExitDate;
            set
            {
                _editedExitDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ResultantExitDate));
            }
        }

        public long UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public TimeSpan WorkTime
        {
            get => _workTime;
            set
            {
                _workTime = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public TimeSpan BreakTime
        {
            get => _breakTime;
            set
            {
                _breakTime = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public DateTime? ResultantEntryDate
        {
            get
            {
                if (EntryDate == null && EditedEntryDate == null)
                {
                    return null;
                }

                if (EntryDate != null && EditedEntryDate == null)
                {
                    return EntryDate;
                }

                return EditedEntryDate;
            }
        }

        [NotMapped]
        public DateTime? ResultantExitDate
        {
            get
            {
                if (ExitDate == null && EditedExitDate == null)
                {
                    return null;
                }

                if (ExitDate != null && EditedExitDate == null)
                {
                    return ExitDate;
                }

                return EditedExitDate;
            }
        }
    }
}