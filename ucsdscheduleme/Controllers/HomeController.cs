using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;
using ucsdscheduleme.Data;
using Microsoft.AspNetCore.Identity;
using PossibleSchedules = System.Collections.Generic.List<System.Collections.Generic.List<ucsdscheduleme.Models.Section>>;

namespace ucsdscheduleme.Controllers
{
    //[Authorize]
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

        public IActionResult GenerateSchedule(Course[] courses, Optimization optimization)
        {
            // Used to return a list courses. 
       
            var scheduleRepo = new ScheduleRepo();

            // Call the schedule finding algorithm.
            PossibleSchedules schedules = scheduleRepo.FindScheduleForClasses(courses);

            List<Section> schedule = scheduleRepo.Optimize(optimization, schedules);

            ScheduleViewModel model = FormatRepo.FormatSectionsToCalendarEvent(schedule);

            return Json(model);
        }

        public IActionResult TypeAhead(string input)
        {
            var suggestions = _context.Courses
                                    .Where(c => c.CourseAbbreviation.Contains(input))
                                    .Take(3)
                                    .Select(c => new { abbreviation = c.CourseAbbreviation, id = c.Id })
                                    .ToArray();

            return Json(suggestions);
        }

        public IActionResult GetModelSample()
        {
            NewScheduleViewModel m = new NewScheduleViewModel
            {
                OverallMetadata = new Metadata
                {
                    AverageGpaExpected = 3.5M,
                    AverageGpaReceived = 3.4M,
                    AverageTotalWorkload = 6.7M
                }
            };

            Dictionary<string, CourseViewModel> c = new Dictionary<string, CourseViewModel>();
            m.Courses = c;

            CalendarEvent ce = new CalendarEvent()
            {
                CourseAbbreviation = "CSE110",
                Day = "Monday",
                StartTimeInMinutesAfterFirstHour = 30,
                DurationInMinutes = 60,
                ProfessorName = "John Cena"
            };

            List<CalendarEvent> be = new List<CalendarEvent>() { ce };

            Dictionary<string, CalendarEvent> se = new Dictionary<string, CalendarEvent>() { { "1234", ce } };

            BaseViewModel b1 = new BaseViewModel()
            {
                BaseElements = be,
                SectionElements = se
            };


            Dictionary<string, BaseViewModel> b = new Dictionary<string, BaseViewModel>()
            {
                { "Gary", b1 }
            };


            CourseViewModel c1 = new CourseViewModel
            {
                Bases = b,
                SelectedBase = "Gary",
                SelectedSection = "1234"
            };

            return (Json(m));
        }
    }
}
