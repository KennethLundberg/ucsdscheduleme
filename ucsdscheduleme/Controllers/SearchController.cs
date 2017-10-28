using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ucsdscheduleme.Controllers
{
    public class SearchController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Searchs for classes.
        /// </summary>
        /// <returns>SearchViewModel - List of classes matching the name entered.</returns>
        /// <param name="courseName">Course. The string the user typed in</param>
        public IActionResult SearchClasses(string courseQuery)
        {
            var course1 = new Course
            {
                CourseName = "CSE 101",
                Description = "Awesome algorithms class"
            };
            var course2 = new Course
            {
                CourseName = "CSE 12",
                Description = "Intro to Algorithms and Data Structures"
            };
            var course3 = new Course
            {
                CourseName = "CSE 100",
                Description = "Advanced data structures algorithms class"
            };
            var course4 = new Course
            {
                CourseName = "CSE 20",
                Description = "Intro to Discrete math"
            };
            var course5 = new Course
            {
                CourseName = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an " +
                    "unknown printer took a galley of type and scrambled it to make a type specimen book. " +
                    "It has survived not only five centuries, but also the leap into electronic typesetting, " +
                    "remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                    "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing " +
                    "software like Aldus PageMaker including versions of Lorem Ipsum"
            };

            List<Course> courseList = new List<Course>{
                course1,
                course2,
                course3,
                course4,
                course5
            };

            // used to return search results to view
            SearchViewModel searchViewModel = new SearchViewModel();

            // TODO retrieve all classes with names containing "course" from 
            // database and add to list. For now, just use dummy data.
            foreach(Course course in courseList)
            {
                if (course.CourseName.Contains(courseQuery))
                {
                    searchViewModel.Results.Add(course);
                }
            }

            return View(nameof(Index), searchViewModel);
        }
    }
}
