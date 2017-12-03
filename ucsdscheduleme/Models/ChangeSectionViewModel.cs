using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class ChangeSectionViewModel
    {
        public int SectionIdToRemove { get; set; }
        public int SectionIdToAdd { get; set; }
    }
}
