using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public enum Optimization
    {
        MostGaps,
        LeastGaps,
        MostDays,
        LeastDays,
        HighestGPA,
        HighestRMP,
        EarlyEnd,
        LateStart
    }
}
