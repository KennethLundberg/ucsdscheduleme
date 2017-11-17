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
        public string OverallQuality { get; set; } 
        public string WouldTakeAgain { get; set; } 
        public string LevelOfDifficulty { get; set; } 
        [StringLength(256)]
        public string URL { get; set; }
    }
}
