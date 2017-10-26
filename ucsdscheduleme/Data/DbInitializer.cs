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

            // Making Location objects
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

            // Making Meeting Objects
            Meeting CSE20Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1400,
                EndTime = 1520,
                Location = centr115,
                Code = "A00"
            };
            Meeting CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1000, EndTime = 1050, 
                Location = pcynh120,
                Code = "A02"
            };
            Meeting CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1100,
                EndTime = 1150, 
                Location = pcynh120,
                Code = "A03"
            };
            Meeting CSE20Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1200,
                EndTime = 1250, 
                Location = pcynh120,
                Code = "A04"
            };
            Meeting CSE20Review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = 1000,
                EndTime = 1150, 
                Location = centr101,
                StartDate = new DateTime(2017, 10, 29)
            };
            Meeting CSE20Final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = 1130,
                EndTime = 1429,
                Location = tba,
                StartDate = new DateTime(2017, 12, 16)
            };

            // Making Professor Object
            Professor prof = new Professor()
            {
                Name = "Minnes Kemp, Mor Mia",
            };

            // Making Section Object
            Section CSE20Section = new Section()
            {
                Professor = prof,
            };

            CSE20Section.Meetings.Add(CSE20Lecture);
            CSE20Section.Meetings.Add(CSE20Discussion1);
            CSE20Section.Meetings.Add(CSE20Discussion2);
            CSE20Section.Meetings.Add(CSE20Discussion3);
            CSE20Section.Meetings.Add(CSE20Review1);
            CSE20Section.Meetings.Add(CSE20Final);

            // Making Course Object
            Course CSE20 = new Course()
            {
                CourseAbbreviation = "CSE20",
                CourseName = "Intro / Discrete Mathematics",
                Units = 4,
                Description = "",

                // Pre-Requisite
                // Co- Requisite
            };

            CSE20.Sections.Add(CSE20Section);

            // Making Cape Object
            Cape CSE20Cape = new Cape()
            {
                Term = "WI17",
                StudentsEnrolled = 267,
                NumberOfEvaluation = 210,
                RecommendedClass = 89.7M,
                RecommendedProfessor = 97.5M,
                StudyHoursPerWeek = 6.69M,
                AverageGradeExpected = "B+ (3.68)",
                AverageGradeReceived = "B (3.16)",
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=895687"
            };

            // Making RateMyProfessor Object
            RateMyProfessor CSE20RateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = (decimal)4.4,
                WouldTakeAgain = 88,
                LevelOfDifficulty = (decimal)3.4,
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=1516842"
            };

            // Filling out the remaining data
            CSE20Lecture.Section = CSE20Discussion1.Section = CSE20Discussion2.Section 
                = CSE20Discussion3.Section = CSE20Review1.Section = CSE20Final.Section = CSE20Section;

            CSE20Section.Course = CSE20;

            CSE20.Cape.Add(CSE20Cape);

            prof.Sections.Add(CSE20Section);

            prof.Cape.Add(CSE20Cape);
            prof.RateMyProfessor.Add(CSE20RateMyProfessor);

            // Adding Location Objects to the DB
            context.Locations.Add(centr115);
            context.Locations.Add(pcynh120);
            context.Locations.Add(centr101);
            context.Locations.Add(centr105);
            context.Locations.Add(wlh2207);
            context.Locations.Add(tba);
            // Adding Meeting Objects to the DB
            context.Meetings.Add(CSE20Lecture);
            context.Meetings.Add(CSE20Discussion1);
            context.Meetings.Add(CSE20Discussion2);
            context.Meetings.Add(CSE20Discussion3);
            context.Meetings.Add(CSE20Review1);
            context.Meetings.Add(CSE20Final);
            // Adding Professor Object to the DB
            context.Professor.Add(prof);
            // Adding Section Object to the DB
            context.Sections.Add(CSE20Section);
            // Adding Course Object to the DB
            context.Courses.Add(CSE20);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE20Cape);
            // Adding RateMyProfessor Object to DB
            context.RateMyProfessor.Add(CSE20RateMyProfessor);

            context.SaveChanges();

            // Adding another CSE20 Section
            CSE20Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1100,
                EndTime = 1220,
                Location = centr105,
                Code = "B00"
            };
            CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1600,
                EndTime = 1650,
                Location = wlh2207,
                Code = "B01"
            };
            CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1700,
                EndTime = 1750,
                Location = wlh2207,
                Code = "B02"
            };
            CSE20Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1800,
                EndTime = 1850,
                Location = wlh2207,
                Code = "B03"
            };
            CSE20Review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = 1000,
                EndTime = 1150,
                Location = tba,
                StartDate = new DateTime(2017, 10, 29)
            };
            CSE20Final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = 1130,
                EndTime = 1429,
                Location = tba,
                StartDate = new DateTime(2017, 12, 16)
            };

            // Adding data into the Section Table
            CSE20Section = new Section()
            {
                Professor = prof,
            };

            CSE20Section.Meetings.Add(CSE20Lecture);
            CSE20Section.Meetings.Add(CSE20Discussion1);
            CSE20Section.Meetings.Add(CSE20Discussion2);
            CSE20Section.Meetings.Add(CSE20Discussion3);
            CSE20Section.Meetings.Add(CSE20Review1);
            CSE20Section.Meetings.Add(CSE20Final);

            CSE20.Sections.Add(CSE20Section);

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
                AverageGradeReceived = "B (3.16)",
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=895690"
            };

            // Adding data into the RateMyProfessor Table
            CSE20RateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = 4.4M,
                WouldTakeAgain = 88,
                LevelOfDifficulty = 3.4M,
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=1516842"
            };

            // Filling out the remaining data
            CSE20Lecture.Section = CSE20Discussion1.Section = CSE20Discussion2.Section
                = CSE20Discussion3.Section = CSE20Review1.Section = CSE20Final.Section = CSE20Section;

            CSE20Section.Course = CSE20;

            CSE20.Cape.Add(CSE20Cape);

            prof.Cape.Add(CSE20Cape);
            prof.RateMyProfessor.Add(CSE20RateMyProfessor);

            // Adding Meeting Objects to DB
            context.Meetings.Add(CSE20Lecture);
            context.Meetings.Add(CSE20Discussion1);
            context.Meetings.Add(CSE20Discussion2);
            context.Meetings.Add(CSE20Discussion3);
            context.Meetings.Add(CSE20Review1);
            context.Meetings.Add(CSE20Final);
            // Adding Section Object to the DB
            context.Sections.Add(CSE20Section);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE20Cape);
            // Adding Rate My Professor Object to the DB
            context.RateMyProfessor.Add(CSE20RateMyProfessor);

            context.SaveChanges();

            var count = context.Sections.First().Meetings.Count;

            bool test = true;
        }
    }
}
