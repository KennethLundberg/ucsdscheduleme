using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class MostGapsOptimization : GapScheduleOptimization
    {

        /// <summary>
        /// Returns the schedule with the most gaps in between classes.
        /// </summary>
        /// <returns>The schedule with the most gaps.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            List<Section> mostGapsSchedule = possibleSchedules[0];
            TimeSpan mostGap = new TimeSpan(0, 0, 0);

            // Iterate through the possible schedules and separate the times into respective days.
            foreach (List<Section> schedule in possibleSchedules)
            {
                TimeSpan currGap = TotalGap(schedule);

                // Checks to see if current schedule has less gaps than current least.
                if (currGap > mostGap)
                {
                    mostGapsSchedule = schedule;
                    mostGap = currGap;
                }
            }
            return mostGapsSchedule;
        }
    }
}
