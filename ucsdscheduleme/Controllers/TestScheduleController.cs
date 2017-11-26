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
                    StartTime = new DateTime(0, 0, 0, 14, 0, 0),
                    EndTime = new DateTime(0, 0, 0, 15, 20, 0),
                    Code = "A00"
                };
                Meeting CSE20Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = new DateTime(0, 0, 0, 10, 0, 0),
                    EndTime = new DateTime(0, 0, 0, 10, 50, 0),
                    Code = "A02"
                };
                Meeting CSE20Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = new DateTime(0, 0, 0, 17, 11, 0),
                    EndTime = new DateTime(0, 0, 0, 17, 12, 0),
                    Code = "B02"
                };
                Course CSE20 = new Course()
                {
                    CourseAbbreviation = "CSE20",
                    CourseName = "Intro / Discrete Mathematics",
                    Units = "4",
                    Description = ""
                };
                Section CSE20Section1 = new Section()
                {
                    Ticket = 201,
                    Course = CSE20
                };
                Section CSE20Section2 = new Section()
                {
                    Ticket = 202,
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
                    StartTime = new DateTime(0, 0, 0, 17, 0, 0),
                    EndTime = new DateTime(0, 0, 0, 17, 10, 0),
                    Code = "A00"
                };
                Meeting CSE21Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = new DateTime(0, 0, 0, 9, 0, 0),
                    EndTime = new DateTime(0, 0, 0, 9, 1, 0),
                    Code = "A02"
                };
                Meeting CSE21Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = new DateTime(0, 0, 0, 9, 3, 0),
                    EndTime = new DateTime(0, 0, 0, 9, 4, 0),
                    Code = "A02"
                }; 
                Course CSE21 = new Course()
                {
                    CourseAbbreviation = "CSE21",
                    CourseName = "Intro / Discrete Mathematics",
                    Units = "4",
                    Description = ""
                };

                Section CSE21Section1 = new Section()
                {
                    Ticket = 211,
                    Course = CSE21
                };
                Section CSE21Section2 = new Section()
                {
                    Ticket = 212,
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
                    StartTime = new DateTime(0, 0, 0, 0, 1, 0),
                    EndTime = new DateTime(0, 0, 0, 0, 2, 0),
                    Code = "A00"
                };
                Meeting CSE101Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = new DateTime(0, 0, 0, 0, 3, 0),
                    EndTime = new DateTime(0, 0, 0, 0, 4, 0),
                    Code = "A02"
                };
                Meeting CSE101Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = new DateTime(0, 0, 0, 0, 5, 0),
                    EndTime = new DateTime(0, 0, 0, 0, 6, 0),
                    Code = "B02"
                };
                Course CSE101 = new Course()
                {
                    CourseAbbreviation = "CSE101",
                    CourseName = "ALGORITHMS",
                    Units = "4",
                    Description = ""
                };
                Section CSE101Section1 = new Section()
                {
                    Ticket = 1011,
                    Course = CSE101
                };
                Section CSE101Section2 = new Section()
                {
                    Ticket = 1012,
                    Course = CSE101
                };
                // Adding different sections to the course object
                CSE101.Sections.Add(CSE101Section1);
                CSE101Section1.Meetings.Add(CSE101Lecture);
                CSE101Section1.Meetings.Add(CSE101Discussion1);
                CSE101Section2.Meetings.Add(CSE101Lecture);
                CSE101Section2.Meetings.Add(CSE101Discussion2);
                CSE101.Sections.Add(CSE101Section2);


                ////////
                Meeting CSE110Lecture = new Meeting()
                {
                    MeetingType = MeetingType.Lecture,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = new DateTime(0, 0, 0, 21, 0, 0),
                    EndTime = new DateTime(0, 0, 0, 21, 1, 0),
                    Code = "A00"
                };
                Meeting CSE110Discussion1 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = Days.Wednesday,
                    StartTime = new DateTime(0, 0, 0, 21, 2, 0),
                    EndTime = new DateTime(0, 0, 0, 21, 3, 0),
                    Code = "A02"
                };
                Meeting CSE110Discussion2 = new Meeting()
                {
                    MeetingType = MeetingType.Discussion,
                    Days = (Days.Tuesday | Days.Thursday),
                    StartTime = new DateTime(0, 0, 0, 21, 4, 0),
                    EndTime = new DateTime(0, 0, 0, 21, 5, 0),
                    Code = "B02"
                };
                Course CSE110 = new Course()
                {
                    CourseAbbreviation = "CSE110",
                    CourseName = "SOFTWARE ENGINEERING",
                    Units = "4",
                    Description = ""
                };
                Section CSE110Section1 = new Section()
                {
                    Ticket = 1101,
                    Course = CSE110
                };
                Section CSE110Section2 = new Section()
                {
                    Ticket = 1102,
                    Course = CSE110
                };
                // Adding different sections to the course object
                CSE110.Sections.Add(CSE110Section1);
                CSE110Section1.Meetings.Add(CSE110Lecture);
                CSE110Section1.Meetings.Add(CSE110Discussion1);
                CSE110Section2.Meetings.Add(CSE110Lecture);
                CSE110Section2.Meetings.Add(CSE110Discussion2);
                CSE110.Sections.Add(CSE110Section2);


                ScheduleRepo repo = new ScheduleRepo();
                Course[] courses = { CSE20, CSE21, CSE101, CSE110 };
                List<List<Section>> returned;
                returned = repo.FindScheduleForClasses(courses);
                Console.WriteLine("RETURNED SCHEDULE NUM: " + returned.Count);
                foreach(List<Section> l in returned) {
                    foreach(Section s in l) 
                    {
                        //Console.Write(s.Course.CourseAbbreviation + " (" + s.Ticket + ") ");
                        Console.Write(s.Ticket + "\t");
                    }
                    Console.WriteLine(" ");
                }
                return View();
            }
        }
    }
}