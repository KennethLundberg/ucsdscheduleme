using System;
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
        public int numEdges { get; set; }
        public int currEdges { get; set; }
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

            //         List<Node> AllSections = new List<Node>();
            List<List<Section>> possibleSchedules = new List<List<Section>>();

            if (coursesToSchedule.Count() == 1)
            {
                possibleSchedules.Add((System.Collections.Generic.List<ucsdscheduleme.Models.Section>)coursesToSchedule[0].Sections);
                return possibleSchedules;
            }

            Course firstCourse = coursesToSchedule[0];
            int numClasses = coursesToSchedule.Length;

            // Create a node for each meeting.
            /*            int numNodes = 0;
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
                        } */

            // Create a node for each meeting.
            int numNodes = 0;
            List<List<Node>> AllSections = new List<List<Node>>();

            foreach (Course i in coursesToSchedule) 
            {
                List<Node> thisCourse = new List<Node>();
                //) (int i = 0; i < coursesToSchedule.Count(); i++)   
                foreach (Section j in i.Sections)
                {
                    Node thisSection = new Node(numNodes++)
                    {
                        Course = i,
                        Section = j,
                        numEdges = 0,
                        currEdges = 0
                    };
                    //Console.WriteLine(thisSection.Course.CourseAbbreviation);
                    thisCourse.Add(thisSection);
                }
                AllSections.Add(thisCourse);
            }


  //          Console.WriteLine("number of nodes:" + numNodes);
            // Create all the edges between meetings
            /*            for (int i = 0; i < AllSections.Count(); i++)
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
                        */
            // Create all the edges between meetings
            for (int i = 0; i < coursesToSchedule.Count() - 1; i++)
            {
//                Console.WriteLine("i" + i);
  //              Console.WriteLine("Num of Courses: " + AllSections.Count());
                foreach (Node j in AllSections[i])
                {
//                    Console.WriteLine("j: " + j.Course.CourseAbbreviation);
                    foreach (Node k in AllSections[i + 1])
                    {
                        j.Edges.Add(k);
                        j.numEdges++;
                        j.currEdges++;
                    }
                }
            }
/*            Console.WriteLine("GRAPH CHECK: ");
            foreach (Node i in AllSections[0]) {
                Console.WriteLine("COURSE: " + i.Course.CourseAbbreviation);   
                Console.WriteLine("NUM EDGES: " + i.Edges.Count()); 
            }
            foreach (Node i in AllSections[1])
            {
                Console.WriteLine("COURSE: " + i.Course.CourseAbbreviation);
                Console.WriteLine("NUM EDGES: " + i.Edges.Count());
                Console.WriteLine(i.Edges.ElementAt(0).Course.CourseAbbreviation);
            } */
            // dfs on graph to find possible schedules
            /*           foreach (Node section in AllSections)
                       {
                           if (section.IsFirstClass)
                           {
                               DFS(section, numClasses, numNodes, ref possibleSchedules);
                               Console.WriteLine("SIZE OF POSS:" + possibleSchedules.Count());
                           }
                       }
                       return possibleSchedules;
                   } */

            foreach (Node i in AllSections[0])
            { 
                List<Section> schedule = new List<Section>();
                DFS(i, numClasses, ref schedule, ref possibleSchedules);
                Console.WriteLine("NUM OF SCH AFTER DFS CALL: " + possibleSchedules.Count());
                //for (int j = 0; j < AllSections.Count; j++)
                //{
                //    List<Node> course = AllSections[j];
                //    for (int k = 0; k < course.Count(); k++) 
                //    {
                //        AllSections[j][k].currEdges = AllSections[j][k].numEdges;
                //    }
                //} 
            }
            return possibleSchedules;
        }

        /// <summary>
        /// Performs DFS on schedule graph.
        /// </summary>
        /// <param name="root">Node to do DFS from</param>
        /// <param name="numClasses">Total number of classes to schedule</param>
        /// <param name="possibleSchedules">Modified list of possible schedules</param>
        private void DFS(Node section, int numClasses, ref List<Section> currSchedule, ref List<List<Section>> possibleSchedules)
        {
            Console.WriteLine("DFSCALL(" + section.Index + ") " + section.Course.CourseAbbreviation);
            currSchedule.Add(section.Section);

            //Console.WriteLine("curredges for " + section.Index + " was " + section.currEdges);
            section.currEdges--;
            //Console.WriteLine("curredges for " + section.Index + " is now " + section.currEdges);
            Console.WriteLine("curredges for " + section.Index + " is " + section.currEdges + " edges");
            foreach (Section z in currSchedule) {
                Console.WriteLine(z.Course.CourseAbbreviation);
            }

            if (section.Edges.Count() == 0) 
            {
                if (currSchedule.Count() == numClasses) {
                    //  if (!Conflict(currSchedule))
                     {
                        List<Section> validSchedule = new List<Section>();
                        foreach(Section k in currSchedule) {
                            validSchedule.Add(k);
                        }
                        possibleSchedules.Add(validSchedule);
                    }
                }
                currSchedule.Remove(currSchedule.Last());
 
                return;
            }

            foreach (Node i in section.Edges) 
            {
                //Console.WriteLine("curredges for " + i.Index + " was " + i.currEdges);
                //i.currEdges--;
                //Console.WriteLine("curredges for " + i.Index + " is now " + i.currEdges);
                DFS(i, numClasses, ref currSchedule, ref possibleSchedules);
                i.currEdges--;

                if (i.currEdges == 0)
                {
                    Console.WriteLine("last = " + currSchedule.Last().Course.CourseAbbreviation);
                    currSchedule.Remove(currSchedule.Last());
                    i.currEdges = i.Edges.Count();
                }
                if (i.currEdges < 0)
                {
                    i.currEdges = i.Edges.Count();
                }
                //section.Edges.Remove(i);
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
                        if ((i.StartTime <= j.EndTime) && (i.EndTime >= j.StartTime))
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
        private bool Conflict(List<Section> currSchedule)
        {
            for (int i = 0; i < currSchedule.Count() - 1; i++) 
            {
                if (Conflict(currSchedule.ElementAt(i), currSchedule.ElementAt(i+1)))
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