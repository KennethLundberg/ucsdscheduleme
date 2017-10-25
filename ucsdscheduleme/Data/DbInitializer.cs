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

            /*
            Location center = new Location() { RoomNumber = 101, Building = "Center" };
            Meeting meeting = new Meeting() { MeetingType = MeetingType.Lab, Days = Days.Friday, StartTime = 10, EndTime = 14, Location = center };
            Section section = new Section() { CourseId = 123, Meetings = new List<Meeting>() { meeting } };

            Course math = new Course() { CourseName = "CSE 100", Sections = new List<Section>() { section } };

            context.Courses.Add(math);

            context.SaveChanges();

            //var course = context.Courses.First();

            //bool breakpointTester = false;
            */

            Location centr115 = new Location() { RoomNumber = 115, 
                    Building = "Center" };
            Location pcynh120 = new Location() { RoomNumber = 120, 
                    Building = "Pepper Canyon Hall" };
            Location centr101 = new Location() { RoomNumber = 101, 
                    Building = "Center" };
            Location centr105 = new Location() { RoomNumber = 105, 
                    Building = "Center" };
            Location wlh2207 = new Location() { RoomNumber = 2207, 
                    Building = "Warren Lecture Hall" };
            Location tba = new Location() { RoomNumber = 0, Building = "TBA" };

            context.Locations.Add(centr115);
            context.Locations.Add(pcynh120);
            context.Locations.Add(centr101);
            context.Locations.Add(centr105);
            context.Locations.Add(wlh2207);
            context.Locations.Add(tba);

            context.SaveChanges();

            Meeting lecture1 = new Meeting() { MeetingType = MeetingType.Lecture,
                    Days = Days.Tuesday | Days.Thursday, StartTime = 1400,
                    EndTime = 1520, Location = centr115, number = "A00" };
            Meeting discussion1 = new Meeting() { MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday, StartTime = 1000, EndTime = 1050, 
                    Location = pcynh120, number = "A02" };
            Meeting discussion2 = new Meeting() { MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday, StartTime = 1100, EndTime = 1150, 
                    Location = pcynh120, number = "A03" };
            Meeting discussion3 = new Meeting() { MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday, StartTime = 1200, EndTime = 1250, 
                    Location = pcynh120, number = "A04" };
            Meeting review1 = new Meeting() { MeetingType = MeetingType.Review,
                    Days = Days.Sunday, StartTime = 1000, EndTime = 1150, 
                    Location = centr101 };
            Meeting final = new Meeting() { MeetingType = MeetingType.Final,
                    Days = Days.Saturday, StartTime = 1130, EndTime = 1429, 
                    Location = tba };

            context.Meetings.Add(lecture1);
            context.Meetings.Add(discussion1);
            context.Meetings.Add(discussion2);
            context.Meetings.Add(discussion3);
            context.Meetings.Add(review1);
            context.Meetings.Add(final);

            context.SaveChanges();

            Professor prof = new Professor() { Name = "Minnes Kemp, Mor Mia" };

            context.Professor.Add();

            context.SaveChanges();

        }
    }
}
