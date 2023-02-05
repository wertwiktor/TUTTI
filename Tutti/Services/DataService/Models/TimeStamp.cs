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
        private long _userId;
        private User _user;
        private TimeSpan _worktime;

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
            }
        }
        public DateTime? ExitDate
        {
            get => _exitDate;
            set
            {
                _exitDate = value;
                OnPropertyChanged();
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
        public TimeSpan Worktime
        {
            get => _worktime;
            set
            {
                _worktime = value;
                OnPropertyChanged();
            }
        }
    }
}