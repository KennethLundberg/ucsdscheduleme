using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class CustomEvent
    {
        public string name { get; set; }
        public bool monday { get; set; }
        public bool tuesday { get; set; }
        public bool wednesday { get; set; }
        public bool thursday { get; set; }
        public bool friday { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
