using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class RateMyProfessor
    {
        public int Id { get; set; }
        public string OverallQuality { get; set; } // <------------ Changed This
        public string WouldTakeAgain { get; set; } // <------------ Changed This
        public string LevelOfDifficulty { get; set; } // <------------ Changed This
        [StringLength(256)]
        public string URL { get; set; }
    }
}
