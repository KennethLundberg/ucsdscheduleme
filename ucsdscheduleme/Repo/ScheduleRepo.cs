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
        public Meeting Meeting { get; set; }

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
        public List<List<Meeting>> FindScheduleForClasses(Course[] coursesToSchedule)
        {
            List<Node> AllMeetings = new List<Node>();
            List<List<Meeting>> possibleSchedules = new List<List<Meeting>>();

            Course firstCourse = coursesToSchedule[0];
            int numClasses = coursesToSchedule.Length;

            // Create a node for each meeting.
            int numNodes = 0;
            foreach (Course course in coursesToSchedule)
            {
                foreach (Section section in course.Sections)
                {
                    foreach (Meeting meeting in section.Meetings)
                    {
                        Node thisMeeting = new Node(numNodes++)
                        {
                            Course = course,
                            Meeting = meeting
                        };
                        AllMeetings.Add(thisMeeting);
                        thisMeeting.IsFirstClass = thisMeeting.Course == firstCourse;
                    }
                }
            }

            // Create all the edges between meetings
            for (int i = 0; i < AllMeetings.Count(); i++)
            {
                for (int j = i + 1; j < AllMeetings.Count(); j++)
                {
                    Node node1 = AllMeetings[i];
                    Node node2 = AllMeetings[j];
                    if (node1.Course != node2.Course)
                    {
                        if (!Conflict(node1.Meeting, node2.Meeting))
                        {
                            node1.Edges.Add(node2);
                        }
                    }
                }
            }

            // dfs on graph to find possible schedules
            foreach (Node meeting in AllMeetings)
            {
                if (meeting.IsFirstClass)
                {
                    DFS(meeting, numClasses, numNodes, ref possibleSchedules);
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
        private void DFS(Node root, int numClasses, int numNodes, ref List<List<Meeting>> possibleSchedules)
        {
            Node curr;
            Stack<Node> stack = new Stack<Node>();
            bool[] isNodeVisited = new bool[numNodes];
            List<Meeting> temp = new List<Meeting>();
            temp.Add(root.Meeting);

            stack.Push(root);
            isNodeVisited[0] = true;

            while (stack.Count != 0)
            {
                curr = stack.Peek();
                stack.Pop();
                temp.Remove(curr.Meeting);

                foreach (Node i in curr.Edges)
                {
                    if (!isNodeVisited[i.Index])
                    {
                        stack.Push(i);
                        temp.Add(i.Meeting);
                        isNodeVisited[i.Index] = true;

                        if (temp.Count() == numClasses)
                        {
                            List<Meeting> possibleSchedule = new List<Meeting>();
                            foreach (Meeting j in temp)
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
        /// Checks for time conflicts.
        /// </summary>
        /// <param name="meeting1">First meeting to check conflicts for</param>
        /// <param name="meeting2"></param>
        /// <returns>True if conflict is found.</returns>
        private bool Conflict(Meeting meeting1, Meeting meeting2)
        {
            return (meeting1.EndTime > meeting2.StartTime) || (meeting1.StartTime > meeting2.EndTime);
        }
    }
}