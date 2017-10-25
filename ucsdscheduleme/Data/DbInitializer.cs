using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Data
{
    public class DbInitializer
    {
        public static void Initialize(ScheduleContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Adding data into the Location Table
            Location centr115 = new Location()
            {
                RoomNumber = 115, 
                Building = "Center"
            };
            Location pcynh120 = new Location()
            {
                RoomNumber = 120, 
                Building = "Pepper Canyon Hall"
            };
            Location centr101 = new Location()
            {
                RoomNumber = 101, 
                Building = "Center"
            };
            Location centr105 = new Location()
            {
                RoomNumber = 105, 
                Building = "Center"
            };
            Location wlh2207 = new Location()
            {
                RoomNumber = 2207, 
                Building = "Warren Lecture Hall"
            };
            Location tba = new Location()
            {
                RoomNumber = 0,
                Building = "TBA"
            };

            context.Locations.Add(centr115);
            context.Locations.Add(pcynh120);
            context.Locations.Add(centr101);
            context.Locations.Add(centr105);
            context.Locations.Add(wlh2207);
            context.Locations.Add(tba);

            context.SaveChanges();

            // Adding Data into the Meeting Table
            Meeting CSE20Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1400,
                EndTime = 1520,
                Location = centr115,
                number = "A00"
                //Section
            };
            Meeting CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1000, EndTime = 1050, 
                Location = pcynh120,
                number = "A02"
                //Section
            };
            Meeting CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1100,
                EndTime = 1150, 
                Location = pcynh120,
                number = "A03"
                //Section
            };
            Meeting CSE20Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1200,
                EndTime = 1250, 
                Location = pcynh120,
                number = "A04"
                //Section
            };
            Meeting CSE20Review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = 1000,
                EndTime = 1150, 
                Location = centr101
                //Section
            };
            Meeting CSE20Final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = 1130,
                EndTime = 1429, 
                Location = tba
                //Section
            };

            context.Meetings.Add(CSE20Lecture);
            context.Meetings.Add(CSE20Discussion1);
            context.Meetings.Add(CSE20Discussion2);
            context.Meetings.Add(CSE20Discussion3);
            context.Meetings.Add(CSE20Review1);
            context.Meetings.Add(CSE20Final);

            context.SaveChanges();

            // Adding data into the Professor Table
            Professor prof = new Professor() { Name = "Minnes Kemp, Mor Mia" };

            context.Professor.Add(prof);

            context.SaveChanges();

            // Adding data into the Section Table
            Section CSE20Section = new Section()
            {
                Professor = prof,
                Meetings = new List<Meeting>()
                {
                    CSE20Lecture, CSE20Discussion1, CSE20Discussion2, CSE20Discussion3, CSE20Review1, CSE20Final
                }
                // Course
            };

            context.Sections.Add(CSE20Section);

            context.SaveChanges();

            // Adding data into the Course Table
            Course CSE20 = new Course()
            {
                CourseAbbreviation = "CSE20",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
                Description = "",
                Sections = new List<Section>
                {
                    CSE20Section
                }
                // Pre-Requisite
                // Co- Requisite
                // Evaluation
            };

            context.Courses.Add(CSE20);

            context.SaveChanges();

            // Adding data into the Cape Table
            Cape CSE20Cape = new Cape()
            {
                Term = "WI17",
                StudentsEnrolled = 267,
                NumberOfEvaluation = 210,
                RecommendedClass = (decimal)89.7,
                RecommendedProfessor = (decimal)97.5,
                StudyHoursPerWeek = (decimal)6.69,
                AverageGradeExpected = "B+ (3.68)",
                AverageGradeReceived = "B (3.16)"
                //URL = 
            };

            context.Cape.Add(CSE20Cape);

            context.SaveChanges();

            // Adding data into the RateMyProfessor Table
            RateMyProfessor CSE20RateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = (decimal)4.4,
                WouldTakeAgain = 88,
                LevelOfDifficulty = (decimal)3.4,
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=1516842"
            };

            context.RateMyProfessor.Add(CSE20RateMyProfessor);

            context.SaveChanges();

            // Adding data into the Evaluation Table
            Evaluation CSE20Eval = new Evaluation()
            {
                Course = CSE20,
                Professor = prof,
                Cape = CSE20Cape,
                RateMyProfessor = CSE20RateMyProfessor
            };

            context.Evaluations.Add(CSE20Eval);

            context.SaveChanges();

            // Filling out the remaining data
            CSE20Lecture.Section = CSE20Discussion1.Section = CSE20Discussion2.Section 
                = CSE20Discussion3.Section = CSE20Review1.Section = CSE20Final.Section = CSE20Section;

            CSE20Section.Course = CSE20;

            CSE20.Evaluations.Add(CSE20Eval);

            prof.Sections.Add(CSE20Section);

            prof.Evaluations.Add(CSE20Eval);

            //context.SaveChanges(); <-- Not showing up in the database
            
            // Make all the context save here or one by one like it is now. <------------------
            // Need to save all meeting types, course, and evaluations <----------------------

            // Adding another class
            CSE20Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1100,
                EndTime = 1220,
                Location = centr105,
                number = "B00"
                //Section
            };
            CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1600,
                EndTime = 1650,
                Location = wlh2207,
                number = "B01"
                //Section
            };
            CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1700,
                EndTime = 1750,
                Location = wlh2207,
                number = "B02"
                //Section
            };
            CSE20Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1800,
                EndTime = 1850,
                Location = wlh2207,
                number = "B03"
                //Section
            };
            CSE20Review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = 1000,
                EndTime = 1150,
                Location = tba
                //Section
            };
            CSE20Final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = 1130,
                EndTime = 1429,
                Location = tba
                //Section
            };

            context.Meetings.Add(CSE20Lecture);
            context.Meetings.Add(CSE20Discussion1);
            context.Meetings.Add(CSE20Discussion2);
            context.Meetings.Add(CSE20Discussion3);
            context.Meetings.Add(CSE20Review1);
            context.Meetings.Add(CSE20Final);

            context.SaveChanges();

            // Adding data into the Section Table
            CSE20Section = new Section()
            {
                Professor = prof,
                Meetings = new List<Meeting>()
                {
                    CSE20Lecture, CSE20Discussion1, CSE20Discussion2, CSE20Discussion3, CSE20Review1, CSE20Final
                }
                // Course
            };

            context.Sections.Add(CSE20Section);

            context.SaveChanges();

            // Adding data into the Course Table
            CSE20.Sections.Add(CSE20Section);

            context.SaveChanges();

            // Adding data into the Cape Table
            CSE20Cape = new Cape()
            {
                Term = "WI17",
                StudentsEnrolled = 101,
                NumberOfEvaluation = 83,
                RecommendedClass = (decimal)90.4,
                RecommendedProfessor = (decimal)97.6,
                StudyHoursPerWeek = (decimal)7.28,
                AverageGradeExpected = "B+ (3.65)",
                AverageGradeReceived = "B (3.16)"
                //URL = 
            };

            context.Cape.Add(CSE20Cape);

            context.SaveChanges();

            // Adding data into the RateMyProfessor Table
            CSE20RateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = (decimal)4.4,
                WouldTakeAgain = 88,
                LevelOfDifficulty = (decimal)3.4,
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=1516842"
            };

            context.RateMyProfessor.Add(CSE20RateMyProfessor);

            context.SaveChanges();

            // Adding data into the Evaluation Table
            CSE20Eval = new Evaluation()
            {
                Course = CSE20,
                Professor = prof,
                Cape = CSE20Cape,
                RateMyProfessor = CSE20RateMyProfessor
            };

            context.Evaluations.Add(CSE20Eval);

            context.SaveChanges();

            // Filling out the remaining data
            CSE20Lecture.Section = CSE20Discussion1.Section = CSE20Discussion2.Section
                = CSE20Discussion3.Section = CSE20Review1.Section = CSE20Final.Section = CSE20Section;

            CSE20Section.Course = CSE20;

            CSE20.Evaluations.Add(CSE20Eval);

            prof.Evaluations.Add(CSE20Eval);
            
        }
    }
}
