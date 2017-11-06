using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class ScrapeResult
    {
        //Specifically Cape and Ratemyprofs data (at this point)
        public string InstructorName { get; set; }
        public string Term { get; set; }
        public int StudentsEnrolled { get; set; }
        public int NumberOfEvaluation { get; set; }
        public decimal RecommendedClass { get; set; }
        public decimal RecommendedProfessor { get; set; }
        public decimal StudyHoursPerWeek { get; set; }
        public string AverageGradeExpected { get; set; }
        public string AverageGradeReceived { get; set; }
        public string OverallQuality { get; set; }
        public string WouldTakeAgain { get; set; } // Percentage
        public string LevelOfDifficulty { get; set; }
    }
}
