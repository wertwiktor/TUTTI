using Framework.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchUI.Models
{
    public class TimeSpanComponent : ModelBase
    {
        private int _seconds;
        private int _minutes;
        private int _hours;

        public TimeSpanComponent()
        {

        }

        public TimeSpanComponent(TimeSpan timeSpan)
        {
            _hours = timeSpan.Hours;
            _minutes = timeSpan.Minutes;
            _seconds = timeSpan.Seconds;
        }

        public TimeSpanComponent(int hours, int minutes, int seconds)
        {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;         
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(_hours, _minutes, _seconds);
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }
    }
}
