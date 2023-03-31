using DataService.Models;
using Framework.ExtensionMethods;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchUI.Models;
using TouchUI.Tools.FileExport.Models;

namespace TouchUI.Tools.WorktimeCalculations
{
    public class WorktimeHelper
    {
        private readonly ILogger _logger = Log.Logger.ForContext<WorktimeHelper>();
        private readonly TimeSpan _shortBreakDuration = new TimeSpan(0, 30, 0);
        private readonly TimeSpan _shortBreakTrigger = new TimeSpan(6, 0, 0);
        private readonly TimeSpan _longBreakDuration = new TimeSpan(0, 45, 0);
        private readonly TimeSpan _longBreakTrigger = new TimeSpan(9, 0, 0);

        public void CalculateWorktimesInTimeStamps(IEnumerable<TimeStamp> timeStamps)
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
                breakTime = CalculateBreakTimeFromWorkDuration(workDuration);

                workTime = exit - entry - breakTime;
                timeStamp.WorkTime = workTime;
                timeStamp.BreakTime = breakTime;
            }
        }

        public WorkdaySummary CalculateWorkdaySummarryFromTimestamps(IEnumerable<TimeStamp> timeStamps)
        {
            var workdaySummary = new WorkdaySummary();
            TimeSpan workDuration = TimeSpan.Zero;

            if (timeStamps == null || !timeStamps.Any(x => x.ResultantEntryDate.HasValue && x.ResultantExitDate.HasValue))
            {
                return workdaySummary;
            }

            workdaySummary.Date = timeStamps.First(x => x.ResultantEntryDate.HasValue).ResultantEntryDate.Value.Date;

            foreach (var timeStamp in timeStamps)
            {
                if (timeStamp.ResultantEntryDate == null || timeStamp.ResultantExitDate == null)
                {
                    continue;
                }
                else if(timeStamp.ResultantEntryDate.Value.Date != workdaySummary.Date)
                {
                    _logger.Error($"Invalid timestamp passed into {nameof(CalculateWorkdaySummarryFromTimestamps)}.".Here());
                    break;
                }

                workDuration = workDuration + (TimeSpan)(timeStamp.ResultantExitDate - timeStamp.ResultantEntryDate);
            }

            var breakTime = CalculateBreakTimeFromWorkDuration(workDuration);
            workdaySummary.BreakTime = breakTime;
            workdaySummary.WorkTime = workDuration - breakTime;

            return workdaySummary;
        }

        private TimeSpan CalculateBreakTimeFromWorkDuration(TimeSpan workDuration)
        {

            if (workDuration < _shortBreakTrigger)
            {
                return new TimeSpan(0, 0, 0);
            }
            else if (workDuration > _shortBreakTrigger && workDuration < _longBreakTrigger)
            {
                return _shortBreakDuration;
            }
            else
            {
                return _longBreakDuration;
            }
        }
    }
}
