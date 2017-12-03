using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class CustomEventRequest
    {
        public string Name { get; set; }
        public Days Days { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class CustomEventResponse
    {
        public string Error { get; set; }
        public bool Success { get; set; }
        public int CourseId { get; set; }
        public int SectionId { get; set; }
        public string CourseAbbreviation { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
    }
}
