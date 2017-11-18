using System;
using System.Collections.Generic;

namespace ucsdscheduleme.Models
{
    public class ScheduleViewModel
    {
        public Metadata OverallMetadata { get; set; }
        public List<Metadata> ClassMetadata { get; set; } = new List<Metadata>();
        public List<Section> SectionList { get; set; } = new List<Section>();
    }

    public struct Metadata
    {
        public decimal AverageGpaExpected { get; set; }
        public decimal AverageGpaReceived { get; set; }
        public decimal AverageTotalWorkload { get; set; }
        public string CourseAbbreviation { get; set; }
        public string ProfessorName { get; set; }
    }
}
