using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;
namespace ucsdscheduleme.Controllers
{
    public class TestScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }    
        public IActionResult GenerateSchedule(string course1, string course2, string course3, string course4) {
            {
                Meeting CSE20Lecture = new Meeting()
                {
                    MeetingType = MeetingType.Lecture,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = 1400,
                    EndTime = 1520,
                    Code = "A00"
                };
                Meeting CSE20Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = 1000,
                    EndTime = 1050,
                    Code = "A02"
                };
                Meeting CSE20Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = 1711,
                    EndTime = 1712,
                    Code = "B02"
                };
                Course CSE20 = new Course()
                {
                    CourseAbbreviation = "CSE20",
                    CourseName = "Intro / Discrete Mathematics",
                    Units = 4,
                    Description = ""
                };
                Section CSE20Section1 = new Section()
                {
                    Ticket = 915105,
                    Course = CSE20
                };
                Section CSE20Section2 = new Section()
                {
                    Ticket = 915105,
                    Course = CSE20
                };
                // Adding different sections to the course object
                CSE20.Sections.Add(CSE20Section1);
                CSE20Section1.Meetings.Add(CSE20Lecture);
                CSE20Section1.Meetings.Add(CSE20Discussion1);
                CSE20Section2.Meetings.Add(CSE20Lecture);
                CSE20Section2.Meetings.Add(CSE20Discussion2);
                CSE20.Sections.Add(CSE20Section2);

                Meeting CSE21Lecture = new Meeting()
                {
                    MeetingType = MeetingType.Lecture,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = 1700,
                    EndTime = 1710,
                    Code = "A00"
                };
                Meeting CSE21Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = 900,
                    EndTime = 901,
                    Code = "A02"
                };
                Meeting CSE21Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = 903,
                    EndTime = 904,
                    Code = "A02"
                }; 
                Course CSE21 = new Course()
                {
                    CourseAbbreviation = "CSE21",
                    CourseName = "Intro / Discrete Mathematics",
                    Units = 4,
                    Description = ""
                };

                Section CSE21Section1 = new Section()
                {
                    Ticket = 915105,
                    Course = CSE21
                };
                Section CSE21Section2 = new Section()
                {
                    Ticket = 915105,
                    Course = CSE21
                }; 
                // Adding different sections to the course object
                CSE21.Sections.Add(CSE21Section1);
                CSE21.Sections.Add(CSE21Section2);
                CSE21Section1.Meetings.Add(CSE21Lecture);
                CSE21Section1.Meetings.Add(CSE21Discussion1);
                CSE21Section2.Meetings.Add(CSE21Lecture);
                CSE21Section2.Meetings.Add(CSE21Discussion2);

                Meeting CSE101Lecture = new Meeting()
                {
                    MeetingType = MeetingType.Lecture,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = 1,
                    EndTime = 2,
                    Code = "A00"
                };
                Meeting CSE101Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = 3,
                    EndTime = 4,
                    Code = "A02"
                };
                Meeting CSE101Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = 5,
                    EndTime = 6,
                    Code = "B02"
                };
                Course CSE101 = new Course()
                {
                    CourseAbbreviation = "CSE101",
                    CourseName = "ALGORITHMS",
                    Units = 4,
                    Description = ""
                };
                Section CSE101Section1 = new Section()
                {
                    Ticket = 915104,
                    Course = CSE101
                };
                Section CSE101Section2 = new Section()
                {
                    Ticket = 915103,
                    Course = CSE101
                };
                // Adding different sections to the course object
                CSE101.Sections.Add(CSE101Section1);
                CSE101Section1.Meetings.Add(CSE101Lecture);
                CSE101Section1.Meetings.Add(CSE101Discussion1);
                CSE101Section2.Meetings.Add(CSE101Lecture);
                CSE101Section2.Meetings.Add(CSE101Discussion2);
                CSE101.Sections.Add(CSE101Section2);

                ScheduleRepo repo = new ScheduleRepo();
                Course[] courses = { CSE20, CSE21, CSE101 };
                List<List<Section>> returned;
                returned = repo.FindScheduleForClasses(courses);
                Console.WriteLine("RETURNED SCHEDULE NUM: " + returned.Count);
                foreach(List<Section> l in returned) {
                    foreach(Section s in l) 
                    {
                        Console.Write(s.Course.CourseAbbreviation + " ");    
                    }
                    Console.WriteLine(" ");
                }
                return View();
            }
        }
    }
}