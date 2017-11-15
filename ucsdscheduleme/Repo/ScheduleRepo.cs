using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ucsdscheduleme.Models;

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
        /// <param name="root">Node to do DFS from</param>
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
                        if ((meeting1.StartTime <= meeting2.EndTime) && (meeting1.EndTime >= meeting2.StartTime))
                        {
                            return true;
                        }
                    }
                }
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
        /// Finds possible schedules given time constraints, earliest/latest times.
        /// </summary>
        /// <returns>The schedule for classes with time constraints.</returns>
        /// <param name="possibleSchedules">Courses to schedule.</param>
        /// <param name="Earliest">Earliest time desired.</param>
        /// <param name="Latest">Latest time desired.</param>
        private List<List<Section>> FindWithTime(List<List<Section>> possibleSchedules, int Earliest, int Latest)
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
        private bool IsWithinTime(List<Section> schedule, int Earliest, int Latest)
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
            float highestGPA = 0;

            foreach (List<Section> schedule in possibleSchedules)
            {
                float scheduleGPA = 0;
                float totalGPA = 0;

                foreach (Section section in schedule)
                {
                    var courseCapes = section.Course.Cape;
                    var professorCapes = section.Professor.Cape;

                    //find the cape for different profs for this course by 
                    //finding a common cape id between courseCape and ProfCape 
                    //var cape = _context.Cape.Single(c => c.courseCapes.Contains(professorCapes));

                    totalGPA = 0;
                    int num = 0;
                    foreach (var cCape in courseCapes)
                    {
                        foreach (var pCape in professorCapes)
                        {
                            if (cCape.Id == pCape.Id)
                            {
                                totalGPA += GetGPA(cCape.AverageGradeReceived);
                                num++;
                            }
                        }
                    }

                    //after find the cape, find average cape for this class
                    totalGPA /= num;
                }
                //total GPA for this schedule
                scheduleGPA += totalGPA;

                //compare GPA of this schedule to the highest schedule GPA
                if (scheduleGPA > highestGPA)
                {
                    highestGPA = scheduleGPA;
                    result = schedule;
                }

            }

            return result;
        }

        //convert the GPA strings in Cape as floats
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capeGrade"></param>
        /// <returns></returns>
        private float GetGPA(string capeGrade)
        {
            string[] tokens = capeGrade.Split('(', ')');
            string num = tokens[1];
            if (num.Contains("."))
                num = num.Replace(".", ",");
            float result = System.Convert.ToSingle(num);
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
            int earliestEnd = 2359;
            //get the lastest time for a schedule
            foreach (List<Section> schedule in possibleSchedules)
            {
                int lastestScheduleTime = 0000;
                int lastestClassTime = 0;

                //get the lastest time for a class
                foreach (Section section in schedule)
                {
                    lastestClassTime = 0000;
                    foreach (Meeting meeting in section.Meetings)
                    {
                        if (meeting.MeetingType != MeetingType.Review &&
                            meeting.MeetingType != MeetingType.Final &&
                            meeting.MeetingType != MeetingType.Midterm)
                        {
                            if (meeting.EndTime > lastestClassTime)
                            {
                                lastestClassTime = meeting.EndTime;
                            }
                        }
                    }
                }
                if (lastestClassTime > lastestScheduleTime)
                {
                    lastestScheduleTime = lastestClassTime;
                }
                //compare
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
            int latestStart = 0000;

            //get the lastest time for a schedule
            foreach (List<Section> schedule in possibleSchedules)
            {
                int earliestScheduleTime = 0000;
                int earliestClassTime = 0000;

                //get the lastest time for a class
                foreach (Section section in schedule)
                {
                    earliestClassTime = 0000;
                    foreach (Meeting meeting in section.Meetings)
                    {
                        if (meeting.MeetingType != MeetingType.Review &&
                            meeting.MeetingType != MeetingType.Final &&
                            meeting.MeetingType != MeetingType.Midterm)
                        {
                            if (meeting.StartTime < earliestClassTime)
                            {
                                earliestClassTime = meeting.EndTime;
                            }
                        }
                    }
                }
                if (earliestClassTime < earliestScheduleTime)
                {
                    earliestScheduleTime = earliestClassTime;
                }
                //compare
                if (latestStart > earliestScheduleTime)
                {
                    latestStart = earliestScheduleTime;
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
            decimal highestRate = 0;
            foreach (List<Section> schedule in possibleSchedules)
            {
                decimal scheduleRate = 0;

                foreach (Section section in schedule)
                {
                    decimal classRate = section.Professor.RateMyProfessor.OverallQuality;
                    scheduleRate += classRate;

                }

                //compare GPA of this schedule to the highest schedule GPA
                if (scheduleRate > highestRate)
                {
                    highestRate = scheduleRate;
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
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private int NumDays(List<Section> schedule)
        {
            int numDays = 0;
            int[] currDays = { 0, 0, 0, 0, 0 };
            foreach (Section section in schedule)
            {
                foreach (Meeting meeting in section.Meetings)
                {
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
            int leastGap = 99999;

            // Iterate through the possible schedules and separate the times into respective days.
            foreach (List<Section> schedule in possibleSchedules)
            {
                int currGap = AvgGap(schedule);

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
            int mostGap = 0;

            // Iterate through the possible schedules and separate the times into respective days.
            foreach (List<Section> schedule in possibleSchedules)
            {
                int currGap = AvgGap(schedule);

                // Checks to see if current schedule has less gaps than current least.
                if (currGap > mostGap) 
                {
                    mostGapsSchedule = schedule;
                    mostGap = currGap;
                }
            }
            return mostGapsSchedule;
        } 

        /// <summary>
        /// Calculates the average gap size between sections
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private int AvgGap(List<Section> schedule)
        {
            int gap = 0;
            List<List<Tuple<int, int>>> week = new List<List<Tuple<int, int>>>();

            foreach (Section section in schedule)
            {
                foreach (Meeting meeting in section.Meetings)
                {
                    if (meeting.MeetingType != MeetingType.Final || 
                        meeting.MeetingType != MeetingType.Midterm ||
                        meeting.MeetingType != MeetingType.Review)
                    {
                        Tuple<int, int> time = new Tuple<int, int>(meeting.StartTime, meeting.EndTime);

                        if (meeting.Days.HasFlag(Days.Monday))
                        {
                            week.ElementAt(0).Add(time);
                        }
                        if (meeting.Days.HasFlag(Days.Tuesday))
                        {
                            week.ElementAt(1).Add(time);
                        }
                        if (meeting.Days.HasFlag(Days.Wednesday))
                        {
                            week.ElementAt(2).Add(time);
                        }
                        if (meeting.Days.HasFlag(Days.Thursday))
                        {
                            week.ElementAt(3).Add(time);
                        }
                        if (meeting.Days.HasFlag(Days.Friday))
                        {
                            week.ElementAt(4).Add(time);
                        }
                    }
                }
            }

            // Sort the class times in each schedule by start time.
            foreach (List<Tuple<int, int>> day in week)
            {
                day.Sort((time1, time2) => time1.Item1.CompareTo(time2.Item2));
            }

            // Calculate the gaps in the schedule.
            foreach (List<Tuple<int, int>> day in week)
            {
                for (int i = 0; i < day.Count() - 1; i++)
                {
                    gap = gap + (day.ElementAt(i + 1).Item2 - day.ElementAt(i).Item1);
                }
            }

            return gap;
        }
    }
}