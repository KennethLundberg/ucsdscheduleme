using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo {
    
    /* The class for each node of the graph */
    public class Node {
        public Course course;
        public Meeting meeting;
        public ICollection<Node> Edges { get; set; }
        public bool visited;
        public bool firstClass;

    }
    public class ScheduleRepo {
        public ScheduleRepo() {
            ICollection<Node> GraphNodes = new Collection<Node>();
            List<List<Meeting>> possibleSchedules = new List<List<Meeting>>();

            Course a = new Course();
            Course b = new Course();
            Course c = new Course();
            Course[] possibleCourses = { a, b, c };
            Course firstCourse = possibleCourses.ElementAt(0);
            int numClass = possibleCourses.Count();

            // creating the nodes
            foreach (Course i in possibleCourses) {
                foreach (Section j in i.Sections) {
                    foreach (Meeting k in j.Meetings) {
                        Node nodes = new Node
                        {
                            visited = false,
                            course = i,
                            meeting = k
                        };
                        GraphNodes.Add(nodes);
                        nodes.firstClass  = (nodes.course == firstCourse) ? true : false;
                    }
                }
            }
 
            // creating the edges
            for (int i = 0; i < GraphNodes.Count(); i++) {
                for (int j = i + 1; j < GraphNodes.Count(); j++){
                    Node node1 = GraphNodes.ElementAt(i);
                    Node node2 = GraphNodes.ElementAt(j);
                    if (node1.course != node2.course) {
                        if (!Conflict(node1.meeting, node2.meeting)) {
                            node1.Edges.Add(node2); 
                        }
                    }
                }
            }

            // dfs on graph to find possible schedules
            foreach (Node i in GraphNodes) {
                if (i.firstClass) {
                    DFS(i); 
                }
            }

            /* Checks for time conflicts */
            bool Conflict(Meeting meeting1, Meeting meeting2) {
                if ((meeting1.EndTime > meeting2.StartTime) || (meeting1.StartTime > meeting2.EndTime)) {
                    return true;
                }
                return false;
            }

            /* Depth first search */
            void DFS(Node root) {
                Node curr;
                Stack<Node> stack = new Stack<Node>();
                List <Meeting> temp = new List<Meeting>();
                temp.Add(root.meeting); 

                stack.Push(root);
                root.visited = true;

                while (stack.Count != 0) {
                    curr = stack.Peek();
                    stack.Pop();
                    temp.Remove(curr.meeting);

                    foreach (Node i in curr.Edges) {
                        if (!i.visited) {
                            stack.Push(i);
                            temp.Add(i.meeting);
                            i.visited = true;

                            if (temp.Count() == numClass) {
                                List<Meeting> possibleSchedule = new List<Meeting>();
                                foreach (Meeting j in temp) {
                                    possibleSchedule.Add(j);
                                }
                                possibleSchedules.Add(possibleSchedule);
                            }
                        }
                    }
                }
                foreach (Node i in GraphNodes) {
                    i.visited = false;
                }
            }

        }
    }
}