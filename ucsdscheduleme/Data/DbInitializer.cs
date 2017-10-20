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

            Location center = new Location() { RoomNumber = 101, Building = "Center" };
            Meeting meeting = new Meeting() { MeetingType = MeetingType.Lab, Days = Days.Friday, StartTime = 10, EndTime = 14, Location = center };
            Section section = new Section() { CourseId = 123, Meetings = new List<Meeting>() { meeting } };

            Course math = new Course() { CourseName = "CSE 100", Sections = new List<Section>() { section } };

            context.Courses.Add(math);

            context.SaveChanges();

            var course = context.Courses.First();


            bool breakpointTester = false;
        }
    }
}
