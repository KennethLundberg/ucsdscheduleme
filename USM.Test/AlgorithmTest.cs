using System.Collections.Generic;
using Xunit;
using ucsdscheduleme.Repo;
using ucsdscheduleme.Models;
using System.Diagnostics;

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
                StartTime = 1400,
                EndTime = 1520,
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
                Units = 4,
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
                StartTime = 1400,
                EndTime = 1520,
                Code = "B00"
            };
            Course CSE21 = new Course()
            {
                CourseAbbreviation = "CSE21",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
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
                StartTime = 1500,
                EndTime = 1620,
                Code = "B00"
            };
            Course CSE22 = new Course()
            {
                CourseAbbreviation = "CSE22",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
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
                StartTime = 0000,
                EndTime = 0001,
                Code = "C00"
            };
            Course CSE23 = new Course()
            {
                CourseAbbreviation = "CSE23",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
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
                StartTime = 0000,
                EndTime = 0001,
                Code = "F00"
            };
            Course CSE24 = new Course()
            {
                CourseAbbreviation = "CSE24",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
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
                StartTime = 1500,
                EndTime = 1620,
                Code = "D00"
            };
            Course CSE25 = new Course()
            {
                CourseAbbreviation = "CSE25",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
                Description = ""
            };
            Section CSE25Section1 = new Section()
            {
                Ticket = 251,
                Course = CSE25
            };

            // Adding different sections to the course object
            //adding for cse 20
            CSE20.Sections.Add(CSE20Section1);
            CSE20Section1.Meetings.Add(CSE20Lecture);
            //CSE20Section1.Meetings.Add(CSE20Discussion1);
            //CSE20Section2.Meetings.Add(CSE20Lecture);
            //CSE20Section2.Meetings.Add(CSE20Discussion2);
            //CSE20.Sections.Add(CSE20Section2);
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

            Course[] courses = { CSE20, CSE21, CSE22, CSE23, CSE24, CSE25 };
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
        /*[Fact]
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
        }*/

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





    }
}

