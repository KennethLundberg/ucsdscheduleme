using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class HighestRateMyProfessorOptimization : ScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the highest overall quality from RateMyProfessor.
        /// </summary>
        /// <returns>The schedule with the hightest overall quality from RateMyProfessor.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            List<Section> result = possibleSchedules[0];
            decimal highestRating = 0;

            foreach (List<Section> schedule in possibleSchedules)
            {
                decimal currentRating = 0;

                foreach (Section section in schedule)
                {
                    currentRating += section.Professor.RateMyProfessor.OverallQuality;
                }

                // Compare GPA of this schedule to the highest schedule GPA
                if (currentRating > highestRating)
                {
                    highestRating = currentRating;
                    result = schedule;
                }
            }

            return result;
        }
    }
}
