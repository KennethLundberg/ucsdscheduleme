using System;
using System.Collections.Generic;
using System.Linq;
using ucsdscheduleme.Models;
using PossibleSchedules = System.Collections.Generic.List<System.Collections.Generic.List<ucsdscheduleme.Models.Section>>;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public abstract class ScheduleOptimization
    {
        public abstract List<Section> Optimize(PossibleSchedules possibleSchedules);
    }
}
