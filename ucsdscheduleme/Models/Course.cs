using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class Course
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string CourseAbbreviation { get; set; }
        [StringLength(50)]
        public string CourseName { get; set; }
        public byte Units { get; set; }
        public string Description { get; set; }

        public ICollection<Section> Sections { get; set; }
        public ICollection<Course> PreRequisites { get; set; }
        public ICollection<Course> CoRequisites { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
