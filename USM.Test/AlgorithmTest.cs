using System.Collections.Generic;
using Xunit;
using ucsdscheduleme.Repo;
using ucsdscheduleme.Models;
using System.Diagnostics;
using System.Linq;

namespace USM.Test
{
    public class TestsAlgorithm
    {
        public Course[] Classes()
        {
            ScheduleRepo sch = new ScheduleRepo();

            Meeting CSE20Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 14, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 15, 20, 0),
                Code = "A00"
            };

            /*Meeting CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1000,
                EndTime = 1050,
                Code = "A02"
            };
            Meeting CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1711,
                EndTime = 1712,
                Code = "B02"
            };*/
            Course CSE20 = new Course()
            {
                CourseAbbreviation = "CSE20",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE20Section1 = new Section()
            {
                Ticket = 201,
                Course = CSE20
            };
            /*Section CSE20Section2 = new Section()
            {
                Ticket = 202,
                Course = CSE20
            };*/

            //a lecture that is the same time with cse 20
            Meeting CSE21Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 14, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 15, 20, 0),
                Code = "B00"
            };
            Course CSE21 = new Course()
            {
                CourseAbbreviation = "CSE21",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE21Section1 = new Section()
            {
                Ticket = 211,
                Course = CSE21
            };

            //a lecture that is the different time same day with cse 20
            Meeting CSE22Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 15, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 16, 20, 0),
                Code = "B00"
            };
            Course CSE22 = new Course()
            {
                CourseAbbreviation = "CSE22",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE22Section1 = new Section()
            {
                Ticket = 221,
                Course = CSE22
            };

            //a lecture that is the different time same day with cse 23
            Meeting CSE23Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 0, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 1, 20, 0),
                Code = "C00"
            };
            Course CSE23 = new Course()
            {
                CourseAbbreviation = "CSE23",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE23Section1 = new Section()
            {
                Ticket = 231,
                Course = CSE23
            };

            //a lecture that is the different time dif day with cse 20
            Meeting CSE24Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Monday | Days.Wednesday | Days.Friday),
                StartTime = new System.DateTime(1, 1, 1, 10, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 10, 50, 0),
                Code = "F00"
            };
            Course CSE24 = new Course()
            {
                CourseAbbreviation = "CSE24",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE24Section1 = new Section()
            {
                Ticket = 241,
                Course = CSE24
            };

            //a lecture that is the same time dif day with cse 20
            Meeting CSE25Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Monday),
                StartTime = new System.DateTime(1, 1, 1, 15, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 16, 20, 0),
                Code = "D00"
            };
            Course CSE25 = new Course()
            {
                CourseAbbreviation = "CSE25",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE25Section1 = new Section()
            {
                Ticket = 251,
                Course = CSE25
            };

            Meeting CSE26Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 15, 30, 0),
                EndTime = new System.DateTime(1, 1, 1, 17, 00, 0),
                Code = "A00"
            };

            Meeting CSE26Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new System.DateTime(1, 1, 1, 10, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 10, 50, 0),
                Code = "A02"
            };
            Meeting CSE26Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 17, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 17, 50, 0),
                Code = "B02"
            };
            Course CSE26 = new Course()
            {
                CourseAbbreviation = "CSE26",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE26Section1 = new Section()
            {
                Ticket = 261,
                Course = CSE26
            };
            Section CSE26Section2 = new Section()
            {
                Ticket = 262,
                Course = CSE26
            };

            Meeting CSE27Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Monday | Days.Wednesday),
                StartTime = new System.DateTime(1, 1, 1, 15, 30, 0),
                EndTime = new System.DateTime(1, 1, 1, 17, 00, 0),
                Code = "A00"
            };

            Meeting CSE27Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new System.DateTime(1, 1, 1, 10, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 10, 50, 0),
                Code = "A02"
            };
            Meeting CSE27Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new System.DateTime(1, 1, 1, 15, 0, 0),
                EndTime = new System.DateTime(1, 1, 1, 17, 50, 0),
                Code = "B02"
            };
            Course CSE27 = new Course()
            {
                CourseAbbreviation = "CSE27",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };
            Section CSE27Section1 = new Section()
            {
                Ticket = 271,
                Course = CSE27
            };
            Section CSE27Section2 = new Section()
            {
                Ticket = 272,
                Course = CSE27
            };

            // Adding different sections to the course object
            //adding for cse 20
            CSE20.Sections.Add(CSE20Section1);
            CSE20Section1.Meetings.Add(CSE20Lecture);
            //adding for cse 21
            CSE21.Sections.Add(CSE21Section1);
            CSE21Section1.Meetings.Add(CSE21Lecture);
            //adding for cse 22
            CSE22.Sections.Add(CSE22Section1);
            CSE22Section1.Meetings.Add(CSE22Lecture);
            //adding for cse 23
            CSE23.Sections.Add(CSE23Section1);
            CSE23Section1.Meetings.Add(CSE23Lecture);
            //adding for cse 24
            CSE24.Sections.Add(CSE24Section1);
            CSE24Section1.Meetings.Add(CSE24Lecture);
            //adding for cse 25
            CSE25.Sections.Add(CSE25Section1);
            CSE25Section1.Meetings.Add(CSE25Lecture);
            //adding for cse 26
            CSE26.Sections.Add(CSE26Section1);
            CSE26Section1.Meetings.Add(CSE26Lecture);
            CSE26Section1.Meetings.Add(CSE26Discussion1);
            CSE26Section2.Meetings.Add(CSE26Lecture);
            CSE26Section2.Meetings.Add(CSE26Discussion2);
            CSE26.Sections.Add(CSE26Section2);
            //adding for cse 27
            CSE27.Sections.Add(CSE27Section1);
            CSE27Section1.Meetings.Add(CSE27Lecture);
            CSE27Section1.Meetings.Add(CSE27Discussion1);
            CSE27Section2.Meetings.Add(CSE27Lecture);
            CSE27Section2.Meetings.Add(CSE27Discussion2);
            CSE27.Sections.Add(CSE27Section2);

            Course[] courses = { CSE20, CSE21, CSE22, CSE23, CSE24, CSE25, CSE26, CSE27 };
            return courses;
        }

        //desperate single test
        [Fact]
        public void TestSingleClass()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            //Course CSE21 = AllCourses[1];
            Course[] courses = { CSE20 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 1;
            int actualNum = returned.Count;

            Assert.Equal(expectedNum, actualNum);
        }

        /* conflict on the same day and same time */
        [Fact]
        public void TestConflictSameDay()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            Course CSE21 = AllCourses[1];
            Course[] courses = { CSE20, CSE21 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 0;
            int actualNum = returned.Count;

            //no available schedules
            Assert.Equal(expectedNum, actualNum);
        }

        //conflict on same day different time
        [Fact]
        public void TestConflictSameDayDifTime()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            Course CSE22 = AllCourses[2];
            Course[] courses = { CSE20, CSE22 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 0;
            int actualNum = returned.Count;

            //no available schedules
            Assert.Equal(expectedNum, actualNum);
        }

        //no conflict on same day different time
        [Fact]
        public void TestNoConflictSameDayDifTime()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            Course CSE23 = AllCourses[3];
            Course[] courses = { CSE20, CSE23 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 1;
            int actualNum = returned.Count;

            //two schedules
            Assert.Equal(expectedNum, actualNum);
        }

        //no conflict on dif day different time
        [Fact]
        public void TestNoConflictDifDaySameTime()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            Course CSE24 = AllCourses[4];
            Course[] courses = { CSE20, CSE24 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 1;
            int actualNum = returned.Count;

            //two schedules
            Assert.Equal(expectedNum, actualNum);
        }

        //no conflict on dif day same time
        [Fact]
        public void TestNoConflictDifDayDifTime()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            Course CSE25 = AllCourses[5];
            Course[] courses = { CSE20, CSE25 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 1;
            int actualNum = returned.Count;

            //two schedules
            Assert.Equal(expectedNum, actualNum);
        }

        // no conflict with two discussions in one class
        [Fact]
        public void TestNoConflictWithDis1()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses.First(c => c.CourseAbbreviation == "CSE20");
            Course CSE26 = AllCourses.First(c => c.CourseAbbreviation == "CSE26");
            Course[] courses = { CSE20, CSE26 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 2;
            int actualNum = returned.Count;

            //two schedules
            List<Section> ls0 = returned[0];
            Section dis1 = ls0[1];
            List<Section> ls1 = returned[1];
            Section dis2 = ls1[1];
            Debug.WriteLine("first dis is "+dis1.Course.CourseAbbreviation);
            Debug.WriteLine("second dis is " + dis2.Course.CourseAbbreviation);


            
            Assert.Equal(expectedNum, actualNum);
        }

        //one discussion pass but one discussion conflicts
        [Fact]
        public void TestNoConflictWithDis2()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE24 = AllCourses[4];
            Course CSE26 = AllCourses[6];
            Course[] courses = { CSE24, CSE26 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 1;
            int actualNum = returned.Count;

            //two schedules
            Assert.Equal(expectedNum, actualNum);
            //test whether the right list 
            List<List<Section>> possibleSchedule = returned;
            List<Section> rightSchedule = new List<Section>();
            rightSchedule.Add(CSE24.Sections.ElementAt(0));
            rightSchedule.Add(CSE26.Sections.ElementAt(1));
            Assert.Contains(rightSchedule, returned);
        }

        // one confliction between discussion and discussion
        // another confliction between discussion and lecture
        // no possible schedule
        [Fact]
        public void TestNoConflictWithDis3()
        {
            //use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE26 = AllCourses[6];
            Course CSE27 = AllCourses[7];
            Course[] courses = { CSE26, CSE27 };

            //call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            int expectedNum = 1;
            int actualNum = returned.Count;

            //two schedules
            Assert.Equal(expectedNum, actualNum);
        }

        // test least day optimization, no possible schedule
/*        [Fact]
        public void TestLeastDayWithDis0()
        {
            // use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE26 = AllCourses[6];
            Course CSE27 = AllCourses[7];
            Course[] courses = { CSE26, CSE27 };

            // call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            List<Section> optimized = sch.LeastDays(returned);

            // should be null
            Assert.Equal(optimized, null);
        } */

        // test least day optimization, one with 2 days, the other with 3
/*        [Fact]
        public void TestLeastDayWithDis1()
        {
            // use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE26 = AllCourses[6];
            Course[] courses = { CSE26 };

            // call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            List<Section> optimized = sch.LeastDays(returned);
            int expectedNum = 2;
            int actualNum = sch.DaysCalculations(optimized);

            // two days: tuesday, thursday
            Assert.Equal(expectedNum, actualNum);
        }

        // test least day optimization, only has one schedule with 3 days
        [Fact]
        public void TestLeastDayWthDis2()
        {
            // use the course above
            ScheduleRepo sch = new ScheduleRepo();
            Course[] AllCourses = Classes();

            // choose the courses to be tested
            Course CSE20 = AllCourses[0];
            Course CSE25 = AllCourses[5];
            Course[] courses = { CSE20, CSE25 };

            // call the algorithm
            List<List<Section>> returned = sch.FindScheduleForClasses(courses);
            List<Section> optimized = sch.LeastDays(returned);
            int expectedNum = 3;
            int actualNum = sch.DaysCalculations(optimized);

            // three days: monday, tuesday, thursday
            Assert.Equal(expectedNum, actualNum);
        } */
    }
}