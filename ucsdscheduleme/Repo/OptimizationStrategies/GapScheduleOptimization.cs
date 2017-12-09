using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public abstract class GapScheduleOptimization : ScheduleOptimization
    {
        /// <summary>
        /// Calculates the total gap size for a schedule.
        /// </summary>
        /// <param name="schedule">Schedule of classes</param>
        /// <returns>The total time gaps for a particular schedule</returns>
        protected static TimeSpan TotalGap(List<Section> schedule)
        {
            TimeSpan gap = new TimeSpan(0);
            List<DateTime> Monday = new List<DateTime>();
            List<DateTime> Tuesday = new List<DateTime>();
            List<DateTime> Wednesday = new List<DateTime>();
            List<DateTime> Thursday = new List<DateTime>();
            List<DateTime> Friday = new List<DateTime>();

            var allMeetings = schedule.SelectMany(s => s.Meetings);
            foreach (Meeting meeting in allMeetings)
            {
                if (!FormatRepo.IsOneTimeEvent(meeting.MeetingType)
                    && meeting.StartTime.Hour != 0
                    && meeting.EndTime.Hour != 0)
                {
                    if (meeting.Days.HasFlag(Days.Monday))
                    {
                        Monday.Add(meeting.StartTime);
                        Monday.Add(meeting.EndTime);
                    }
                    if (meeting.Days.HasFlag(Days.Tuesday))
                    {
                        Tuesday.Add(meeting.StartTime);
                        Tuesday.Add(meeting.EndTime);
                    }
                    if (meeting.Days.HasFlag(Days.Wednesday))
                    {
                        Wednesday.Add(meeting.StartTime);
                        Wednesday.Add(meeting.EndTime);
                    }
                    if (meeting.Days.HasFlag(Days.Thursday))
                    {
                        Thursday.Add(meeting.StartTime);
                        Thursday.Add(meeting.EndTime);
                    }
                    if (meeting.Days.HasFlag(Days.Friday))
                    {
                        Friday.Add(meeting.StartTime);
                        Friday.Add(meeting.EndTime);
                    }
                }
            }

            // Sort the class times in each schedule by start time.
            Monday.Sort();
            Tuesday.Sort();
            Wednesday.Sort();
            Thursday.Sort();
            Friday.Sort();

            // Calculate the gaps in the schedule.
            for (int i = 1; i < Monday.Count - 1; i += 2)
            {
                gap += (Monday[i + 1].TimeOfDay - Monday[i].TimeOfDay);
            }

            for (int i = 1; i < Tuesday.Count - 1; i += 2)
            {
                gap += (Tuesday[i + 1].TimeOfDay - Tuesday[i].TimeOfDay);
            }

            for (int i = 1; i < Wednesday.Count - 1; i += 2)
            {
                gap += (Wednesday[i + 1].TimeOfDay - Wednesday[i].TimeOfDay);
            }

            for (int i = 1; i < Thursday.Count - 1; i += 2)
            {
                gap += (Thursday[i + 1].TimeOfDay - Thursday[i].TimeOfDay);
            }

            for (int i = 1; i < Friday.Count - 1; i += 2)
            {
                gap += (Friday[i + 1].TimeOfDay - Friday[i].TimeOfDay);
            }

            return gap;
        }
    }
}
