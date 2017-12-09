using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo.OptimizationStrategies
{
    public static class ScheduleOptimizationFactory
    {
        public static ScheduleOptimization GetOptimization(Optimization optimization)
        {
            ScheduleOptimization strategy = null;
            switch (optimization)
            {
                case Optimization.MostGaps:
                    strategy = new MostGapsOptimization();
                    break;
                case Optimization.LeastGaps:
                    strategy = new LeastGapsOptimization();
                    break;
                case Optimization.MostDays:
                    strategy = new MostDaysOptimization();
                    break;
                case Optimization.LeastDays:
                    strategy = new LeastDaysOptimization();
                    break;
                case Optimization.HighestGPA:
                    strategy = new HighestGPAOptimization();
                    break;
                case Optimization.HighestRMP:
                    strategy = new HighestRateMyProfessorOptimization();
                    break;
                case Optimization.EarlyEnd:
                    strategy = new EarliestEndOptimization();
                    break;
                case Optimization.LateStart:
                    strategy = new LatestStartOptimization();
                    break;
            }
            return strategy;
        }
    }
}