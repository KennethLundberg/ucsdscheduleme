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
        public int ProfessorId { get; set; }
        public int CourseId { get; set; }
        [StringLength(4)]
        public string Term { get; set; }
        public int StudentsEnrolled { get; set; }
        public int NumberOfEvaluation { get; set; }
        public decimal RecommendedClass { get; set; } 
        public decimal RecommendedProfessor { get; set; } 
        public decimal StudyHoursPerWeek { get; set; }
        public decimal AverageGradeExpected { get; set; }
        public decimal AverageGradeReceived { get; set; }
        [StringLength(256)]
        public string URL { get; set; }

        public Professor Professor { get; set; }
        public Course Course { get; set; }
    }
}
