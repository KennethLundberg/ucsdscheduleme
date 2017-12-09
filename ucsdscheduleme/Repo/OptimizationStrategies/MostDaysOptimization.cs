using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class MostDaysOptimization : TimeScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the most days scheduled.
        /// </summary>
        /// <returns>The schedule with the most days.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            int mostDay = 0;
            List<Section> mostDaySchedule = possibleSchedules[0];

            // Iterate through the possible schedules and counts how many days are scheduled.
            foreach (List<Section> schedule in possibleSchedules)
            {
                int numDays = NumDays(schedule);

                // Compares the current schedule days to the current max days.
                if (numDays > mostDay)
                {
                    mostDay = numDays;
                    mostDaySchedule = schedule;
                }
            }
            return mostDaySchedule;
        }
    }
}
