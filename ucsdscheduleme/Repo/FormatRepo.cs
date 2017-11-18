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
            List<OneTimeEvent> oneTimeEvents = new List<OneTimeEvent>();
            List<Metadata> classMetadata = new List<Metadata>();
            Metadata overall = new Metadata();

            model.OneTimeEvents = oneTimeEvents;
            model.Events = events;
            model.ClassMetadata = classMetadata;
            model.OverallMetadata = overall;

            decimal numberOfCourses = model.SectionList.Count;

            foreach (Section section in sections)
            {
                string courseAbbreviation = section.Course.CourseAbbreviation;
                string professorName = section.Professor.Name;

                AddMetadata(ref classMetadata, ref overall, section, numberOfCourses);

                foreach(Meeting meeting in section.Meetings)
                {
                    MeetingType type = meeting.MeetingType;

                    if (type == MeetingType.Final || type == MeetingType.Review || type == MeetingType.Midterm)
                    {
                        AddOneTimeEvent(ref oneTimeEvents, courseAbbreviation, meeting);
                    }
                    else
                    {
                        AddCalendarEvent(ref events, courseAbbreviation, professorName, meeting);
                    }
                }
            }

            return model;
        }

        private static void AddMetadata(ref List<Metadata> classMetadata, ref Metadata overall, Section section, decimal numberOfCourses)
        {
            var capeForSection = section.Course.Cape.First(ca => ca.Professor == section.Professor);

            overall.AverageGpaExpected += capeForSection.AverageGradeExpected / numberOfCourses;
            overall.AverageGpaReceived += capeForSection.AverageGradeReceived / numberOfCourses;
            overall.AverageTotalWorkload += capeForSection.StudyHoursPerWeek;

            classMetadata.Add(new Metadata
            {
                AverageGpaExpected = capeForSection.AverageGradeExpected,
                AverageGpaReceived = capeForSection.AverageGradeReceived,
                AverageTotalWorkload = capeForSection.StudyHoursPerWeek,
                CourseAbbreviation = section.Course.CourseAbbreviation,
                ProfessorName = section.Professor.Name
            });
        }

        private static void AddOneTimeEvent(ref List<OneTimeEvent> oneTimeEvent, string courseAbbreviation, Meeting meeting)
        {
            // Make sure start date is known.
            string startDate;
            if (meeting.StartDate != null)
            {
                startDate = ((DateTime) meeting.StartDate).ToString("ddd MM/dd/yy");
            }
            else
            {
                startDate = "Unknown Date";
            }

            // Make sure start time is known.
            string startTime;
            if (meeting.StartTime == new DateTime())
            {
                startTime = "No Start Time";
            }
            else
            {
                startTime = meeting.StartTime.ToString("hh:mmtt");
            }

            // Make sure end date is known.
            string endTime;
            if (meeting.EndTime == new DateTime())
            {
                endTime = "No End Time";
            }
            else
            {
                endTime = meeting.EndTime.ToString("hh:mmtt");
            }

            string start = $"{startDate} {startTime} - {endTime} ";
            OneTimeEvent final = new OneTimeEvent()
            {
                CourseAbbreviation = courseAbbreviation,
                DateAndTime = start,
                Location = $"{meeting.Location.RoomNumber} {meeting.Location.Building}",
                Type = meeting.MeetingType.ToString()
            };

            oneTimeEvent.Add(final);
        }

        private static void AddCalendarEvent(ref List<CalendarEvent> events, string courseAbbreviation, string professorName, Meeting meeting)
        {
            DateTime start = meeting.StartTime;
            string sectionCode = meeting.Code;
            MeetingType type = meeting.MeetingType;

            // Formats it into the form: 12:00am
            string startString = start.ToString("h:mmtt");
            string endString = meeting.EndTime.ToString("h:mmtt");

            int durationInMinutes = (int)meeting.EndTime.Subtract(start).TotalMinutes;
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
                    Type = type.ToString(),
                    ProfessorName = professorName,
                    Timespan = $"{startString} - {endString}",
                    DurationInMinutes = durationInMinutes,
                    StartTimeInMinutesAfterFirstHour = startTimeInMinutes
                };

                events.Add(calendarEvent);
            }
        }
    }
}
