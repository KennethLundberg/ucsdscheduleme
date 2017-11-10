using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;

namespace ucsdscheduleme.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
