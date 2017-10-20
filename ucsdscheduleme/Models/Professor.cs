using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
