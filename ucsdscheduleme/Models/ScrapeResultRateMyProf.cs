using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class ScrapeResultRateMyProf
    {
        // Rate My Professor data(Not sure about data types!!) 
        public string InstructorName { get; set; }
        public string OverallQuality { get; set; }
        public string WouldTakeAgain { get; set; }
        public string LevelofDifficulty { get; set; }
    }
}
