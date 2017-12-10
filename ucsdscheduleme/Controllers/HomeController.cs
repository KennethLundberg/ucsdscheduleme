using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;
using ucsdscheduleme.Data;
using Microsoft.AspNetCore.Identity;
using PossibleSchedules = System.Collections.Generic.List<System.Collections.Generic.List<ucsdscheduleme.Models.Section>>;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using ucsdscheduleme.Repo.OptimizationStrategies;

namespace ucsdscheduleme.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ScheduleContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ScheduleContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            UserScheduleViewModel model = new UserScheduleViewModel();

            // Find our user with the auth token.
            var user = _userManager.GetUserAsync(User).Result;

            // Grab schedule from db.
            var sectionIds = _context.UserSections
                                    .Where(us => us.User == user)
                                    .Select(us => us.SectionId);

            // If we have no courses to show, why don't we just stop here?
            if(sectionIds.Count() == 0)
            {
                var emptyReturn = new UserScheduleViewModel
                {
                    CourseListItems = new List<CourseListItemViewModel>(),
                    ScheduleViewModel = new ScheduleViewModel()
                };
                return View(emptyReturn);
            }

            var schedule = _context.Sections
                                   .Include(s => s.Course)
                                        .ThenInclude(c => c.User)
                                   .Include(s => s.Course)
                                        .ThenInclude(c => c.Sections)
                                            .ThenInclude(s => s.Professor)
                                   .Include(s => s.Course)
                                        .ThenInclude(c => c.Sections)
                                            .ThenInclude(s => s.Meetings)
                                                .ThenInclude(m => m.Location)
                                   .Include(s => s.Course)
                                        .ThenInclude(c => c.Cape)
                                            .ThenInclude(ca => ca.Professor)
                                                .ThenInclude(p => p.RateMyProfessor)
                                   
                                   .Where(s => sectionIds.Contains(s.Id))
                                   .ToList();

            // Populate model with schedule info.
            if (schedule != null)
            {
                model.ScheduleViewModel = FormatRepo.FormatSectionsToCalendarEvent(schedule);
                model.CourseListItems = schedule.Select(s => new CourseListItemViewModel
                    {
                        CourseId = s.Course.Id,
                        CourseAbbreviation = s.Course.CourseAbbreviation,
                        IsCustomEvent = s.Course.User != null
                    }).ToList();
            }
            // We shouldn't ever get here, because if a user has a user section, then schedule shouldn't be null.
            else
            {
                model.ScheduleViewModel = new ScheduleViewModel();
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public void ChangeScheduleSection([FromBody] ChangeSectionViewModel sections)
        {
            var user = _userManager.GetUserAsync(User).Result;

            var sectionToRemove = _context.UserSections.FirstOrDefault(us => us.User == user && us.SectionId == sections.SectionIdToRemove);
            if(sectionToRemove != null)
            {
                _context.Remove(sectionToRemove);
            }
            if(_context.Sections.Any(s => s.Id == sections.SectionIdToAdd))
            {
                _context.UserSections.Add(new UserSection { SectionId = sections.SectionIdToAdd, User = user });
            }
            _context.SaveChanges();
        }

        [HttpPost]
        public IActionResult GenerateSchedule([FromBody] CourseInfoToSchedule courseInfo)
        {
            // Get the current user.
            var user = _userManager.GetUserAsync(User).Result;

            // Check if there are no course ids, if there arn't remove all courses from schedule.
            if (courseInfo.CourseIds?.Length == 0)
            {
                // Remove all user sections for this user.
                var sectionsForUser = _context.UserSections.Where(us => us.UserId == user.Id).ToList();
                _context.UserSections.RemoveRange(sectionsForUser ?? new List<UserSection>());
                _context.SaveChanges();
                return Json(new ScheduleViewModel() {Error = "Please add a class to generate a schedule." });
            }

            Course[] courses = _context.Courses
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Professor)
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Meetings)
                        .ThenInclude(m => m.Location)
                .Include(c => c.Cape)
                    .ThenInclude(ca => ca.Professor)
                        .ThenInclude(p => p.RateMyProfessor)
                .Where(c => courseInfo.CourseIds.Contains(c.Id)).ToArray();
            Optimization optimization = courseInfo.Optimization;

            var scheduleRepo = new ScheduleRepo();

            // Call the schedule finding algorithm.
            PossibleSchedules schedules = scheduleRepo.FindScheduleForClasses(courses);

            if (!schedules.Any())
            {
                // TODO: return error
                return Json(new ScheduleViewModel() { Error = "No possible schedule for given courses." });
            }


            List<Section> schedule = ScheduleOptimizationFactory.GetOptimization(optimization).Optimize(schedules);

            // Create user sections to add.
            var sectionsToAdd = schedule.Select(s => new UserSection { Section = s, User = user });           
            // Get the schedule sections for this user.
            var sectionsToRemove = _context.UserSections.Where(us => us.User == user);
            // Remove old sections.
            _context.RemoveRange(sectionsToRemove);
            // Add new ones.
            _context.AddRange(sectionsToAdd);
            _context.SaveChanges();

            ScheduleViewModel model = FormatRepo.FormatSectionsToCalendarEvent(schedule);
            model.Error = "";

            return Json(model);
        }

        [HttpPost]
        public IActionResult TypeAhead([FromBody] TypeAheadSearch search)
        {
            var modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);

            var suggestedCourses = _context.Courses                          
                                           .AsNoTracking()
                                           .Where(c => c.UserId == null && c.CourseAbbreviation.ToUpper().StartsWith(search.Input.ToUpper()));

            if (search.AlreadyAddedCourses != null)
            {
                suggestedCourses = suggestedCourses.Where(s => !search.AlreadyAddedCourses.Contains(s.Id));
            }

            var suggestions = suggestedCourses.OrderBy(c => c.CourseAbbreviation)
                                              .Take(3)
                                              .Select(c => new { abbreviation = c.CourseAbbreviation, id = c.Id })
                                              .ToArray();


            return Json(suggestions);
        }

        [HttpPost]
        public IActionResult CreateCustomEvent([FromBody] CustomEventRequest data)
        {
            var modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);
            var user = _userManager.GetUserAsync(User).Result;

            // Initialize database objects
            var course = new Course() {
                CourseAbbreviation = data.Name,
                User = user
            };

            var section = new Section() {
                Course = course
            };

            var meeting = new Meeting() {
                IsUnique = true,
                MeetingType = MeetingType.CustomEvent,
                Code = "A00",
                Days = data.Days,
                StartTime = new DateTime(1, 1, 1, data.StartTime.Hour, data.StartTime.Minute, 0),
                EndTime = new DateTime(1, 1, 1, data.EndTime.Hour, data.EndTime.Minute, 0),
            };

            // Add back connections
            section.Meetings.Add(meeting);
            course.Sections.Add(section);

            var userSection = new UserSection() {
                Section = section,
                User = user,
            };

            // Add to database
            _context.Courses.Add(course);
            _context.Sections.Add(section);
            _context.Meetings.Add(meeting);
            _context.UserSections.Add(userSection);
            _context.SaveChanges();

            // Return response object
            var calendarEvents = FormatRepo.PopulateSectionEventsForBase(new List<Section> { section }, course)
                                           .Select(d => d.Value).First();

            var response = new CustomEventResponse
            {
                CourseId = course.Id,
                SectionId = section.Id,
                CourseAbbreviation = course.CourseAbbreviation,
                CalendarEvents = calendarEvents
            };

            return Json(response);
        }

        [HttpDelete]
        public void RemoveCustomEvent( int courseId )
        {
            var user = _userManager.GetUserAsync(User).Result;

            Course course = _context.Courses
                                    .Include(c => c.Sections)
                                        .ThenInclude(s => s.Meetings)
                                    .FirstOrDefault(c => c.User == user && c.Id == courseId);
            Section section = course.Sections.First();
            UserSection userSection = _context.UserSections.First(us => us.User == user && us.Section == section);

            _context.Meetings.RemoveRange(section.Meetings);
            _context.Sections.Remove(section);
            _context.UserSections.Remove(userSection);
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}
