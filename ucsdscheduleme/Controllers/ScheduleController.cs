using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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


            // Used to return a list courses. 
            var schedule = new ScheduleViewModel();

            // TODO Retrieve course object from database based on course name.
            // For now, just sort and create new Course object for each string 
            // passed in.
            var list = new List<string>
            {
                course1, course2, course3, course4
            };
            list.Sort();

            var c1 = new Course 
            { 
                Id = 1,
                CourseName = list[0],
                Units = 4,
                Sections = new List<Section> {
                    new Section
                    {
                        Id = 10,
                        CourseId = 1,
                        ProfessorId = 100,
                        Meetings = new List<Meeting>
                        {
                            new Meeting
                            {
                                Id = 101,
                                SectionId = 10,
                                StartTime = 10,
                                EndTime = 11
                            }
                        }
                    }
                }
            };

            var c2 = new Course
            {
                Id = 2,
                CourseName = list[1],
                Units = 4,
                Sections = new List<Section> {
                    new Section
                    {
                        Id = 10,
                        CourseId = 2,
                        ProfessorId = 100,
                        Meetings = new List<Meeting>
                        {
                            new Meeting
                            {
                                Id = 102,
                                SectionId = 10,
                                StartTime = 9,
                                EndTime = 11
                            }
                        }
                    }
                }
            };

            var c3 = new Course { CourseName = list[2] };
            var c4 = new Course { CourseName = list[3] };

            // TODO Call schedule algorithm on four classes.


            schedule.CourseList.Add(c1);
            schedule.CourseList.Add(c2);
            schedule.CourseList.Add(c3);
            schedule.CourseList.Add(c4);

            return View(nameof(Index), schedule);
        }
    }
}