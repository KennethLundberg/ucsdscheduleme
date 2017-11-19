using System;
using System.Collections.Generic;

namespace ucsdscheduleme.Models
{
    public class ScheduleViewModel
    {
        public Metadata OverallMetadata { get; set; }
        public List<Metadata> ClassMetadata { get; set; } = new List<Metadata>();
        public List<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();
        public List<OneTimeEvent> OneTimeEvents { get; set; }
    }

    public class NewScheduleViewModel
    {
        public Metadata OverallMetadata { get; set; }
        public Dictionary<string, CourseViewModel> Courses { get; set; }
    }

    public class CourseViewModel
    {
        public string SelectedBase { get; set; }
        public string SelectedSection { get; set; }
        public Dictionary<string, BaseViewModel> Bases { get; set; }
        public List<OneTimeEvent> OneTimeEvents { get; set; }
    }

    public class BaseViewModel
    {
        public List<CalendarEvent> BaseElements { get; set; }
        public Dictionary<string, CalendarEvent> SectionElements { get; set; }
        public List<Metadata> Metadata { get; set; }
    }

    public class OneTimeEvent
    {
        public string CourseAbbreviation { get; set; }
        public string DateAndTime { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
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
