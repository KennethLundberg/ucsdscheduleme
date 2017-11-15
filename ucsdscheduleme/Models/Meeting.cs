using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    [Flags()]
    public enum Days : byte
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,
        TBA = 128
    }
    public enum MeetingType
    {
        Lecture, Discussion, Lab, Review, Final, Midterm
    }
    public class Meeting
    {
        public int Id { get; set; }
        [StringLength(3)]
        public string Code { get; set; }
        public Days Days { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public DateTime? StartDate { get; set; }

        public MeetingType MeetingType { get; set; }
        public Location Location { get; set; }
    }
}
