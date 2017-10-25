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
        [StringLength(3)]
        public string Code { get; set; }
        public Days Days { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get
            {
                if(MeetingType == MeetingType.Final)
                {
                    return _startDate;
                }
                else
                {
                    throw new Exception();
                }
            }
            set
            {
                if(MeetingType == MeetingType.Final)
                {
                    _startDate = value;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public Section Section { get; set; }
        public MeetingType MeetingType { get; set; }
        public Location Location { get; set; }
    }
}
