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

namespace ucsdscheduleme.Controllers
{
#if !DEBUG
    [Authorize]
#endif
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
            ScheduleViewModel model;

            // Find our user with the auth token.
            var user = _userManager.GetUserAsync(User).Result;

#if DEBUG
            if (user == null)
                return View();
#endif
            // Grab schedule from db.
            List<Section> schedule = user.UserSections?.Select(us => us.Section).ToList();

            // Populate model with schedule info.
            if (schedule != null)
            {
                model = FormatRepo.FormatSectionsToCalendarEvent(schedule);
            }
            else
            {
                model = new ScheduleViewModel();
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult GenerateSchedule([FromBody] CourseInfoToSchedule courseInfo)
        {
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

            List<Section> schedule = scheduleRepo.Optimize(optimization, schedules);

            ScheduleViewModel model = FormatRepo.FormatSectionsToCalendarEvent(schedule);

            return Json(model);
        }

        [HttpPost]
        public IActionResult TypeAhead([FromBody] TypeAheadSearch search)
        {
            var modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);

            var suggestedCourses = _context.Courses.AsNoTracking()
                                      .Where(c => c.CourseAbbreviation.ToUpper().Contains(search.Input.ToUpper()));

            if (search.AlreadyAddedCourses != null)
            {
                suggestedCourses = suggestedCourses.Where(s => !search.AlreadyAddedCourses.Contains(s.Id));
            }

            var suggestions = suggestedCourses.Take(3)
                                              .Select(c => new { abbreviation = c.CourseAbbreviation, id = c.Id })
                                              .ToArray();


            return Json(suggestions);
        }

        [HttpPost]
        public IActionResult CreateCustomEvent([FromBody] CustomEvent data)
        {
            var user = _userManager.GetUserAsync(User).Result;

            Days days = 0;

            if(data.monday == true) {
                days = days & Days.Monday;
            }
            if (data.tuesday == true)
            {
                days = days & Days.Tuesday;
            }
            if (data.wednesday == true)
            {
                days = days & Days.Wednesday;
            }
            if (data.thursday == true)
            {
                days = days & Days.Thursday;
            }
            if (data.friday == true)
            {
                days = days & Days.Friday;
            }

            string[] startTokens = data.startTime.Split(':');///////////////////
            string[] endTokens = data.endTime.Split(':');

            var startHr = Int32.Parse(startTokens[0]);
            var startMin = Int32.Parse(startTokens[1]);
            var endHr = Int32.Parse(endTokens[0]);
            var endMin = Int32.Parse(endTokens[1]);

            var course = new Course() {
                CourseAbbreviation = data.name,
                UserId = user.Id,
                User = user///////////////////////////
            };

            var section = new Section() {
                Course = course
            };

            var meeting = new Meeting() {
                Days = days,
                StartTime = new DateTime(1, 1, 1, startHr, startMin, 0),
                EndTime = new DateTime(1, 1, 1, endHr, endMin, 0),
            };

            //add to db
            section.Meetings.Add(meeting);
            course.Sections.Add(section);

            var userSection = new UserSection() {
                Section = section,
                SectionId = 
                section.Id,
                User = user,
                UserId = user.Id
            };

            user.UserSections.Add(userSection);

            return View();
        }
    }
}
