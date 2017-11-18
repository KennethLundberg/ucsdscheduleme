using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

            ScheduleViewModel model = new ScheduleViewModel();

            // Find our user with the auth token
            var user = _userManager.GetUserAsync(User).Result;

            model.SectionList = user.UserSections.Select(us => us.Section).ToList();

            Metadata overallMetadata = model.OverallMetadata;

            decimal numberOfCourses = model.SectionList.Count;

            foreach (var section in model.SectionList)
            {
                var capeForSection = section.Course.Cape.First(ca => ca.Professor == section.Professor);

                overallMetadata.AverageGpaExpected += capeForSection.AverageGradeExpected / numberOfCourses;
                overallMetadata.AverageGpaReceived += capeForSection.AverageGradeReceived / numberOfCourses;
                overallMetadata.AverageTotalWorkload += capeForSection.StudyHoursPerWeek;

                model.ClassMetadata.Add(new Metadata
                {
                    AverageGpaExpected = capeForSection.AverageGradeExpected,
                    AverageGpaReceived = capeForSection.AverageGradeReceived,
                    AverageTotalWorkload = capeForSection.StudyHoursPerWeek,
                    CourseAbbreviation = section.Course.CourseAbbreviation,
                    ProfessorName = section.Professor.Name
                });
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
