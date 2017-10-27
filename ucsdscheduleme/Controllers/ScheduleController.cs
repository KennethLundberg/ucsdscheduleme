using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateSchedule(string course1, string course2, 
                                              string course3, string course4) {

            Console.WriteLine("hello world");
            Console.WriteLine("course1:" + course1);
            Console.WriteLine("course2:" + course2);
            Console.WriteLine("course3:" + course3);
            Console.WriteLine("course4:" + course4);
            //var schedule = new List<Course>();

            //var c1 = new Course { CourseName = course1 };
            //var c2 = new Course { CourseName = course2 };
            //var c3 = new Course { CourseName = course3 };
            //var c4 = new Course { CourseName = course4 };

            //Console.WriteLine("c1:" + c1.CourseName);
            //Console.WriteLine("c2:" + c2.CourseName);

            //schedule.Add(c1);
            //schedule.Add(c2);
            //schedule.Add(c3);
            //schedule.Add(c4);

            var resultList = new List<string>{
                course1,
                course2,
                course3,
                course4
            };

            ViewData["course1"] = course1;
            ViewData["course2"] = course2;
            ViewData["course3"] = course3;
            ViewData["course4"] = course4;

            ViewData["courseList"] = resultList;

            return View("Index");
        }
    }
}