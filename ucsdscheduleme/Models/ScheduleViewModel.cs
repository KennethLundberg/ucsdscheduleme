using System;
using System.Collections.Generic;

namespace ucsdscheduleme.Models
{
    public class ScheduleViewModel
    {
        public List<Course> CourseList { get; set; } = new List<Course>();

        public static implicit operator ScheduleViewModel(List<Section> v)
        {
            throw new NotImplementedException();
        }
    }
}
