using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Data;

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
        /// <param name="course1">Course1.</param>
        /// <param name="course2">Course2.</param>
        /// <param name="course3">Course3.</param>
        /// <param name="course4">Course4.</param>
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
                        Meetings = new List<Meeting>
                        {
                            new Meeting
                            {
                                Id = 101,
                                MeetingType = MeetingType.Lecture,
                                StartTime = 10,
                                EndTime = 11
                            },
                            new Meeting
                            {
                                Id = 102,
                                MeetingType = MeetingType.Discussion,
                                StartTime = 20,
                                EndTime = 21
                            }
                        }
                    },
                    new Section
                    {
                        Id = 10,
                        Meetings = new List<Meeting>
                        {
                            new Meeting
                            {
                                Id = 101,
                                MeetingType = MeetingType.Lecture,
                                StartTime = 10,
                                EndTime = 11
                            },
                            new Meeting
                            {
                                Id = 102,
                                MeetingType = MeetingType.Discussion,
                                StartTime = 20,
                                EndTime = 21
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
                        Meetings = new List<Meeting>
                        {
                            new Meeting
                            {
                                Id = 102,
                                MeetingType = MeetingType.Discussion,
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
