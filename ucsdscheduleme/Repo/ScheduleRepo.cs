using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ucsdscheduleme.Models;
using PossibleSchedules = System.Collections.Generic.List<System.Collections.Generic.List<ucsdscheduleme.Models.Section>>;

namespace ucsdscheduleme.Repo
{

    // The class for each node of the graph.
    public class Node
    {
        public Course Course { get; set; }
        public Section Section { get; set; }

        public int Index { get; set; }
        public int Layer { get; set; }
        public List<Node> Edges { get; set; } = new List<Node>();
    }

    public class ScheduleRepo
    {
        /// <summary>
        /// Finds all possible shedules for a list of courses.
        /// </summary>
        /// <param name="coursesToSchedule">Which courses to schedule.</param>
        /// <returns>List of possible schedules for course list.</returns>
        public List<List<Section>> FindScheduleForClasses(Course[] coursesToSchedule)
        {
            List<List<Section>> possibleSchedules = new List<List<Section>>();

            Course firstCourse = coursesToSchedule[0];
            int numClasses = coursesToSchedule.Length;

            // Create a node for each Section.
            List<List<Node>> AllSections = new List<List<Node>>();

            for (int i = 0; i < coursesToSchedule.Count(); i++)
            {
                List<Node> thisCourse = new List<Node>();
                foreach (Section j in coursesToSchedule[i].Sections)
                {
                    Node thisSection = new Node()
                    {
                        Course = coursesToSchedule[i],
                        Section = j,
                        Layer = i
                    };

                    thisCourse.Add(thisSection);
                }
                AllSections.Add(thisCourse);
            }

            // Create all the edges between meetings.
            for (int i = 0; i < coursesToSchedule.Count() - 1; i++)
            {
                foreach (Node j in AllSections[i])
                {
                    foreach (Node k in AllSections[i + 1])
                    {
                        j.Edges.Add(k);
                    }
                }
            }

            // Call DFS on each subgraph, starting with the first class sections
            // being the root
            foreach (Node node in AllSections[0])
            {
                DFS(node, numClasses, ref possibleSchedules);
            }
            return possibleSchedules;
        }

        /// <summary>
        /// Performs DFS on schedule graph. Begins with pushing the root onto the stack. Then
        /// continuously pops and adds the popped section onto the schedule. When a node
        /// is popped, it will push all its children onto the stack. Once a node from the last
        /// layer is popped, then this is a unique schedule. This schedule is checked
        /// for conflicts and then added if there are no time conflicts.
        /// </summary>
        /// <param name="section">Node to do DFS from</param>
        /// <param name="numClasses">Total number of classes to schedule</param>
        /// <param name="possibleSchedules">Modified list of possible schedules</param>
        private void DFS(Node section, int numClasses, ref List<List<Section>> possibleSchedules)
        {
            // start stack with the root
            Stack<Node> s = new Stack<Node>();
            s.Push(section);

            Section[] tempSchedule = new Section[numClasses];

            // Adds the first class in the correct order on the temp schedule, then push children.
            while (s.Count != 0)
            {
                Node curr = s.Pop();
                tempSchedule[curr.Layer] = curr.Section;

                // The popped node was part of the last layer.
                if (curr.Layer == (numClasses - 1))
                {
                    // If there are no conflicts, copy into list and add to possible.
                    if (!Conflict(tempSchedule))
                    {
                        List<Section> currSchedule = new List<Section>();

                        for (int i = 0; i < tempSchedule.Count(); i++)
                        {
                            currSchedule.Add(tempSchedule[i]);
                        }
                        possibleSchedules.Add(currSchedule);
                    }
                }

                // Not the last layer of the tree, push children onto stack.
                else
                {
                    foreach (Node i in curr.Edges)
                    {
                        s.Push(i);
                    }
                }
            }
        }

        /// <summary>
        /// Checks for time conflicts between two nodes.
        /// </summary>
        /// <param name="section1">First section to check conflicts for</param>
        /// <param name="section2">Second section to check conflicts for</param>
        /// <returns>True if conflict is found.</returns>
        private bool Conflict(Section section1, Section section2)
        {
            foreach (Meeting meeting1 in section1.Meetings)
            {
                foreach (Meeting meeting2 in section2.Meetings)
                {
                    if ((meeting1.Days & meeting2.Days) != 0)
                    {
                        // Want to compare recurring events like lecture, discussion, labs
                        if (!IsOneTimeEvent(meeting1.MeetingType)
                            && (!IsOneTimeEvent(meeting2.MeetingType)))
                        {
                            if (Conflict(meeting1, meeting2))
                            {
                                return true;
                            }
                        }
                        else if (IsOneTimeEvent(meeting1.MeetingType) && IsOneTimeEvent(meeting2.MeetingType))
                        {
                            if (meeting1.StartDate == meeting2.StartDate)
                            {
                                if (Conflict(meeting1, meeting2))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool Conflict(Meeting meeting1, Meeting meeting2)
        {
            if (((TimeSpan.Compare(meeting1.StartTime.TimeOfDay, meeting2.EndTime.TimeOfDay) == 0)
                 || (TimeSpan.Compare(meeting1.StartTime.TimeOfDay, meeting2.EndTime.TimeOfDay) == -1))
            && ((TimeSpan.Compare(meeting1.EndTime.TimeOfDay, meeting2.StartTime.TimeOfDay) == 0)
                || (TimeSpan.Compare(meeting1.EndTime.TimeOfDay, meeting2.StartTime.TimeOfDay) == 1)))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks for time conflicts between a node and the existing schedule.
        /// </summary>
        /// <returns>True if there is a conflict, else false.</returns>
        /// <param name="tempSchedule">Schedule to check conflicts for</param>
        private bool Conflict(Section[] tempSchedule)
        {
            for (int i = 0; i < tempSchedule.Count() - 1; i++)
            {
                if (Conflict(tempSchedule[i], tempSchedule[i + 1]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tests whether or not a meeting is a one time event or not.
        /// </summary>
        /// <param name="type">The meeting type of the meeting to check.</param>
        /// <returns>True if meeting is a one time event, false otherwise.</returns>
        private static bool IsOneTimeEvent(MeetingType type)
        {
            return type == MeetingType.Final || type == MeetingType.Review || type == MeetingType.Midterm;
        }

        /// <summary>
        /// Finds possible schedules given time constraints, earliest/latest times.
        /// </summary>
        /// <returns>The schedule for classes with time constraints.</returns>
        /// <param name="possibleSchedules">Courses to schedule.</param>
        /// <param name="Earliest">Earliest time desired.</param>
        /// <param name="Latest">Latest time desired.</param>
        private List<List<Section>> FindWithTime(List<List<Section>> possibleSchedules, DateTime Earliest, DateTime Latest)
        {
            List<List<Section>> constraintSchedules = new List<List<Section>>();
            foreach (List<Section> schedule in possibleSchedules)
            {
                if (IsWithinTime(schedule, Earliest, Latest))
                {
                    constraintSchedules.Add(schedule);
                }
            }
            return constraintSchedules;
        }

        /// <summary>
        /// Checks if a single schedule is within the time constraints.
        /// </summary>
        /// <returns><c>true</c>, if within time was ised, <c>false</c> otherwise.</returns>
        /// <param name="schedule">Schedule to check.</param>
        /// <param name="Earliest">Earliest time desired.</param>
        /// <param name="Latest">Latest time desired.</param>
        private bool IsWithinTime(List<Section> schedule, DateTime Earliest, DateTime Latest)
        {
            foreach (Section section in schedule)
            {
                foreach (Meeting meeting in section.Meetings)
                {
                    if (meeting.StartTime < Earliest)
                    {
                        return false;
                    }
                    if (meeting.EndTime > Latest)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the schedule with the highest received GPAs from CAPEs.
        /// </summary>
        /// <returns>The schedule with highest CAPEs GPA</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public List<Section> HighestGPA(List<List<Section>> possibleSchedules)
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

                    if (cape != null)
                    {
                        currentGPA += cape.AverageGradeReceived;
                    }
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

        /// <summary>
        /// Returns the schedule with the highest overall quality from RateMyProfessor.
        /// </summary>
        /// <returns>The schedule with the hightest overall quality from RateMyProfessor.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public List<Section> RMPRating(List<List<Section>> possibleSchedules)
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

        /// <summary>
        /// Returns the schedule with the earliest ending time.
        /// </summary>
        /// <returns>The schedule with earliest ending time.</returns>
        /// <param name="possibleSchedules">Possible schedules without time conflicts.</param>
        public List<Section> EarliestEnd(List<List<Section>> possibleSchedules)
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

        /// <summary>
        /// Returns the schedule with the latest start time.
        /// </summary>
        /// <returns>The schedule with the latest start time.</returns>
        /// <param name="possibleSchedules">Possible schedules.</param>
        public List<Section> LatestStart(List<List<Section>> possibleSchedules)
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
                                earliestCourseTime = meeting.EndTime;
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

        /// <summary>
        /// Returns the schedule with the least days scheduled.
        /// </summary>
        /// <returns>The schedule with the least days.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public List<Section> LeastDays(List<List<Section>> possibleSchedules)
        {
            int leastDay = 5;
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

        /// <summary>
        /// Returns the schedule with the most days scheduled.
        /// </summary>
        /// <returns>The schedule with the most days.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public List<Section> MostDays(List<List<Section>> possibleSchedules)
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

        /// <summary>
        /// Calculates the number of days the schedule has classes on
        /// </summary>
        /// <param name="schedule">A schedule of classes</param>
        /// <returns> the number of days in the schedule</returns>
        private int NumDays(List<Section> schedule)
        {
            int numDays = 0;
            int[] currDays = { 0, 0, 0, 0, 0 };
            foreach (Section section in schedule)
            {
                foreach (Meeting meeting in section.Meetings)
                {
                    // Add to day array
                    if (meeting.MeetingType != MeetingType.Final || meeting.MeetingType != MeetingType.Midterm
                        || meeting.MeetingType != MeetingType.Review)
                    {
                        if (meeting.Days.HasFlag(Days.Monday))
                        {
                            currDays[0]++;
                        }
                        if (meeting.Days.HasFlag(Days.Tuesday))
                        {
                            currDays[1]++;
                        }
                        if (meeting.Days.HasFlag(Days.Wednesday))
                        {
                            currDays[2]++;
                        }
                        if (meeting.Days.HasFlag(Days.Thursday))
                        {
                            currDays[3]++;
                        }
                        if (meeting.Days.HasFlag(Days.Friday))
                        {
                            currDays[4]++;
                        }
                    }
                }
            }

            // Totals the days scheduled.
            foreach (int day in currDays)
            {
                if (day != 0)
                {
                    numDays++;
                }
            }

            return numDays;
        }

        /// <summary>
        /// Returns the schedule with the least gaps in between classes.
        /// </summary>
        /// <returns>The schedule with the least gaps.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public List<Section> LeastGaps(List<List<Section>> possibleSchedules)
        {
            List<Section> leastGapsSchedule = possibleSchedules[0];
            TimeSpan leastGap = new TimeSpan(99,59,59);

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

        /// <summary>
        /// Returns the schedule with the most gaps in between classes.
        /// </summary>
        /// <returns>The schedule with the most gaps.</returns>
        /// <param name="possibleSchedules">Possible schedules with no time conflicts.</param>
        public List<Section> MostGaps(List<List<Section>> possibleSchedules)
        {
            List<Section> mostGapsSchedule = possibleSchedules[0];
            TimeSpan mostGap = new TimeSpan(0,0,0);

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

        public List<Section> Optimize(Optimization optimization, PossibleSchedules schedules)
        {
            List<Section> schedule;
            switch (optimization)
            {
                case Optimization.HighestGPA:
                    schedule = HighestGPA(schedules);
                    break;
                case Optimization.HighestRMP:
                    schedule = RMPRating(schedules);
                    break;
                case Optimization.EarlyEnd:
                    schedule = EarliestEnd(schedules);
                    break;
                case Optimization.LateStart:
                    schedule = LatestStart(schedules);
                    break;
                case Optimization.MostDays:
                    schedule = MostDays(schedules);
                    break;
                case Optimization.LeastDays:
                    schedule = LeastDays(schedules);
                    break;
                case Optimization.MostGaps:
                    schedule = MostGaps(schedules);
                    break;
                case Optimization.LeastGaps:
                    schedule = LeastGaps(schedules);
                    break;
                default:
                    throw new FormatException();
            }
            return schedule;
        }

        /// <summary>
        /// Calculates the average gap size between sections
        /// </summary>
        /// <param name="schedule">Schedule of classes</param>
        /// <returns>The average hours of gaps in the schedule between classes</returns>
        private TimeSpan TotalGap(List<Section> schedule)
        {
            TimeSpan gap;
            List<DateTime?> Monday = new List<DateTime?>();
            List<DateTime?> Tuesday = new List<DateTime?>();
            List<DateTime?> Wednesday = new List<DateTime?>();
            List<DateTime?> Thursday = new List<DateTime?>();
            List<DateTime?> Friday = new List<DateTime?>();

            foreach (Section section in schedule)
            {
                foreach (Meeting meeting in section.Meetings)
                {
                    if (meeting.MeetingType != MeetingType.Final || 
                        meeting.MeetingType != MeetingType.Midterm ||
                        meeting.MeetingType != MeetingType.Review)
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
            }

            // Sort the class times in each schedule by start time.
            Monday.Sort();
            Tuesday.Sort();
            Wednesday.Sort();
            Thursday.Sort();
            Friday.Sort();
            
            // Calculate the gaps in the schedule.
            for (int i = 1; i < Monday.Count() - 1; i += 2)
            {
                gap = gap + (Monday.ElementAt(i + 1) - Monday.ElementAt(i)).Value;
            }

            for (int i = 1; i < Tuesday.Count() - 1; i += 2)
            {
                gap = gap + (Tuesday.ElementAt(i + 1) - Tuesday.ElementAt(i)).Value;
            }

            for (int i = 1; i < Wednesday.Count() - 1; i += 2)
            {
                gap = gap + (Wednesday.ElementAt(i + 1) - Wednesday.ElementAt(i)).Value;
            }

            for (int i = 1; i < Thursday.Count() - 1; i += 2)
            {
                gap = gap + (Thursday.ElementAt(i + 1) - Thursday.ElementAt(i)).Value;
            }

            for (int i = 1; i < Friday.Count() - 1; i += 2)
            {
                gap = gap + (Friday.ElementAt(i + 1) - Friday.ElementAt(i)).Value;
            }

            return gap;
        }
    }
}