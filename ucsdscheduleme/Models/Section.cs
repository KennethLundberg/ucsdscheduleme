using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class Section
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ProfessorId { get; set; }

        public Professor Professor { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
    }
}
