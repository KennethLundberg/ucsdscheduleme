using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class RateMyProfessor
    {
        public int Id { get; set; }
        public decimal OverallQuality { get; set; }
        public int WouldTakeAgain { get; set; } // Percentage
        public decimal LevelOfDifficulty { get; set; }
        public string URL { get; set; }
    }
}
