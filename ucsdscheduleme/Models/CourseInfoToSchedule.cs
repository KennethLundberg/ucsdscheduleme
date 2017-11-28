using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class CourseInfoToSchedule
    {
        public int?[] CourseIds { get; set; }
        public Optimization Optimization { get; set; }
    }
}
