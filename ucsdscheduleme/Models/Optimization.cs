using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public enum Optimization
    {
        HighestGPA,
        RMPRating,
        EarliestEnd,
        LatestStart,
        MostGaps,
        LeastGaps,
        MostDays,
        LeastDays
    }
}
