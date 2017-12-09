using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class LeastDaysOptimization : TimeScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the least days scheduled.
        /// </summary>
        /// <returns>The schedule with the least days.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            int leastDay = 10;
            List<Section> leastDaySchedule = possibleSchedules[0];

            // Iterate through the possible schedules and counting the number of days
            // per schedule
            foreach (List<Section> schedule in possibleSchedules)
            {
                int numDays = NumDays(schedule);

                // Checks to see if the current schedule has less days than the current least.
                if (numDays < leastDay)
                {
                    leastDay = numDays;
                    leastDaySchedule = schedule;
                }
            }
            return leastDaySchedule;
        }
    }
}
