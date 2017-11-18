using System;
using System.Collections.Generic;

namespace ucsdscheduleme.Models
{
    public class ScheduleViewModel
    {
        public Metadata OverallMetadata { get; set; } = new Metadata();
        public List<Metadata> ClassMetadata { get; set; } = new List<Metadata>();
        public List<Section> SectionList { get; set; } = new List<Section>();
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
}
