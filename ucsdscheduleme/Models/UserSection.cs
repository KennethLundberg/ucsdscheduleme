using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class UserSection
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SectionId { get; set; }

        public Section Section { get; set; }
        public ApplicationUser User { get; set; }
    }
}
