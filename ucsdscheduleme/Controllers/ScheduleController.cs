using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Data;
using System.Linq;
using ucsdscheduleme.Repo;

namespace ucsdscheduleme.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ScheduleContext _context;

        public ScheduleController(ScheduleContext context)
        {
            _context = context;
        }
    
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Form postback that generates the schedule based on four strings passed.
        /// </summary>
        /// <returns>The four courses in sorted order</returns>
        /// <param name="course1name">name of Course 1.</param>
        /// <param name="course2name">name of Course 2.</param>
        /// <param name="course3name">name of Course 3.</param>
        /// <param name="course4name">name of Course 4.</param>
        public IActionResult GenerateSchedule(string course1name, string course2name, 
                                              string course3name, string course4name) {

            var course1 = _context.Courses
                                  .Single(c => c.CourseAbbreviation.Contains(course1name));
            var course2 = _context.Courses
                                  .Single(c => c.CourseAbbreviation.Contains(course2name));
            var course3 = _context.Courses
                                  .Single(c => c.CourseAbbreviation.Contains(course3name));
            var course4 = _context.Courses
                                  .Single(c => c.CourseAbbreviation.Contains(course4name));
            
            // Used to return a list courses. 
            var schedule = new ScheduleViewModel();

            Course[] courses = new Course[] { course1, course2, course3, course4 };

            var scheduleRepo = new ScheduleRepo();

            // Call the schedule finding algorithm.
            var data = scheduleRepo.FindScheduleForClasses(courses);
            schedule = data[0];

            return View(nameof(Index), schedule);
        }
    }
}
