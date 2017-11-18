using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;
using ucsdscheduleme.Data;

namespace ucsdscheduleme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScheduleContext _context;

        public HomeController(ScheduleContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            ScheduleViewModel model = new ScheduleViewModel();
            /*
            {
                // This eventually will be replaced with a call to get the User object from the db
                // and pulling the section info from their user sections.
                SectionList = _context.UserSections.Select(us => us.Section).ToList(),

                OverallMetadata = new Metadata()
            };
            */

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
    }
}
