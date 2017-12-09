using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class EarliestEndOptimization : ScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the earliest ending time.
        /// </summary>
        /// <returns>The schedule with earliest ending time.</returns>
        /// <param name="possibleSchedules">Possible schedules without time conflicts.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            List<Section> result = possibleSchedules[0];
            DateTime? earliestEnd = new DateTime(1, 1, 1, 23, 59, 0);

            // Get the latest time for a schedule
            foreach (List<Section> schedule in possibleSchedules)
            {
                DateTime? lastestScheduleTime = new DateTime(1, 1, 1, 0, 0, 0);

                foreach (Section section in schedule)
                {
                    DateTime? latestCourseTime = new DateTime(1, 1, 1, 0, 0, 0);
                    foreach (Meeting meeting in section.Meetings)
                    {
                        if (meeting.MeetingType != MeetingType.Review &&
                            meeting.MeetingType != MeetingType.Final &&
                            meeting.MeetingType != MeetingType.Midterm)
                        {
                            // Get the latest end time for a course
                            if (meeting.EndTime > latestCourseTime)
                            {
                                latestCourseTime = meeting.EndTime;
                            }
                        }
                    }

                    // Get the latest end time for the schedule
                    if (latestCourseTime > lastestScheduleTime)
                    {
                        lastestScheduleTime = latestCourseTime;
                    }
                }

                // Get scheduele with the earliest schedule
                if (earliestEnd > lastestScheduleTime)
                {
                    earliestEnd = lastestScheduleTime;
                    result = schedule;
                }
            }
            return result;
        }
    }
}
