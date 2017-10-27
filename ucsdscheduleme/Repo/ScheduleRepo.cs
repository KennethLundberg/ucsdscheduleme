using System.Collections.Generic;
using System.Linq;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo
{

    // The class for each node of the graph.
    public class Node
    {
        public Node(int index)
        {
            Index = index;
        }

        public Course Course { get; set; }
        public Section Section { get; set; }

        public readonly int Index;
        public bool IsFirstClass { get; set; }

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
            List<Node> AllSections = new List<Node>();
            List<List<Section>> possibleSchedules = new List<List<Section>>();

            Course firstCourse = coursesToSchedule[0];
            int numClasses = coursesToSchedule.Length;

            // Create a node for each meeting.
            int numNodes = 0;
            foreach (Course course in coursesToSchedule)
            {
                foreach (Section section in course.Sections)
                {
                    Node thisSection = new Node(numNodes++)
                    {
                        Course = course,
                        Section = section
                    };
                    AllSections.Add(thisSection);
                    thisSection.IsFirstClass = thisSection.Course == firstCourse;
                }
            }

            // Create all the edges between meetings
            for (int i = 0; i < AllSections.Count(); i++)
            {
                for (int j = i + 1; j < AllSections.Count(); j++)
                {
                    Node node1 = AllSections[i];
                    Node node2 = AllSections[j];
                    if (node1.Course != node2.Course)
                    {
                        if (!Conflict(node1.Section, node2.Section))
                        {
                            node1.Edges.Add(node2);
                        }
                    }
                }
            }

            // dfs on graph to find possible schedules
            foreach (Node section in AllSections)
            {
                if (section.IsFirstClass)
                {
                    DFS(section, numClasses, numNodes, ref possibleSchedules);
                }
            }

            return possibleSchedules;
        }

        /// <summary>
        /// Performs DFS on schedule graph.
        /// </summary>
        /// <param name="root">Node to do DFS from</param>
        /// <param name="numClasses">Total number of classes to schedule</param>
        /// <param name="possibleSchedules">Modified list of possible schedules</param>
        private void DFS(Node root, int numClasses, int numNodes, ref List<List<Section>> possibleSchedules)
        {
            Node curr;
            Stack<Node> stack = new Stack<Node>();
            bool[] isNodeVisited = new bool[numNodes];
            List<Section> temp = new List<Section>
            {
                root.Section
            };
            stack.Push(root);
            isNodeVisited[0] = true;

            while (stack.Count != 0)
            {
                curr = stack.Peek();
                stack.Pop();
                temp.Remove(curr.Section);

                foreach (Node i in curr.Edges)
                {
                    if (!isNodeVisited[i.Index] && !Conflict(i.Section, temp))
                    {
                        stack.Push(i);
                        temp.Add(i.Section);
                        isNodeVisited[i.Index] = true;

                        if (temp.Count() == numClasses)
                        {
                            List<Section> possibleSchedule = new List<Section>();
                            foreach (Section j in temp)
                            {
                                possibleSchedule.Add(j);
                            }
                            possibleSchedules.Add(possibleSchedule);
                        }
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
            foreach (Meeting i in section1.Meetings)
            {
                foreach (Meeting j in section2.Meetings)
                {
                    if ((i.Days & j.Days) != 0)
                    {

                        if (i.EndTime <= j.StartTime || i.StartTime <= j.EndTime)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks for time conflicts between a node and the existing schedule.
        /// </summary>
        /// <returns>The conflict.</returns>
        /// <param name="section1">First node to check conflicts for</param>
        /// <param name="temp">Current existing schedule</param>
        private bool Conflict(Section section1, List<Section> temp)
        {
            foreach (Section i in temp)
            {
                if (Conflict(section1, i))
                {
                    return false;
                }
            }
            return true;
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