using System;
using System.Collections.Generic;
using System.Linq;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo
{
    public class FormatRepo
    {
        // Our calendar starts at 7:30 am, so all calculations are done relative to this time.
        private static readonly int START_HOUR = 7;
        private static readonly int START_MINUTES = 30;
        private static readonly int MINUTES_IN_HOUR = 60;

        /// <summary>
        /// Translates the schedule data from a database friendly format into a format that is more easily digested
        /// by javascript as a view object.
        /// </summary>
        /// <param name="selectedSections">The sections that make up a users current schedule</param>
        /// <returns>A populated view model to display in the home page.</returns>
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

                char selectedBaseId = ' ';

                var meetingWithCode = selectedSection.Meetings?
                                                         .FirstOrDefault(m => m.Code != null);

                selectedBaseId = meetingWithCode?.Code[0] ??
                            throw new ArgumentException($"Section with no meeting found: Id'{selectedSection.Id}'");

                CourseViewModel thisCourse = new CourseViewModel
                { 
                    CourseAbbreviation = course.CourseAbbreviation,
                    Bases = new Dictionary<char, BaseViewModel>(),
                    SelectedSection = selectedSection.Id,
                    // If our sections have no meetings, we're in trouble. Time to abort.
                    SelectedBase = selectedBaseId
                };

                model.Courses.Add(course.Id,thisCourse);

                var baseSectionGroups = course.Sections.GroupBy(s => s.Meetings.First(m => !IsOneTimeEvent(m.MeetingType)).Code[0]);
                foreach (var baseSectionGroup in baseSectionGroups)
                {
                    var baseForCourse = PopulateBaseForCourse(baseSectionGroup.ToList());
                    thisCourse.Bases.Add(baseSectionGroup.Key, baseForCourse);
                }
            }

            return model;
        }

        /// <summary>
        /// Creates a BaseViewModel populated from a list of sections in that base.
        /// </summary>
        /// <param name="sectionsForBase">List of all sections in the current base.</param>
        /// <returns>A populated base view model in the format easiest for JSON tokenization.</returns>
        private static BaseViewModel PopulateBaseForCourse(List<Section> sectionsForBase)
        {
            var course = sectionsForBase[0].Course;

            BaseViewModel thisBase = new BaseViewModel
            {
                Metadata = AddMetadata(sectionsForBase.First())              
            };

            List<OneTimeEvent> oneTimeEvents = PopulateOneTimeEvents(sectionsForBase, course);
            thisBase.OneTimeEvents = oneTimeEvents;

            Section firstSection = sectionsForBase[0];
            List<CalendarEvent> baseEvents = PopulateBaseEvents(firstSection, course);
            thisBase.BaseEvents = baseEvents;

            Dictionary<int, List<CalendarEvent>> thisBasesSectionEvents = PopulateSectionEventsForBase(sectionsForBase, course);
            thisBase.SectionEvents = thisBasesSectionEvents;

            return thisBase;
        }

        /// <summary>
        /// Populates a dictionary of all CalendarEvents for each section to be added to the BaseViewModel.
        /// </summary>
        /// <param name="sectionsForBase">All the sections contained in the current base.</param>
        /// <param name="course">The course these sections belong to.</param>
        /// <returns>A populated dictionary containing all calendar events for each section in the base.</returns>
        public static Dictionary<int, List<CalendarEvent>> PopulateSectionEventsForBase(List<Section> sectionsForBase, Course course)
        {
            Dictionary<int, List<CalendarEvent>> thisBasesSectionEvents = new Dictionary<int, List<CalendarEvent>>();
            foreach (var section in sectionsForBase)
            {
                List<CalendarEvent> thisSectionsEvents = new List<CalendarEvent>();
                var sectionSpecificMeetings = section.Meetings.Where(m => m.IsUnique && !IsOneTimeEvent(m.MeetingType));
                string professorName = section.Professor?.Name ?? "Unknown";
                foreach (var meeting in sectionSpecificMeetings)
                {
                    AddCalendarEvent(ref thisSectionsEvents, course.CourseAbbreviation, professorName, meeting);
                }
                thisBasesSectionEvents.Add(section.Id, thisSectionsEvents);
            }

            return thisBasesSectionEvents;
        }

        /// <summary>
        /// Populates all the events that are associated with every section inside of a base.
        /// </summary>
        /// <param name="firstSection">The first section to pull the list of meetings from.</param>
        /// <param name="course">The course that all these sections are a part of.</param>
        /// <returns>A populated list of all calendar events.</returns>
        public static List<CalendarEvent> PopulateBaseEvents(Section firstSection, Course course)
        {
            // If a meeting is not associated with a section, then we know that it is used
            // by all classes in this base.
            var baseMeetings = firstSection
                                    .Meetings
                                    .Where(m => !m.IsUnique && !IsOneTimeEvent(m.MeetingType));

            List<CalendarEvent> baseEvents = new List<CalendarEvent>();
            foreach (var baseMeeting in baseMeetings)
            {
                var type = baseMeeting.MeetingType;

                string professorName = firstSection.Professor?.Name ?? "Unknown";

                if (!IsOneTimeEvent(type))
                {
                    AddCalendarEvent(ref baseEvents, course.CourseAbbreviation, professorName, baseMeeting);
                }
            }

            return baseEvents;
        }

        /// <summary>
        /// Populates all the one time events for a given base, such as finals and midterms.
        /// </summary>
        /// <param name="sectionsForBase">List of all of the sections for a given base.</param>
        /// <param name="course">The course that these sections are a part of.</param>
        /// <returns>Populated List of events that only happen once.</returns>
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

        /// <summary>
        /// Tests whether or not a meeting is a one time event or not.
        /// </summary>
        /// <param name="type">The meeting type of the meeting to check.</param>
        /// <returns>True if meeting is a one time event, false otherwise.</returns>
        private static bool IsOneTimeEvent(MeetingType type)
        {
            return type == MeetingType.Final || type == MeetingType.Review || type == MeetingType.Midterm;
        }

        /// <summary>
        /// Populates the metadata for a given base from the professor and cape objects.
        /// </summary>
        /// <param name="section">The section to get the information from.</param>
        /// <returns>Populated metadata containing information for the base of the section.</returns>
        private static Metadata AddMetadata(Section section)
        {
            var capeForSection = section.Course.Cape.FirstOrDefault(ca => ca.Professor == section.Professor);

            var professor = section.Professor;
            var rateMyProfessor = professor?.RateMyProfessor;
            string professorName;

            if(professor == null)
            {
                // If this is a custom event.
                if(section.Course.User != null)
                {
                    // We don't want this custom event to create a metadata object.
                    return null;
                }
                // Otherwise we just don't know who the professor is
                else
                {
                    professorName = "Unk Professor";
                }
            }
            else
            {
                professorName = professor.Name;
            }

            string professorString = (capeForSection == null) ? $"{professorName} (Capes Unknown)" 
                                                              : professorName;
            return new Metadata
            {
                AverageGpaExpected = capeForSection?.AverageGradeExpected ?? 0M,
                AverageGpaReceived = capeForSection?.AverageGradeReceived ?? 0M,
                AverageWorkload = capeForSection?.StudyHoursPerWeek ?? 0M,
                CourseAbbreviation = section.Course.CourseAbbreviation,
                ProfessorName = professorString,
                Quality = rateMyProfessor?.OverallQuality ?? 0M,
                Difficulty = rateMyProfessor?.LevelOfDifficulty ?? 0M
            };
        }

        /// <summary>
        /// Adds a particular meeting that is a one time event to an existing list of one time events.
        /// </summary>
        /// <param name="oneTimeEvents">List of one time events that will be added to.</param>
        /// <param name="courseAbbreviation">Abbreviation for course meeting belongs to.</param>
        /// <param name="meeting">Meeting to add one time event from.</param>
        private static void AddOneTimeEvent(ref List<OneTimeEvent> oneTimeEvents, string courseAbbreviation, Meeting meeting)
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

            string time = $"{startTime} - {endTime} ";
            OneTimeEvent final = new OneTimeEvent()
            {
                CourseAbbreviation = courseAbbreviation,
                Time = time,
                Date = startDate,
                Location = $"{meeting.Location.RoomNumber} {meeting.Location.Building}",
                Type = meeting.MeetingType.ToString()
            };

            oneTimeEvents.Add(final);
        }

        /// <summary>
        /// Adds a calendar event for either a section event or base event, splitting each meeting day 
        /// into its own calendar event.
        /// </summary>
        /// <param name="events">List of existing events that this meeting's events are added to.</param>
        /// <param name="courseAbbreviation">Abbrevitaion for the course this meeting belongs to.</param>
        /// <param name="professorName">Name of professor for this event.</param>
        /// <param name="meeting">Meeting to create events from.</param>
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

            // If the start time is less than 0, this event isnt during our calendar, so skip it. 
            if(startTimeInMinutes < 0)
            {
                return;
            }

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
