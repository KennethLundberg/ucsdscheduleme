using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class Cape
    {
        public int Id { get; set; }
        [StringLength(4)]
        public string Term { get; set; }
        public int StudentsEnrolled { get; set; }
        public int NumberOfEvaluation { get; set; }
        public decimal RecommendedClass { get; set; } // Percentage
        public decimal RecommendedProfessor { get; set; } // Percentage
        public decimal StudyHoursPerWeek { get; set; }
        public string GradeExpected { get; set; } // Has grade and GPA sometimes
        public string GradeReceived { get; set; } // Has grade and GPA somtimes, and sometimes just N/A
        public string URL { get; set; }
    }
}
