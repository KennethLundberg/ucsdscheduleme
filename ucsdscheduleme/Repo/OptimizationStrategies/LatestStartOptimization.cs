using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public class LatestStartOptimization : ScheduleOptimization
    {
        /// <summary>
        /// Returns the schedule with the latest start time.
        /// </summary>
        /// <returns>The schedule with the latest start time.</returns>
        /// <param name="possibleSchedules">Possible schedules.</param>
        public override List<Section> Optimize(List<List<Section>> possibleSchedules)
        {
            List<Section> result = possibleSchedules[0];
            DateTime? latestStart = new DateTime(1, 1, 1, 0, 0, 0);

            // Get the lastest time for a schedule
            foreach (List<Section> schedule in possibleSchedules)
            {
                DateTime? earliestScheduleTime = new DateTime(1, 1, 1, 23, 59, 0);

                foreach (Section section in schedule)
                {
                    DateTime? earliestCourseTime = new DateTime(1, 1, 1, 23, 59, 0);

                    foreach (Meeting meeting in section.Meetings)
                    {
                        if (meeting.MeetingType != MeetingType.Review &&
                            meeting.MeetingType != MeetingType.Final &&
                            meeting.MeetingType != MeetingType.Midterm)
                        {
                            // Get earliest start time for course
                            if (meeting.StartTime < earliestCourseTime)
                            {
                                earliestCourseTime = meeting.StartTime;
                            }
                        }
                    }

                    // Get the earliest start time for schedule
                    if (earliestCourseTime < earliestScheduleTime)
                    {
                        earliestScheduleTime = earliestCourseTime;
                    }
                }

                // Compare
                if (latestStart < earliestScheduleTime)
                {
                    latestStart = earliestScheduleTime;
                    result = schedule;
                }
            }
            return result;
        }
    }
}
