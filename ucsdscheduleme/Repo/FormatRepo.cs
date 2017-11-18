using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo
{
    public class FormatRepo
    {
        private static readonly int START_HOUR = 7;
        private static readonly int START_MINUTES = 30;
        private static readonly int MINUTES_IN_HOUR = 60;

        public static ScheduleViewModel FormatSectionsToCalendarEvent(List<Section> sections)
        {

            ScheduleViewModel model = new ScheduleViewModel();
            List<CalendarEvent> events = new List<CalendarEvent>();
            model.Events = events;

            foreach(Section section in sections)
            {
                string courseAbbreviation = section.Course.CourseAbbreviation;
                string professorName = section.Professor.Name;
                string sectionCode = "";
                foreach(Meeting meeting in section.Meetings)
                {
                    string type = meeting.MeetingType.ToString();

                    DateTime start = meeting.StartTime;

                    // Formats it into the form: 12:00am
                    string startString = start.ToString("h:mmtt");
                    string endString = meeting.EndTime.ToString("h:mmtt");

                    int durationInMinutes = (int) meeting.EndTime.Subtract(start).TotalMinutes;
                    int startTimeInMinutes = (start.Hour - START_HOUR) * MINUTES_IN_HOUR + start.Minute - START_MINUTES;

                    var days = Enum.GetValues(typeof(Days))
                        .Cast<Days>()
                        .Where(s => meeting.Days.HasFlag(s));

                    foreach (var day in days)
                    {
                        CalendarEvent calendarEvent = new CalendarEvent
                        {
                            CourseAbbreviation = courseAbbreviation,
                            Day = day.ToString(),
                            SectionCode = sectionCode,
                            Type = type,
                            ProfessorName = professorName,
                            Timespan = $"{startString} - {endString}",
                            DurationInMinutes = durationInMinutes,
                            StartTimeInMinutesAfterFirstHour = startTimeInMinutes
                        };

                        events.Add(calendarEvent);
                    }
                }
            }

            return model;
        }
    }
}
