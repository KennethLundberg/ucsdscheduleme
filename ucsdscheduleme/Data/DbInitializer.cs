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
            Professor MinnesKemp = new Professor()
            {
                Name = "Minnes Kemp, Mor Mia",
            };

            // Making Section Object
            Section CSE20Section = new Section()
            {
                Professor = MinnesKemp,
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
            RateMyProfessor MinnesKempRateMyProfessor = new RateMyProfessor()
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

            MinnesKemp.Sections.Add(CSE20Section);

            MinnesKemp.Cape.Add(CSE20Cape);
            MinnesKemp.RateMyProfessor.Add(MinnesKempRateMyProfessor);

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
            context.Professor.Add(MinnesKemp);
            // Adding Section Object to the DB
            context.Sections.Add(CSE20Section);
            // Adding Course Object to the DB
            context.Courses.Add(CSE20);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE20Cape);
            // Adding RateMyProfessor Object to DB
            context.RateMyProfessor.Add(MinnesKempRateMyProfessor);

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
                Professor = MinnesKemp,
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

            // Filling out the remaining data
            CSE20Lecture.Section = CSE20Discussion1.Section = CSE20Discussion2.Section
                = CSE20Discussion3.Section = CSE20Review1.Section = CSE20Final.Section = CSE20Section;

            CSE20Section.Course = CSE20;

            CSE20.Cape.Add(CSE20Cape);

            MinnesKemp.Cape.Add(CSE20Cape);

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

            context.SaveChanges();

            // Adding CSE 11
            Location WLH2001 = new Location()
            {
                Building = "Warren Lecture Hall",
                RoomNumber = 2001
            };

            Location SOLIS107 = new Location()
            {
                Building = "Solios Hall",
                RoomNumber = 107
            };
            Location PETER108 = new Location()
            {
                Building = "Peterson Hall",
                RoomNumber = 108
            };
            Location CENTR119 = new Location()
            {
                Building = "Center Hall",
                RoomNumber = 119
            };
            Location PETER110 = new Location()
            {
                Building = "Peterson Hall",
                RoomNumber = 110
            };

            Meeting CSE11Lecture = new Meeting()
            {
                Code = "A00",
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 0800,
                EndTime = 0920,
                MeetingType = MeetingType.Lecture,
                Location = WLH2001
            };
            Meeting CSE11Discussion = new Meeting()
            {
                Code = "A01",
                Days = Days.Friday,
                StartTime = 1000,
                EndTime = 1050,
                MeetingType = MeetingType.Discussion,
                Location = SOLIS107
            };
            Meeting CSE11Lab = new Meeting()
            {
                Code = "A50",
                Days = Days.TBA,
                StartTime = 0,
                EndTime = 0,
                MeetingType = MeetingType.Lab,
                Location = tba
            };
            Meeting CSE11Review1 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = PETER108,
                StartDate = new DateTime(2017, 10, 09)
            };
            Meeting CSE11Review2 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = CENTR119,
                StartDate = new DateTime(2017, 10, 23)
            };
            Meeting CSE11Review3 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = PETER108,
                StartDate = new DateTime(2017, 11, 01)
            };
            Meeting CSE11Review4 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = 1600,
                EndTime = 1650,
                MeetingType = MeetingType.Review,
                Location = WLH2001,
                StartDate = new DateTime(2017, 11, 05)
            };
            Meeting CSE11Review5 = new Meeting()
            {
                Days = Days.Thursday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = PETER110,
                StartDate = new DateTime(2017, 11, 09)
            };
            Meeting CSE11Review6 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = CENTR119,
                StartDate = new DateTime(2017, 11, 20)
            };
            Meeting CSE11Review7 = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = 0830,
                EndTime = 0920,
                MeetingType = MeetingType.Review,
                Location = PETER108,
                StartDate = new DateTime(2017, 11, 21)
            };
            Meeting CSE11Review8 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = WLH2001,
                StartDate = new DateTime(2017, 12, 06)
            };
            Meeting CSE11Review9 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = 1600,
                EndTime = 1650,
                MeetingType = MeetingType.Review,
                Location = WLH2001,
                StartDate = new DateTime(2017, 12, 10)
            };
            Meeting CSE11Final = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = 0800,
                EndTime = 1059,
                MeetingType = MeetingType.Final,
                Location = tba,
                StartDate = new DateTime(2017, 12, 12)
            };

            Professor RickOrd = new Professor()
            {
                Name = "Rick Ord",
            };

            Section CSE11A = new Section()
            {
                Professor = RickOrd,
            };

            CSE11A.Meetings.Add(CSE11Discussion);
            CSE11A.Meetings.Add(CSE11Final);
            CSE11A.Meetings.Add(CSE11Lab);
            CSE11A.Meetings.Add(CSE11Lecture);
            CSE11A.Meetings.Add(CSE11Review1);
            CSE11A.Meetings.Add(CSE11Review2);
            CSE11A.Meetings.Add(CSE11Review3);
            CSE11A.Meetings.Add(CSE11Review4);
            CSE11A.Meetings.Add(CSE11Review5);
            CSE11A.Meetings.Add(CSE11Review6);
            CSE11A.Meetings.Add(CSE11Review7);
            CSE11A.Meetings.Add(CSE11Review8);
            CSE11A.Meetings.Add(CSE11Review9);

            Course CSE11 = new Course()
            {
                CourseAbbreviation = "CSE11",
                CourseName = "Intr/Computer Sci&Obj-Ori: Java",
                Units = 4,
                Description = ""
                // Cape
            };

            CSE11.Sections.Add(CSE11A);

            Cape CSE11Cape = new Cape()
            {
                Term = "FA16",
                StudentsEnrolled = 275,
                NumberOfEvaluation = 135,
                RecommendedClass = 91.9M,
                RecommendedProfessor = 96.0M,
                StudyHoursPerWeek = 11.98M,
                AverageGradeExpected = "B+ (3.65)",
                AverageGradeReceived = "B+ (3.52)",
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=882255"
            };

            RateMyProfessor RickOrdRateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = 4.3M,
                WouldTakeAgain = 61,
                LevelOfDifficulty = 2.4M,
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=63529"
            };

            CSE11Lecture.Section = CSE11Discussion.Section = CSE11Final.Section
                = CSE11Lab.Section = CSE11Review1.Section = CSE11Review2.Section 
                = CSE11Review3.Section = CSE11Review4.Section = CSE11Review5.Section
                = CSE11Review6.Section = CSE11Review7.Section = CSE11Review8.Section
                = CSE11Review9.Section = CSE11A;

            CSE11A.Course = CSE11;

            CSE11.Cape.Add(CSE11Cape);

            RickOrd.Sections.Add(CSE11A);

            RickOrd.Cape.Add(CSE11Cape);
            RickOrd.RateMyProfessor.Add(RickOrdRateMyProfessor);

            context.Locations.Add(WLH2001);
            context.Locations.Add(SOLIS107);
            context.Locations.Add(PETER108);
            context.Locations.Add(CENTR119);
            context.Locations.Add(PETER110);

            context.Meetings.Add(CSE11Lecture);
            context.Meetings.Add(CSE11Discussion);
            context.Meetings.Add(CSE11Final);
            context.Meetings.Add(CSE11Lab);
            context.Meetings.Add(CSE11Review1);
            context.Meetings.Add(CSE11Review2);
            context.Meetings.Add(CSE11Review3);
            context.Meetings.Add(CSE11Review4);
            context.Meetings.Add(CSE11Review5);
            context.Meetings.Add(CSE11Review6);
            context.Meetings.Add(CSE11Review7);
            context.Meetings.Add(CSE11Review8);
            context.Meetings.Add(CSE11Review9);

            context.Professor.Add(RickOrd);

            context.Sections.Add(CSE11A);

            context.Courses.Add(CSE11);

            context.Cape.Add(CSE11Cape);

            context.RateMyProfessor.Add(RickOrdRateMyProfessor);

            context.SaveChanges();

            // Adding a New CSE11 Section

            Location WLH2005 = new Location()
            {
                Building = "Warren Lecture Hall",
                RoomNumber = 2005
            };

            CSE11Lecture = new Meeting()
            {
                Code = "B00",
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1100,
                EndTime = 1220,
                MeetingType = MeetingType.Lecture,
                Location = WLH2005
            };
            CSE11Lab = new Meeting()
            {
                Code = "B50",
                Days = Days.TBA,
                StartTime = 0,
                EndTime = 0,
                MeetingType = MeetingType.Lab,
                Location = tba
                // Section
            };
            CSE11Discussion = new Meeting()
            {
                Code = "B01",
                Days = Days.Friday,
                StartTime = 1100,
                EndTime = 1150,
                MeetingType = MeetingType.Discussion,
                Location = SOLIS107
            };
            CSE11Review1 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 10, 09)
                // Section
            };
            CSE11Review2 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 10, 23)
                // Section
            };
            CSE11Review3 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 11, 01)
                // Section
            };
            CSE11Review4 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = 1600,
                EndTime = 1650,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 11, 05)
                // Section
            };
            CSE11Review5 = new Meeting()
            {
                Days = Days.Thursday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 11, 09)
                // Section
            };
            CSE11Review6 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 11, 20)
                // Section
            };
            CSE11Review7 = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = 0830,
                EndTime = 0920,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 11, 21)
                // Section
            };
            CSE11Review8 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = 0800,
                EndTime = 0850,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 12, 06)
                // Section
            };
            CSE11Review9 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = 1600,
                EndTime = 1650,
                MeetingType = MeetingType.Review,
                Location = tba,
                StartDate = new DateTime(2017, 12, 10)
                // Section
            };
            CSE11Final = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = 0800,
                EndTime = 1059,
                MeetingType = MeetingType.Final,
                Location = tba,
                StartDate = new DateTime(2017, 12, 13)
            };

            Section CSE11B = new Section()
            {
                Professor = RickOrd
            };
            CSE11B.Course = CSE11;
            CSE11B.Meetings.Add(CSE11Discussion);
            CSE11B.Meetings.Add(CSE11Lecture);
            CSE11B.Meetings.Add(CSE11Lab);
            CSE11B.Meetings.Add(CSE11Review1);
            CSE11B.Meetings.Add(CSE11Review2);
            CSE11B.Meetings.Add(CSE11Review3);
            CSE11B.Meetings.Add(CSE11Review4);
            CSE11B.Meetings.Add(CSE11Review5);
            CSE11B.Meetings.Add(CSE11Review6);
            CSE11B.Meetings.Add(CSE11Review7);
            CSE11B.Meetings.Add(CSE11Review8);
            CSE11B.Meetings.Add(CSE11Review9);
            CSE11B.Meetings.Add(CSE11Final);

            CSE11.Sections.Add(CSE11B);

            CSE11Cape = new Cape()
            {
                Term = "FA16",
                StudentsEnrolled = 258,
                NumberOfEvaluation = 140,
                RecommendedClass = 92.1M,
                RecommendedProfessor = 97.6M,
                StudyHoursPerWeek = 12.34M,
                AverageGradeExpected = "A- (3.71)",
                AverageGradeReceived = "B+ (3.52)",
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=885069"
            };

            CSE11Lecture.Section = CSE11Discussion.Section = CSE11Final.Section
                = CSE11Lab.Section = CSE11Review1.Section = CSE11Review2.Section
                = CSE11Review3.Section = CSE11Review4.Section = CSE11Review5.Section
                = CSE11Review6.Section = CSE11Review7.Section = CSE11Review8.Section
                = CSE11Review9.Section = CSE11B;

            CSE11B.Course = CSE11;

            CSE11.Cape.Add(CSE11Cape);

            RickOrd.Sections.Add(CSE11B);

            RickOrd.Cape.Add(CSE11Cape);

            context.Locations.Add(WLH2005);

            context.Meetings.Add(CSE11Lecture);
            context.Meetings.Add(CSE11Discussion);
            context.Meetings.Add(CSE11Final);
            context.Meetings.Add(CSE11Lab);
            context.Meetings.Add(CSE11Review1);
            context.Meetings.Add(CSE11Review2);
            context.Meetings.Add(CSE11Review3);
            context.Meetings.Add(CSE11Review4);
            context.Meetings.Add(CSE11Review5);
            context.Meetings.Add(CSE11Review6);
            context.Meetings.Add(CSE11Review7);
            context.Meetings.Add(CSE11Review8);
            context.Meetings.Add(CSE11Review9);

            context.Sections.Add(CSE11B);

            context.Cape.Add(CSE11Cape);

            context.SaveChanges();

            var count = context.Sections.First().Meetings.Count;

            bool test = true;
        }
    }
}
