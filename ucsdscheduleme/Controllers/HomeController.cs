using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;
using ucsdscheduleme.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ucsdscheduleme.Repo;

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

            ScheduleViewModel model;

            // Find our user with the auth token.
            var user = _userManager.GetUserAsync(User).Result;

            // Grab schedule from db.
            List<Section> schedule = user.UserSections.Select(us => us.Section).ToList();

            // Populate model with schedule info.
            model = FormatRepo.FormatSectionsToCalendarEvent(schedule);

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
            var data = scheduleRepo.FindScheduleForClasses(courses);

            List<Section> schedule = data[0];

            switch (optimization)
            {
                case Optimization.HighestGPA:
                    //scheduule = functioin(data);
                    break;
                case Optimization.HighestRMP:
                    //scheduule = functioin(data);
                    break;
                case Optimization.EarlyEnd:
                    //scheduule = functioin(data);
                    break;
                case Optimization.LateStart:
                    //scheduule = functioin(data);
                    break;
                case Optimization.MostDays:
                    //scheduule = functioin(data);
                    break;
                case Optimization.LeastDays:
                    //scheduule = functioin(data);
                    break;
                case Optimization.MostGaps:
                    //scheduule = functioin(data);
                    break;
                case Optimization.LeastGaps:
                    //scheduule = functioin(data);
                    break;
            }

            return Json(schedule);
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
    }
}
