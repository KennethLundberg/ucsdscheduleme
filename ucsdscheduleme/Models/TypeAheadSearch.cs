using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class TypeAheadSearch
    {
        public string Input { get; set; }
        public int?[] AlreadyAddedCourses { get; set; }
    }
}
