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
        /// <param name="currSchedule">First node to check conflicts for</param>
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
    }
}