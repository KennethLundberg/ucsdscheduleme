using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public abstract class TimeScheduleOptimization : ScheduleOptimization
    {
        /// <summary>
        /// Calculates the number of days the schedule has classes on
        /// </summary>
        /// <param name="schedule">A schedule of classes</param>
        /// <returns> the number of days in the schedule</returns>
        protected static int NumDays(List<Section> schedule)
        {
            int numDays = 0;

            var allDays = schedule.SelectMany(s => s.Meetings)
                                  .Where(m => !FormatRepo.IsOneTimeEvent(m.MeetingType))
                                  .Select(m => m.Days);

            List<Days> days = Enum.GetValues(typeof(Days)).Cast<Days>().ToList();

            foreach (var day in days)
            {
                if (allDays.Any(d => d.HasFlag(day)))
                {
                    numDays++;
                }
            }

            return numDays;
        }
    }
}
