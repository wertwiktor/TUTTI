﻿using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Models;

namespace TouchUI.Tools.WorktimeCalculations
{
    public class WorktimeHelper
    {
        private readonly TimeSpan _shortBreakDuration = new TimeSpan(0, 30, 0);
        private readonly TimeSpan _shortBreakTrigger= new TimeSpan(6, 0, 0);
        private readonly TimeSpan _longBreakDuration = new TimeSpan(0, 45, 0);
        private readonly TimeSpan _longBreakTrigger = new TimeSpan(9, 0, 0);

        public void CalculateWorktimesInTimeStamps(List<TimeStamp> timeStamps)
        {
            foreach (var timeStamp in timeStamps)
            {
                TimeSpan workTime = new TimeSpan(0, 0, 0);
                TimeSpan breakTime = new TimeSpan(0, 0, 0);
                DateTime entry;
                DateTime exit;

                if (!timeStamp.ResultantEntryDate.HasValue || !timeStamp.ResultantExitDate.HasValue || timeStamp.ResultantEntryDate.Value >= timeStamp.ResultantExitDate.Value)
                {
                    timeStamp.WorkTime = workTime;
                    timeStamp.BreakTime = breakTime;
                    continue;
                }

                entry = timeStamp.ResultantEntryDate.Value;
                exit = timeStamp.ResultantExitDate.Value;

                var workDuration = exit - entry;
                if(workDuration < _shortBreakTrigger)
                {
                    breakTime = new TimeSpan(0, 0, 0);
                }
                else if(workDuration > _shortBreakTrigger && workDuration < _longBreakTrigger)
                {
                    breakTime = _shortBreakDuration;
                }    
                else
                {
                    breakTime = _longBreakDuration;
                }

                workTime = exit - entry - breakTime;
                workTime = new TimeSpan(workTime.Hours, workTime.Minutes, workTime.Seconds); //This prevents storing of anything smaller than seconds.
                timeStamp.WorkTime = workTime;
                timeStamp.BreakTime = breakTime;
            }
        }
    }
}
