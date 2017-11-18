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
            // TODO put back before commiting
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // Making Location objects
            Location CENTR115 = new Location()
            {
                RoomNumber = "115", 
                Building = "Center"
            };
            Location PCYNH120 = new Location()
            {
                RoomNumber = "120", 
                Building = "Pepper Canyon Hall"
            };
            Location CENTR101 = new Location()
            {
                RoomNumber = "101", 
                Building = "Center"
            };
            Location CENTR105 = new Location()
            {
                RoomNumber = "105", 
                Building = "Center"
            };
            Location WLH2207 = new Location()
            {
                RoomNumber = "2207", 
                Building = "Warren Lecture Hall"
            };
            Location TBA = new Location()
            {
                RoomNumber = "",
                Building = "TBA"
            };

            // Making Meeting Objects
            Meeting CSE20Lecture = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new DateTime(0, 0, 0, 14, 0, 0),
                EndTime = new DateTime(0, 0, 0, 15, 20, 0),
                Location = CENTR115,
                Code = "A00"
            };
            Meeting CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 50, 0),
                Location = PCYNH120,
                Code = "A02"
            };
            Meeting CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 11, 0, 0),
                EndTime = new DateTime(0, 0, 0, 11, 50, 0),
                Location = PCYNH120,
                Code = "A03"
            };
            Meeting CSE20Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 12, 0, 0),
                EndTime = new DateTime(0, 0, 0, 12, 50, 0),
                Location = PCYNH120,
                Code = "A04"
            };
            Meeting CSE20Review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                EndTime = new DateTime(0, 0, 0, 11, 50, 0),
                Location = CENTR101,
                StartDate = new DateTime(2017, 10, 29)
            };
            Meeting CSE20Final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = new DateTime(0, 0, 0, 11, 30, 0),
                EndTime = new DateTime(0, 0, 0, 14, 29, 0),
                Location = TBA,
                StartDate = new DateTime(2017, 12, 16)
            };

            // Making Professor Object
            Professor MinnesKemp = new Professor()
            {
                Name = "Minnes Kemp, Mor Mia",
            };

            // Making Section Object
            Section CSE20Section1 = new Section()
            {
                Ticket = 915105,
                Professor = MinnesKemp,
            };
            Section CSE20Section2 = new Section()
            {
                Ticket = 915106,
                Professor = MinnesKemp,
            };
            Section CSE20Section3 = new Section()
            {
                Ticket = 915107,
                Professor = MinnesKemp,
            };

            // Adding Meetings to the sections
            CSE20Section1.Meetings.Add(CSE20Lecture);
            CSE20Section1.Meetings.Add(CSE20Discussion1);
            CSE20Section1.Meetings.Add(CSE20Final);
            CSE20Section1.Meetings.Add(CSE20Review1);

            CSE20Section2.Meetings.Add(CSE20Lecture);
            CSE20Section2.Meetings.Add(CSE20Discussion2);
            CSE20Section2.Meetings.Add(CSE20Final);
            CSE20Section2.Meetings.Add(CSE20Review1);

            CSE20Section3.Meetings.Add(CSE20Lecture);
            CSE20Section3.Meetings.Add(CSE20Discussion3);
            CSE20Section3.Meetings.Add(CSE20Final);
            CSE20Section3.Meetings.Add(CSE20Review1);

            // Making Course Object
            Course CSE20 = new Course()
            {
                CourseAbbreviation = "CSE20",
                CourseName = "Intro / Discrete Mathematics",
                Units = "4",
                Description = ""
            };

            // Adding different sections to the course object
            CSE20.Sections.Add(CSE20Section1);
            CSE20.Sections.Add(CSE20Section2);
            CSE20.Sections.Add(CSE20Section3);

            // Making Cape Object
            Cape CSE20Cape = new Cape()
            {
                Term = "WI17",
                StudentsEnrolled = 267,
                NumberOfEvaluation = 210,
                RecommendedClass = 89.7M,
                RecommendedProfessor = 97.5M,
                StudyHoursPerWeek = 6.69M,
                AverageGradeExpected = 3.68M,
                AverageGradeReceived = 3.16M,
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=895687"
            };

            // Making RateMyProfessor Object
            RateMyProfessor MinnesKempRateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = "4.4",
                WouldTakeAgain = "88",
                LevelOfDifficulty = "3.4",
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=1516842"
            };

            // Adding course name in the section object
            CSE20Section1.Course = CSE20;
            CSE20Section2.Course = CSE20;
            CSE20Section3.Course = CSE20;

            // Adding Cape to the course object
            CSE20.Cape.Add(CSE20Cape);

            // Adding cape and rate my professor to the professor object
            MinnesKemp.Cape.Add(CSE20Cape);
            MinnesKemp.RateMyProfessor = MinnesKempRateMyProfessor;

            // Adding section to the professor object
            MinnesKemp.Sections.Add(CSE20Section1);
            MinnesKemp.Sections.Add(CSE20Section2);
            MinnesKemp.Sections.Add(CSE20Section3);

            // Adding Location Objects to the DB
            context.Locations.Add(CENTR115);
            context.Locations.Add(PCYNH120);
            context.Locations.Add(CENTR101);
            context.Locations.Add(CENTR105);
            context.Locations.Add(WLH2207);
            context.Locations.Add(TBA);
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
            context.Sections.Add(CSE20Section1);
            context.Sections.Add(CSE20Section2);
            context.Sections.Add(CSE20Section3);
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
                StartTime = new DateTime(0, 0, 0, 11, 0, 0),
                EndTime = new DateTime(0, 0, 0, 12, 20, 0),
                Location = CENTR105,
                Code = "B00"
            };
            CSE20Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 16, 0, 0),
                EndTime = new DateTime(0, 0, 0, 16, 50, 0),
                Location = WLH2207,
                Code = "B01"
            };
            CSE20Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 17, 0, 0),
                EndTime = new DateTime(0, 0, 0, 17, 50, 0),
                Location = WLH2207,
                Code = "B02"
            };
            CSE20Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 18, 0, 0),
                EndTime = new DateTime(0, 0, 0, 18, 50, 0),
                Location = WLH2207,
                Code = "B03"
            };
            CSE20Review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                EndTime = new DateTime(0, 0, 0, 11, 50, 0),
                Location = TBA,
                StartDate = new DateTime(2017, 10, 29)
            };
            CSE20Final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = new DateTime(0, 0, 0, 11, 30, 0),
                EndTime = new DateTime(0, 0, 0, 14, 29, 0),
                Location = TBA,
                StartDate = new DateTime(2017, 12, 16)
            };

            // Adding data into the Section Table
            CSE20Section1 = new Section()
            {
                Ticket = 915110,
                Professor = MinnesKemp,
            };
            CSE20Section2 = new Section()
            {
                Ticket = 915111,
                Professor = MinnesKemp,
            };
            CSE20Section3 = new Section()
            {
                Ticket = 915112,
                Professor = MinnesKemp,
            };

            // Adding Meeting times to the sections
            CSE20Section1.Meetings.Add(CSE20Lecture);
            CSE20Section1.Meetings.Add(CSE20Discussion1);
            CSE20Section1.Meetings.Add(CSE20Final);
            CSE20Section1.Meetings.Add(CSE20Review1);

            CSE20Section2.Meetings.Add(CSE20Lecture);
            CSE20Section2.Meetings.Add(CSE20Discussion2);
            CSE20Section2.Meetings.Add(CSE20Final);
            CSE20Section2.Meetings.Add(CSE20Review1);

            CSE20Section3.Meetings.Add(CSE20Lecture);
            CSE20Section3.Meetings.Add(CSE20Discussion3);
            CSE20Section3.Meetings.Add(CSE20Final);
            CSE20Section3.Meetings.Add(CSE20Review1);

            // Adding different sections to the course object
            CSE20.Sections.Add(CSE20Section1);
            CSE20.Sections.Add(CSE20Section2);
            CSE20.Sections.Add(CSE20Section3);

            // Adding data into the Cape Table
            CSE20Cape = new Cape()
            {
                Term = "WI17",
                StudentsEnrolled = 101,
                NumberOfEvaluation = 83,
                RecommendedClass = (decimal)90.4,
                RecommendedProfessor = (decimal)97.6,
                StudyHoursPerWeek = (decimal)7.28,
                AverageGradeExpected = 3.65M,
                AverageGradeReceived = 3.16M,
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=895690"
            };

            // Adding course name in the section object
            CSE20Section1.Course = CSE20;
            CSE20Section2.Course = CSE20;
            CSE20Section3.Course = CSE20;

            CSE20.Cape.Add(CSE20Cape);

            MinnesKemp.Cape.Add(CSE20Cape);

            // Adding section to the professor object
            MinnesKemp.Sections.Add(CSE20Section1);
            MinnesKemp.Sections.Add(CSE20Section2);
            MinnesKemp.Sections.Add(CSE20Section3);

            // Adding Meeting Objects to DB
            context.Meetings.Add(CSE20Lecture);
            context.Meetings.Add(CSE20Discussion1);
            context.Meetings.Add(CSE20Discussion2);
            context.Meetings.Add(CSE20Discussion3);
            context.Meetings.Add(CSE20Review1);
            context.Meetings.Add(CSE20Final);
            // Adding Section Object to the DB
            context.Sections.Add(CSE20Section1);
            context.Sections.Add(CSE20Section2);
            context.Sections.Add(CSE20Section3);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE20Cape);

            context.SaveChanges();

            // 
            // Adding CSE 11
            //

            // Making Location objects
            Location WLH2001 = new Location()
            {
                Building = "Warren Lecture Hall",
                RoomNumber = "2001"
            };

            Location SOLIS107 = new Location()
            {
                Building = "Solios Hall",
                RoomNumber = "107"
            };
            Location PETER108 = new Location()
            {
                Building = "Peterson Hall",
                RoomNumber = "108"
            };
            Location CENTR119 = new Location()
            {
                Building = "Center Hall",
                RoomNumber = "119"
            };
            Location PETER110 = new Location()
            {
                Building = "Peterson Hall",
                RoomNumber = "110"
            };

            // Making Meeting Objects
            Meeting CSE11Lecture = new Meeting()
            {
                Code = "A00",
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 9, 20, 0),
                MeetingType = MeetingType.Lecture,
                Location = WLH2001
            };
            Meeting CSE11Discussion = new Meeting()
            {
                Code = "A01",
                Days = Days.Friday,
                StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 50, 0),
                MeetingType = MeetingType.Discussion,
                Location = SOLIS107
            };
            Meeting CSE11Lab = new Meeting()
            {
                Code = "A50",
                Days = Days.TBA,
                //StartTime = null,
                //EndTime = null,
                MeetingType = MeetingType.Lab,
                Location = TBA
            };
            Meeting CSE11Review1 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = PETER108,
                StartDate = new DateTime(2017, 10, 09)
            };
            Meeting CSE11Review2 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = CENTR119,
                StartDate = new DateTime(2017, 10, 23)
            };
            Meeting CSE11Review3 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = PETER108,
                StartDate = new DateTime(2017, 11, 01)
            };
            Meeting CSE11Review4 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = new DateTime(0, 0, 0, 16, 0, 0),
                EndTime = new DateTime(0, 0, 0, 16, 50, 0),
                MeetingType = MeetingType.Review,
                Location = WLH2001,
                StartDate = new DateTime(2017, 11, 05)
            };
            Meeting CSE11Review5 = new Meeting()
            {
                Days = Days.Thursday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = PETER110,
                StartDate = new DateTime(2017, 11, 09)
            };
            Meeting CSE11Review6 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = CENTR119,
                StartDate = new DateTime(2017, 11, 20)
            };
            Meeting CSE11Review7 = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = new DateTime(0, 0, 0, 8, 30, 0),
                EndTime = new DateTime(0, 0, 0, 9, 20, 0),
                MeetingType = MeetingType.Review,
                Location = PETER108,
                StartDate = new DateTime(2017, 11, 21)
            };
            Meeting CSE11Review8 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = WLH2001,
                StartDate = new DateTime(2017, 12, 06)
            };
            Meeting CSE11Review9 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = new DateTime(0, 0, 0, 16, 0, 0),
                EndTime = new DateTime(0, 0, 0, 16, 50, 0),
                MeetingType = MeetingType.Review,
                Location = WLH2001,
                StartDate = new DateTime(2017, 12, 10)
            };
            Meeting CSE11Final = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 59, 0),
                MeetingType = MeetingType.Final,
                Location = TBA,
                StartDate = new DateTime(2017, 12, 12)
            };

            // Making Professor Object
            Professor RickOrd = new Professor()
            {
                Name = "Rick Ord",
            };

            // Making Section Object
            Section CSE11A = new Section()
            {
                Professor = RickOrd,
                Ticket = 915077
            };

            // Adding Meetings to the sections
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

            // Making Course Object
            Course CSE11 = new Course()
            {
                CourseAbbreviation = "CSE11",
                CourseName = "Intr/Computer Sci&Obj-Ori: Java",
                Units = "4",
                Description = ""
                // Cape
            };

            // Adding sections to the course object
            CSE11.Sections.Add(CSE11A);

            // Making Cape Object
            Cape CSE11Cape = new Cape()
            {
                Term = "FA16",
                StudentsEnrolled = 275,
                NumberOfEvaluation = 135,
                RecommendedClass = 91.9M,
                RecommendedProfessor = 96.0M,
                StudyHoursPerWeek = 11.98M,
                AverageGradeExpected = 3.65M,
                AverageGradeReceived = 3.52M,
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=882255"
            };

            // Making RateMyProfessor Object
            RateMyProfessor RickOrdRateMyProfessor = new RateMyProfessor()
            {
                OverallQuality = "4.3",
                WouldTakeAgain = "61",
                LevelOfDifficulty = "2.4",
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=63529"
            };

            // Adding course name in the section object
            CSE11A.Course = CSE11;

            // Adding Cape to the course object
            CSE11.Cape.Add(CSE11Cape);

            // Adding cape and rate my professor to the professor object
            RickOrd.Cape.Add(CSE11Cape);
            RickOrd.RateMyProfessor = RickOrdRateMyProfessor;

            // Adding section to the professor object
            RickOrd.Sections.Add(CSE11A);

            // Adding Location Objects to the DB
            context.Locations.Add(WLH2001);
            context.Locations.Add(SOLIS107);
            context.Locations.Add(PETER108);
            context.Locations.Add(CENTR119);
            context.Locations.Add(PETER110);
            // Adding Meeting Objects to the DB
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
            // Adding Professor Object to the DB
            context.Professor.Add(RickOrd);
            // Adding Section Object to the DB
            context.Sections.Add(CSE11A);
            // Adding Course Object to the DB
            context.Courses.Add(CSE11);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE11Cape);
            // Adding RateMyProfessor Object to DB
            context.RateMyProfessor.Add(RickOrdRateMyProfessor);

            context.SaveChanges();

            // Adding a New CSE11 Section
            Location WLH2005 = new Location()
            {
                Building = "Warren Lecture Hall",
                RoomNumber = "2005"
            };

            CSE11Lecture = new Meeting()
            {
                Code = "B00",
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = new DateTime(0, 0, 0, 11, 0, 0),
                EndTime = new DateTime(0, 0, 0, 12, 20, 0),
                MeetingType = MeetingType.Lecture,
                Location = WLH2005
            };
            CSE11Lab = new Meeting()
            {
                Code = "B50",
                Days = Days.TBA,
                //StartTime = new DateTime(0, 0, 0, 0, 0, 0),
                //EndTime = new DateTime(0, 0, 0, 0, 0, 0),
                MeetingType = MeetingType.Lab,
                Location = TBA
            };
            CSE11Discussion = new Meeting()
            {
                Code = "B01",
                Days = Days.Friday,
                StartTime = new DateTime(0, 0, 0, 11, 0, 0),
                EndTime = new DateTime(0, 0, 0, 11, 50, 0),
                MeetingType = MeetingType.Discussion,
                Location = SOLIS107
            };
            CSE11Review1 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 10, 09)
            };
            CSE11Review2 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 10, 23)
            };
            CSE11Review3 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 11, 01)
            };
            CSE11Review4 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = new DateTime(0, 0, 0, 16, 0, 0),
                EndTime = new DateTime(0, 0, 0, 16, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 11, 05)
            };
            CSE11Review5 = new Meeting()
            {
                Days = Days.Thursday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 11, 09)
            };
            CSE11Review6 = new Meeting()
            {
                Days = Days.Monday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 11, 20)
            };
            CSE11Review7 = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = new DateTime(0, 0, 0, 8, 30, 0),
                EndTime = new DateTime(0, 0, 0, 9, 20, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 11, 21)
            };
            CSE11Review8 = new Meeting()
            {
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 12, 06)
            };
            CSE11Review9 = new Meeting()
            {
                Days = Days.Sunday,
                StartTime = new DateTime(0, 0, 0, 16, 0, 0),
                EndTime = new DateTime(0, 0, 0, 16, 50, 0),
                MeetingType = MeetingType.Review,
                Location = TBA,
                StartDate = new DateTime(2017, 12, 10)
            };
            CSE11Final = new Meeting()
            {
                Days = Days.Tuesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 59, 0),
                MeetingType = MeetingType.Final,
                Location = TBA,
                StartDate = new DateTime(2017, 12, 13)
            };

            // Making Section Object
            Section CSE11B = new Section()
            {
                Ticket = 915080,
                Professor = RickOrd
            };

            // Adding Meetings to the sections
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

            // Adding sections to the course object
            CSE11Cape = new Cape()
            {
                Term = "FA16",
                StudentsEnrolled = 258,
                NumberOfEvaluation = 140,
                RecommendedClass = 92.1M,
                RecommendedProfessor = 97.6M,
                StudyHoursPerWeek = 12.34M,
                AverageGradeExpected = 3.71M,
                AverageGradeReceived = 3.52M,
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=885069"
            };

            // Adding sections to the course object
            CSE11.Sections.Add(CSE11B);

            // Adding course name in the section object
            CSE11B.Course = CSE11;

            // Adding Cape to the course object
            CSE11.Cape.Add(CSE11Cape);

            // Adding section to the professor object
            RickOrd.Sections.Add(CSE11B);

            // Adding cape to the professor object
            RickOrd.Cape.Add(CSE11Cape);

            // Adding Location Objects to the DB
            context.Locations.Add(WLH2005);
            // Adding Meeting Objects to the DB
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
            // Adding Section Object to the DB
            context.Sections.Add(CSE11B);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE11Cape);

            context.SaveChanges();

            
            ////CSE 101
            ////

            // Making location objects
            Location CENTR214 = new Location()
            {
                Building = "Center",
                RoomNumber = "214"
            };
            Location WLH2113 = new Location()
            {
                Building = "Warren Lecture Hall",
                RoomNumber = "2113"
            };

            Location CENTR222 = new Location()
            {
                Building = "Center",
                RoomNumber = "222"
            };

            //Making meeting objects
            Meeting CSE101Lecture1 = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Code = "A00",
                Days = Days.Monday | Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 17, 0, 0),
                EndTime = new DateTime(0, 0, 0, 18, 20, 0),
                Location = CENTR214
            };
            Meeting CSE101Lecture2 = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Code = "B00",
                Days = Days.Tuesday | Days.Thursday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 9, 20, 0),
                Location = CENTR115
            };
            Meeting CSE101Discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Code = "A02",
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 9, 0, 0),
                EndTime = new DateTime(0, 0, 0, 9, 50, 0),
                Location = WLH2113

            };
            Meeting CSE101Discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Code = "A03",
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 50, 0),
                Location = WLH2113
            };
            Meeting CSE101Discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Code = "A04",
                Days = Days.Wednesday,
                StartTime = new DateTime(0, 0, 0, 11, 0, 0),
                EndTime = new DateTime(0, 0, 0, 11, 50, 0),
                Location = WLH2113
            };
            Meeting CSE101Discussion4 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Code = "B01",
                Days = Days.Friday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 8, 50, 0),
                Location = CENTR222
            };
            Meeting CSE101Discussion5 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Code = "B02",
                Days = Days.Friday,
                StartTime = new DateTime(0, 0, 0, 9, 0, 0),
                EndTime = new DateTime(0, 0, 0, 9, 50, 0),
                Location = CENTR222
            };
            Meeting CSE101Discussion6 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Code = "B03",
                Days = Days.Friday,
                StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 50, 0),
                Location = CENTR222
            };
            Meeting CSE101Final1 = new Meeting()
            {
                MeetingType = MeetingType.Final,
                StartDate = new DateTime(2017, 12, 14),
                Days = Days.Thursday,
                StartTime = new DateTime(0, 0, 0, 19, 0, 0),
                EndTime = new DateTime(0, 0, 0, 21, 59, 0),
                Location = TBA
            };
            Meeting CSE101Final2 = new Meeting()
            {
                MeetingType = MeetingType.Final,
                StartDate = new DateTime(2017, 12, 12),
                Days = Days.Tuesday,
                StartTime = new DateTime(0, 0, 0, 8, 0, 0),
                EndTime = new DateTime(0, 0, 0, 10, 59, 0),
                Location = TBA
            };

            //Making new rate my professor ratings
            RateMyProfessor ImpagliazzoRMP = new RateMyProfessor()
            {
                OverallQuality = "2.8",
                WouldTakeAgain = "0",
                LevelOfDifficulty = "3.6",
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=563826"
            };
            RateMyProfessor JonesRMP = new RateMyProfessor()
            {
                OverallQuality = "3.0",
                WouldTakeAgain = "0",
                LevelOfDifficulty = "3.0", 
                URL = "http://www.ratemyprofessors.com/ShowRatings.jsp?tid=2110462"
            };

            //Making new capes
            Cape CSE101ImpagliazzoCAPE = new Cape()
            {
                Term = "SP17",
                StudentsEnrolled = 77,
                NumberOfEvaluation = 31,
                RecommendedClass = 90.3M,
                RecommendedProfessor = 54.8M,
                StudyHoursPerWeek = 7.63M,
                AverageGradeExpected = 3.38M,
                AverageGradeReceived = 3.21M, 
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=903696"
            };
            Cape CSE101JonesCAPE = new Cape()
            {
                Term = "SP17",
                StudentsEnrolled = 229,
                NumberOfEvaluation = 102,
                RecommendedClass = 84.2M,
                RecommendedProfessor = 89.5M,
                StudyHoursPerWeek = 8.76M,
                AverageGradeExpected = 3.10M,
                AverageGradeReceived = 3.20M,
                URL = "http://cape.ucsd.edu/responses/CAPEReport.aspx?sectionid=903688"
            };

            //Making professor objects
            Professor Impagliazzo = new Professor()
            {
                Name = "Impagliazzo, Russell",
                RateMyProfessor = ImpagliazzoRMP
            };
            Professor Jones = new Professor()
            {
                Name = "Jones, Miles E",
                RateMyProfessor = JonesRMP, 
            };

            // Adding cape and rate my professor to the professor object
            Impagliazzo.RateMyProfessor = ImpagliazzoRMP;
            Jones.RateMyProfessor = JonesRMP;
            Impagliazzo.Cape.Add(CSE101ImpagliazzoCAPE);
            Jones.Cape.Add(CSE101JonesCAPE);

            // Making section1 objects
            Section CSE101Section1 = new Section()
            {
                Ticket = 915137,
                Professor = Impagliazzo
            };

            // Adding Meetings to the sections
            CSE101Section1.Meetings.Add(CSE101Lecture1);
            CSE101Section1.Meetings.Add(CSE101Discussion1);
            CSE101Section1.Meetings.Add(CSE101Final1);

            // Adding section to the professor object
            Impagliazzo.Sections.Add(CSE101Section1);

            // Making section2 objects
            Section CSE101Section2 = new Section()
            {
                Ticket = 915138,
                Professor = Jones
            };

            // Adding Meetings to the sections
            CSE101Section2.Meetings.Add(CSE101Lecture1);
            CSE101Section2.Meetings.Add(CSE101Discussion2);
            CSE101Section2.Meetings.Add(CSE101Final1);

            // Adding section to the professor object
            Impagliazzo.Sections.Add(CSE101Section2);

            // Making section3 objects
            Section CSE101Section3 = new Section()
            {
                Ticket = 915139,
                Professor = Jones
            };

            // Adding Meetings to the sections
            CSE101Section3.Meetings.Add(CSE101Lecture1);
            CSE101Section3.Meetings.Add(CSE101Discussion3);
            CSE101Section3.Meetings.Add(CSE101Final1);

            // Adding section to the professor object
            Impagliazzo.Sections.Add(CSE101Section3);

            // Making section4 objects
            Section CSE101Section4 = new Section()
            {
                Ticket = 915141,
                Professor = Jones
            };

            // Adding Meetings to the sections
            CSE101Section4.Meetings.Add(CSE101Lecture2);
            CSE101Section4.Meetings.Add(CSE101Discussion4);
            CSE101Section4.Meetings.Add(CSE101Final2);

            // Adding section to the professor object
            Jones.Sections.Add(CSE101Section4);

            // Making section5 objects
            Section CSE101Section5 = new Section()
            {
                Ticket = 915142,
                Professor = Jones
            };

            // Adding Meetings to the sections
            CSE101Section5.Meetings.Add(CSE101Lecture2);
            CSE101Section5.Meetings.Add(CSE101Discussion5);
            CSE101Section5.Meetings.Add(CSE101Final2);

            // Adding section to the professor object
            Jones.Sections.Add(CSE101Section5);

            // Making section6 objects
            Section CSE101Section6 = new Section()
            {
                Ticket = 915143,
                Professor = Jones
            };

            // Adding Meetings to the sections
            CSE101Section6.Meetings.Add(CSE101Lecture2);
            CSE101Section6.Meetings.Add(CSE101Discussion6);
            CSE101Section6.Meetings.Add(CSE101Final2);

            // Adding section to the professor object
            Jones.Sections.Add(CSE101Section6);

            //Making new Course
            Course CSE101 = new Course()
            {
                CourseAbbreviation = "CSE 101",
                CourseName = "Design & Analysis of Algorithm",
                Description = "",
                Units = "4"
            };

            // Adding different sections to the course object
            CSE101.Sections.Add(CSE101Section1);
            CSE101.Sections.Add(CSE101Section2);
            CSE101.Sections.Add(CSE101Section3);
            CSE101.Sections.Add(CSE101Section4);
            CSE101.Sections.Add(CSE101Section5);
            CSE101.Sections.Add(CSE101Section6);

            // Adding course name in the section object
            CSE101Section1.Course = CSE101;
            CSE101Section2.Course = CSE101;
            CSE101Section3.Course = CSE101;
            CSE101Section4.Course = CSE101;
            CSE101Section5.Course = CSE101;
            CSE101Section6.Course = CSE101;

            // Adding Cape to the course object
            CSE101.Cape.Add(CSE101ImpagliazzoCAPE);
            CSE101.Cape.Add(CSE101JonesCAPE);

            // Adding Location Objects to the DB
            context.Locations.Add(CENTR214);
            context.Locations.Add(WLH2113);
            context.Locations.Add(CENTR222);
            // Adding Meeting Objects to the DB
            context.Meetings.Add(CSE101Lecture1);
            context.Meetings.Add(CSE101Lecture2);
            context.Meetings.Add(CSE101Discussion1);
            context.Meetings.Add(CSE101Discussion2);
            context.Meetings.Add(CSE101Discussion3);
            context.Meetings.Add(CSE101Discussion4);
            context.Meetings.Add(CSE101Discussion5);
            context.Meetings.Add(CSE101Discussion6);
            context.Meetings.Add(CSE101Final1);
            context.Meetings.Add(CSE101Final2);
            // Adding RateMyProfessor Object to DB
            context.RateMyProfessor.Add(JonesRMP);
            context.RateMyProfessor.Add(ImpagliazzoRMP);
            // Adding Cape Object to the DB
            context.Cape.Add(CSE101JonesCAPE);
            context.Cape.Add(CSE101ImpagliazzoCAPE);
            // Adding Professor Object to the DB
            context.Professor.Add(Impagliazzo);
            context.Professor.Add(Jones);
            // Adding Section Object to the DB
            context.Sections.Add(CSE101Section1);
            context.Sections.Add(CSE101Section2);
            context.Sections.Add(CSE101Section3);
            context.Sections.Add(CSE101Section4);
            context.Sections.Add(CSE101Section5);
            context.Sections.Add(CSE101Section6);
            // Adding Course Object to the DB
            context.Courses.Add(CSE101);

            context.SaveChanges();

            var count = context.Sections.First().Meetings.Count;

            bool test = true;
        }
    }
}
