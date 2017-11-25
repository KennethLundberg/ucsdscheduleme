using System;
using System.Collections.Generic;
using System.Linq;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo
{
    public class FormatRepo
    {
        private static readonly int START_HOUR = 7;
        private static readonly int START_MINUTES = 30;
        private static readonly int MINUTES_IN_HOUR = 60;

        public static ScheduleViewModel FormatSectionsToCalendarEvent(List<Section> selectedSections)
        {
            ScheduleViewModel model = new ScheduleViewModel
            {
                Courses = new Dictionary<int, CourseViewModel>()
            };
            var coursesSectionPairs = selectedSections.ToDictionary(s => s,s => s.Course);

            foreach (var courseSection in coursesSectionPairs)
            {
                var course = courseSection.Value;
                var selectedSection = courseSection.Key;
                CourseViewModel thisCourse = new CourseViewModel
                {
                    Bases = new Dictionary<char, BaseViewModel>(),
                    SelectedSection = selectedSection.Id,
                    // If our sections have no meetings, we're in trouble. Time to abort.
                    SelectedBase = selectedSection.Meetings?.First().Code[0] ?? 
                        throw new ArgumentException($"Section with no meeting found: Id'{selectedSection.Id}'")
                };

                model.Courses.Add(course.Id,thisCourse);

                var baseSectionGroups = course.Sections.GroupBy(s => s.Meetings.First().Code[0]);
                foreach (var baseSectionGroup in baseSectionGroups)
                {
                    var baseForCourse = PopulateBaseForCourse(selectedSection, baseSectionGroup.ToList());
                    thisCourse.Bases.Add(baseSectionGroup.Key, baseForCourse);
                }
            }

            return model;
        }

        private static BaseViewModel PopulateBaseForCourse(Section selectedSection, List<Section> sectionsForBase)
        {
            var course = selectedSection.Course;

            BaseViewModel thisBase = new BaseViewModel
            {
                Metadata = AddMetadata(sectionsForBase.First())
            };

            List<OneTimeEvent> oneTimeEvents = PopulateOneTimeEvents(sectionsForBase, course);
            thisBase.OneTimeEvents = oneTimeEvents;

            List<CalendarEvent> baseEvents = PopulateBaseEvents(selectedSection, sectionsForBase, course);
            thisBase.BaseEvents = baseEvents;

            Dictionary<int, List<CalendarEvent>> thisBasesSectionEvents = PopulateSectionEventsForBase(sectionsForBase, course);
            thisBase.SectionEvents = thisBasesSectionEvents;

            return thisBase;
        }

        private static Dictionary<int, List<CalendarEvent>> PopulateSectionEventsForBase(List<Section> sectionsForBase, Course course)
        {
            Dictionary<int, List<CalendarEvent>> thisBasesSectionEvents = new Dictionary<int, List<CalendarEvent>>();
            foreach (var section in sectionsForBase)
            {
                List<CalendarEvent> thisSectionsEvents = new List<CalendarEvent>();
                var sectionSpecificMeetings = section.Meetings.Where(m => m.SectionId != null);
                foreach (var meeting in sectionSpecificMeetings)
                {
                    AddCalendarEvent(ref thisSectionsEvents, course.CourseAbbreviation, section.Professor.Name, meeting);
                }
                thisBasesSectionEvents.Add(section.Id, thisSectionsEvents);
            }

            return thisBasesSectionEvents;
        }

        private static List<CalendarEvent> PopulateBaseEvents(Section selectedSection, List<Section> sectionsForBase, Course course)
        {
            var baseMeetings = sectionsForBase.First()
                                               .Meetings
                                               .Where(m => m.SectionId == null);

            List<CalendarEvent> baseEvents = new List<CalendarEvent>();
            foreach (var baseMeeting in baseMeetings)
            {
                var type = baseMeeting.MeetingType;

                if (!IsOneTimeEvent(type))
                {
                    AddCalendarEvent(ref baseEvents, course.CourseAbbreviation, selectedSection.Professor.Name, baseMeeting);
                }
            }

            return baseEvents;
        }

        private static List<OneTimeEvent> PopulateOneTimeEvents(List<Section> sectionsForBase, Course course)
        {
            var oneTimeMeetings = sectionsForBase.First()
                                                  .Meetings
                                                  .Where(m => IsOneTimeEvent(m.MeetingType));

            List<OneTimeEvent> oneTimeEvents = new List<OneTimeEvent>();
            foreach (var oneTimeMeeting in oneTimeMeetings)
            {
                AddOneTimeEvent(ref oneTimeEvents, course.CourseAbbreviation, oneTimeMeeting);
            }

            return oneTimeEvents;
        }

        private static bool IsOneTimeEvent(MeetingType type)
        {
            return type == MeetingType.Final || type == MeetingType.Review || type == MeetingType.Midterm;
        }

        private static Metadata AddMetadata(Section section)
        {
            var capeForSection = section.Course.Cape.First(ca => ca.Professor == section.Professor);

            return new Metadata
            {
                AverageGpaExpected = capeForSection.AverageGradeExpected,
                AverageGpaReceived = capeForSection.AverageGradeReceived,
                AverageTotalWorkload = capeForSection.StudyHoursPerWeek,
                CourseAbbreviation = section.Course.CourseAbbreviation,
                ProfessorName = section.Professor.Name
            };
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
