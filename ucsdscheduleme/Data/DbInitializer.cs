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
            Meeting lecture1 = new Meeting()
            {
                MeetingType = MeetingType.Lecture,
                Days = (Days.Tuesday | Days.Thursday),
                StartTime = 1400,
                EndTime = 1520,
                Location = centr115,
                number = "A00"
                //Section
            };
            Meeting discussion1 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1000, EndTime = 1050, 
                Location = pcynh120,
                number = "A02"
                //Section
            };
            Meeting discussion2 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1100,
                EndTime = 1150, 
                Location = pcynh120,
                number = "A03"
                //Section
            };
            Meeting discussion3 = new Meeting()
            {
                MeetingType = MeetingType.Discussion,
                Days = Days.Wednesday,
                StartTime = 1200,
                EndTime = 1250, 
                Location = pcynh120,
                number = "A04"
                //Section
            };
            Meeting review1 = new Meeting()
            {
                MeetingType = MeetingType.Review,
                Days = Days.Sunday,
                StartTime = 1000,
                EndTime = 1150, 
                Location = centr101
                //Section
            };
            Meeting final = new Meeting()
            {
                MeetingType = MeetingType.Final,
                Days = Days.Saturday,
                StartTime = 1130,
                EndTime = 1429, 
                Location = tba
                //Section
            };

            context.Meetings.Add(lecture1);
            context.Meetings.Add(discussion1);
            context.Meetings.Add(discussion2);
            context.Meetings.Add(discussion3);
            context.Meetings.Add(review1);
            context.Meetings.Add(final);

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
                    lecture1, discussion1, discussion2, discussion3, review1, final
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


            // Adding data into the Evaluation Table
            Evaluation CSE20Eval = new Evaluation()
            {
                Course = CSE20,
                Professor = prof
                // Cape
                // RateMyProfessor
            };

        }
    }
}
