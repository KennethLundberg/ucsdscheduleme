using System.Collections.Generic;
using CourseId = System.Int32;
using BaseId = System.Char;
using SectionId = System.Int32;

namespace ucsdscheduleme.Models
{
    public class ScheduleViewModel
    {
        public Metadata OverallMetadata { get; set; } = new Metadata();
        public List<Metadata> ClassMetadata { get; set; } = new List<Metadata>();
        public List<Section> SectionList { get; set; } = new List<Section>();
        public Dictionary<CourseId, CourseViewModel> Courses { get; set; }
    }

    public class CourseViewModel
    {
        public char SelectedBase { get; set; }
        public int SelectedSection { get; set; }
        public Dictionary<BaseId, BaseViewModel> Bases { get; set; }
    }

    public class BaseViewModel
    {
        public List<CalendarEvent> BaseEvents { get; set; }
        public Dictionary<SectionId, List<CalendarEvent>> SectionEvents { get; set; }
        public Metadata Metadata { get; set; }
        public List<OneTimeEvent> OneTimeEvents { get; set; }
    }

    public class OneTimeEvent
    {
        public string CourseAbbreviation { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
    }

    public class Metadata
    {
        public decimal AverageGpaExpected { get; set; }
        public decimal AverageGpaReceived { get; set; }
        public decimal AverageTotalWorkload { get; set; }
        public string CourseAbbreviation { get; set; }
        public string ProfessorName { get; set; } 
        public string FormattedGpaExpected {get {return ConvertGPAToStringFormat(AverageGpaExpected); }}
        public string FormattedGpaRecieved { get {return ConvertGPAToStringFormat(AverageGpaReceived); }}
        public int CourseId { get; set; }


        //helper function that takes GPA decimal and returns equivalent
        // letter grade in proper format.
        private static string ConvertGPAToStringFormat(decimal grade) 
        {
            string prefix; 
            if( grade >= 4.0M )
            {
                prefix = "A";
            }
            else if( grade >= 3.7M)
            {
                prefix = "A-";
            }
            else if( grade >= 3.3M)
            {
                prefix = "B+";
            }
            else if( grade >= 3.0M)
            {
                prefix = "B";
            }
            else if( grade >= 2.7M)
            {
                prefix = "B-";
            }
            else if( grade >= 2.3M)
            {
                prefix = "C+";
            }
            else if( grade >= 2.0M)
            {
                prefix = "C";
            }
            else if(grade >= 1.7M)
            {
                prefix = "C-";
            }
            else if(grade >= 1.0M)
            {
                prefix = "D";
            }
            else
            {
                prefix = "F";
            }
            //format that looks like "B+ (3.82)"
            return prefix + " (" + grade + ")";
        }
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
