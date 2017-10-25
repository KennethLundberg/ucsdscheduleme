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
        Saturday = 64
    }
    public enum MeetingType
    {
        Lecture, Discussion, Lab, Review, Final
    }
    public class Meeting
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        [StringLength(3)]
        public string number { get; set; }
        public Days Days { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public MeetingType MeetingType { get; set; }
        public Location Location { get; set; }
    }
}
