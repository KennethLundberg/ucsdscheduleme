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
        //public string UserId { get; set; }
        [StringLength(10)]
        public string CourseAbbreviation { get; set; }
        [StringLength(50)]
        public string CourseName { get; set; }
        [StringLength(6)]
        public string Units { get; set; }
        public string Description { get; set; }

        //public ApplicationUser User { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<Course> PreRequisites { get; set; } = new List<Course>();
        public ICollection<Course> CoRequisites { get; set; } = new List<Course>();
        public ICollection<Cape> Cape { get; set; } = new List<Cape>();
    }
}
