using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class HighestGPAOptimization : ScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the highest received GPAs from CAPEs.
        /// </summary>
        /// <returns>The schedule with highest CAPEs GPA</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            List<Section> result = possibleSchedules[0];
            decimal highestGPA = 0;

            foreach (List<Section> schedule in possibleSchedules)
            {
                decimal currentGPA = 0;

                foreach (Section section in schedule)
                {
                    // Find a cape that corresponds to the course and professor
                    var cape = section.Course.Cape.First(c => c.ProfessorId == section.Professor.Id);

                    currentGPA += cape?.AverageGradeReceived ?? 0M;
                }

                // Compare GPA of this schedule to the highest schedule GPA
                if (currentGPA > highestGPA)
                {
                    highestGPA = currentGPA;
                    result = schedule;
                }
            }

            return result;
        }
    }
}
