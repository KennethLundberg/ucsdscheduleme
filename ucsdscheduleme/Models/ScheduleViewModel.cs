using System;
using System.Collections.Generic;

namespace ucsdscheduleme.Models
{
    public class ScheduleViewModel
    {
        public Metadata OverallMetadata { get; set; }
        public List<Metadata> ClassMetadata { get; set; } = new List<Metadata>();
        public List<Section> SectionList { get; set; } = new List<Section>();
        public List<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();
    }

    public struct Metadata
    {
        public decimal AverageGpaExpected { get; set; }
        public decimal AverageGpaReceived { get; set; }
        public decimal AverageTotalWorkload { get; set; }
        public string CourseAbbreviation { get; set; }
        public string ProfessorName { get; set; }
    }

    public class CalendarEvent
    {
        // Fields used to place the event in the calendar.
        public int StartTimeInMinutesAfterFirstHour { get; set; }
        public int DurationInMinutes { get; set; }
        // String to be displayed to the user in the event. Ex: "( 8:00am - 9:20am )".
        public string Timespan { get; set; }
        // String literal of the day enum
        public string Type { get; set; }
        // String literal of the Day flag
        public string Day { get; set; }
        public string CourseAbbreviation { get; set; }
        public string ProfessorName { get; set; }
        // For example "A00"
        public string SectionCode { get; set; }
    }
}
