using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class Professor
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public RateMyProfessor RateMyProfessor { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<Cape> Cape { get; set; } = new List<Cape>();
    }
}
