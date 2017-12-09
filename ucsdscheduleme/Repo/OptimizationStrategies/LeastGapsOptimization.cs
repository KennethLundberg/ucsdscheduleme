using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class LeastGapsOptimization : GapScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the least gaps in between classes.
        /// </summary>
        /// <returns>The schedule with the least gaps.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            List<Section> leastGapsSchedule = possibleSchedules[0];
            TimeSpan leastGap = new TimeSpan(99, 59, 59);

            // Iterate through the possible schedules and separate the times into respective days.
            foreach (List<Section> schedule in possibleSchedules)
            {
                TimeSpan currGap = TotalGap(schedule);

                // Checks to see if current schedule has less gaps than current least.
                if (currGap < leastGap)
                {
                    leastGapsSchedule = schedule;
                    leastGap = currGap;
                }
            }
            return leastGapsSchedule;
        }
    }
}
