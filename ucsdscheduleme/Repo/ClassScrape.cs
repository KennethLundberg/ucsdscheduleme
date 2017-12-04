using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using ucsdscheduleme.Data;
using ucsdscheduleme.Models;

namespace HtmlAgilitySandbox
{
    class ClassScrape
    {
        // Database context.
        private readonly ScheduleContext _context;
        // Department the query is working on (i.e. CSE, MATH).
        private readonly string department;

        // C# standard is that you only create one of these.
        private static HttpClient client = new HttpClient();
        // This is the Url to make the form post to.
        private static readonly string _baseRequestUrl = @"https://act.ucsd.edu/scheduleOfClasses/scheduleOfClassesStudent.htm";
        // This is the Url that is used to build the request for an individual response.
        private static readonly string _baseIndividualResponseUrl = @"https://act.ucsd.edu/scheduleOfClasses/scheduleOfClassesStudentResult.htm";

        private ActFormRequest _actFormRequest;

        /// <summary>
        /// Contains XPaths used in scraping data from Act.
        /// </summary>
        struct ActStrings
        {
            // XPaths
            public static readonly string AllTables = "//table";
            public static readonly string NavigationTableNode = "//td[@align='right']";
            public static readonly string LastPageNode = "//a[text() = 'Last']";
            public static readonly string AllTableRows = "//table[contains(@class, 'tbrdr')]//tr";
            public static readonly string ClassCrsheader = ".//td[contains(@class, 'crsheader')]";
            public static readonly string PrereqNode = ".//td/span[contains(., 'Prerequisites')]/parent::*";
            public static readonly string MeetingTypeNode = "./td[span[contains(@id, 'insTyp')]]";
            public static readonly string AnchorChildNode = ".//a";
            public static readonly string TRSectionClass = "sectxt";
            public static readonly string TRTestClass = "nonenrtxt";

            // DateTime formatting
            public static readonly string TimeFormat = "h:mmtt";
            public static readonly string DateFormat = "M/d/yyyy";

            // Browser http alias
            public static readonly string UserAlias = "User-Agent";
            public static readonly string BrowserAlias = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
        }

        /// <summary>
        /// Constructor that builds a form request to ucsd act to grab all html pages for a given term and 
        /// course request.
        /// </summary>
        /// <param name="selectedTerm">Usually a four character code that specifies the term you want 
        /// to search from. Ex: "FA17" would return courses from the Fall 2017 quarter.</param>
        /// <param name="courses">The space seperated course search parameter. See the ucsd act class 
        /// search for full information on how to use this. Ex: "cse 1-199" returns all CSE courses between 
        /// 1 and 199</param>
        public ClassScrape(ScheduleContext context, string selectedTerm, string courses)
        {
            _context = context;
            _actFormRequest = new ActFormRequest(selectedTerm, courses);

            // Course abbreviation prefix
            department = courses.Split(" ")[0].ToUpper();
        }

        /// <summary>
        /// Called from ScrapeRepo Update(). The queried courses and term are in the _actFormRequest member.
        /// Calls appropriate functions below to fulfill the query and update the db.
        /// </summary>
        public void Update()
        {
            InsertDataFromHtmlPages(GetAllDocumentsForQuery());
        }

        /// <summary>
        /// Uses the form request parameters specified in the construction of the object to return 
        /// all Html documents for each response page of the course query.
        /// </summary>
        /// <returns>List of all Html documents returned for course query.</returns>
        public List<HtmlDocument> GetAllDocumentsForQuery()
        {
            // The eventual return object.
            List<HtmlDocument> allDocumentsForQuerry = new List<HtmlDocument>();

            // Build out our form request.
            FormUrlEncodedContent request = _actFormRequest.WebregRequestFormEncoded;

            // This one is a post because it contains the search params, all others are going to be get, because
            // state is kept via a cookie that C# automatically handles.
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, _baseIndividualResponseUrl);
            message.Headers.Referrer = new Uri(_baseRequestUrl);
            // Pretend we're a browser.
            message.Headers.Add(ActStrings.UserAlias, ActStrings.BrowserAlias);
            message.Content = request;

            var formResponse = client.SendAsync(message).Result;

            var responseAsString = formResponse.Content.ReadAsStringAsync().Result;

            // Now we have the resutlts for the first page of classes
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseAsString);

            // Get the table that contains the number of pages we'll need to get results for.
            var nodes = htmlDoc.DocumentNode.SelectNodes(ActStrings.AllTables);

            // If nodes if empty then the search didn't find any classes to skip this whole method
            if (nodes == null)
            {
                return null;
            }

            // The navigation table is ALWAYS the third table.
            var navigationTable = nodes[2];
            var navigationText = navigationTable.SelectSingleNode(ActStrings.NavigationTableNode).InnerText;

            // Get the string that states how many pages is the current search result
            var pageNumberString =  navigationText.Substring(0, navigationText.IndexOf(')'));

            // The last page number of the current search
            int lastPageNumber = (pageNumberString.ElementAt(pageNumberString.Length - 1) - '0');

            allDocumentsForQuerry.Add(htmlDoc);

            // Start at page 2 because we already grabbed page 1.
            for(int pageNum = 2; pageNum <= lastPageNumber; pageNum++)
            {
                Uri thisPageUri = new Uri(_baseIndividualResponseUrl + "?page=" + pageNum);
                HttpRequestMessage thisMessage = new HttpRequestMessage(HttpMethod.Get, thisPageUri);
                // Tell ucsd that we're always coming from the first response page.
                thisMessage.Headers.Referrer = new Uri(_baseIndividualResponseUrl);
                // Pretend we're a browser.
                thisMessage.Headers.Add(ActStrings.UserAlias, ActStrings.BrowserAlias);

                var pageResponse = client.SendAsync(thisMessage).Result;

                var pageResponseAsString = pageResponse.Content.ReadAsStringAsync().Result;

                // Now we have the results for this page of classes
                var thisHtmlDoc = new HtmlDocument();
                thisHtmlDoc.LoadHtml(pageResponseAsString);

                // Add it to the final return
                allDocumentsForQuerry.Add(thisHtmlDoc);
            }

            return allDocumentsForQuerry;
        }

        /// <summary>
        /// Parses ACT pages received from class querry and inserts results into database.
        /// </summary>
        /// <param name="allDocumentsForQuerry">The HTML pages gathered from the course querry.</param>
        private void InsertDataFromHtmlPages(List<HtmlDocument> allDocumentsForQuerry)
        {

            // Check if the list is valid or not
            if(allDocumentsForQuerry == null)
            {
                return;
            }

            // Hash maps for each object needed in database
            // For course we're explicitly telling to grab the meeting of all sections, ratemyprofessor of all professor
            // and cape of all course
            Dictionary<string, Course> courseDictionary = _context?.Courses
                                                                   .Include(c => c.Sections)
                                                                        .ThenInclude(s => s.Meetings)
                                                                   .Include(c => c.Cape)
                                                                   .Include(c => c.Sections)
                                                                        .ThenInclude(s => s.Professor)
                                                                        .ThenInclude(p => p.RateMyProfessor)
                                                                   .ToDictionary(c => c.CourseAbbreviation);
            Dictionary<string, Section> sectionDictionary = _context?.Sections.ToDictionary(s => s.Ticket.ToString());
            Dictionary<string, Location> locationDictionary = _context?.Locations.ToDictionary(l => l.Building + l.RoomNumber);
            Dictionary<string, Professor> professorDictionary = _context?.Professor.ToDictionary(p => p.Name);

            // Vars to keep track of current class number, professor and course.
            string currentClassNum = "";
            string currentProf = "";
            Course currentCourse = new Course();
            // Chars to trim from the extracted units.
            char[] unitsTrimChars = { '(', ')' };

            // Variable to keep track of when we get final and lecture mixed up
            // with the schedule in act.ucsd.edu
            bool foundLE = false;

            // Lists of each needed object to keep track of each iteration.
            List<Course> courseList = new List<Course>();
            List<Section> sectionList = new List<Section>();
            List<Meeting> meetingList = new List<Meeting>();

            // Remove meetings from db
            //var done = _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE Meetings").Result;

            // Loop through all of the documents in the query.
            foreach (HtmlDocument htmlDocument in allDocumentsForQuerry)
            {
                // Get all of the TR children of the table.
                HtmlNodeCollection allTableNodes = htmlDocument.DocumentNode.SelectNodes(ActStrings.AllTableRows);

                foreach (HtmlNode node in allTableNodes)
                {
                    // Gather current node's class for checks later on.
                    var nodeClasses = node.Attributes["class"]?.Value;

                    // Check if TR contains children with course name and abbreviation.
                    HtmlNodeCollection trCrsHeaderCheck = node?.SelectNodes(ActStrings.ClassCrsheader);

                    // This label is used when there is a lecture meeting type with final node name in the 
                    // act website, eg Math 3C, 4c
                    lectureInFinal:

                    // This block will scrape the header of each course offered, 
                    // Will scrape the name of the course, units, course name
                    if (trCrsHeaderCheck != null)
                    {
                        // Make sure that the "Prerequisites field exists, otherwise this row is empty.
                        HtmlNode prereqNode = node?.SelectSingleNode(ActStrings.PrereqNode);
                        if (prereqNode == null) continue;

                        // Push all static meetings to each section. Static meeting needs to have sectionID = null
                        foreach (Meeting meeting in meetingList)
                        {
                            foreach (Section section in sectionList)
                            {
                                // Create new meeting each time so that each section will have correct
                                // unique meetings.
                                Meeting staticMeeting = new Meeting()
                                {
                                    Code = meeting.Code,
                                    Days = meeting.Days,
                                    StartTime = meeting.StartTime,
                                    EndTime = meeting.EndTime,
                                    StartDate = meeting.StartDate,
                                    MeetingType = meeting.MeetingType,
                                    Location = meeting.Location,
                                    IsUnique = false
                                };
                                section.Meetings.Add(staticMeeting);
                            }
                        }

                        // Push all sections to current course.
                        foreach (Section section in sectionList)
                        {
                            currentCourse.Sections.Add(section);
                        }

                        // Empty meeting and section lists for next iteration.
                        meetingList.Clear();
                        sectionList.Clear();

                        // Nodes for the class name and the class number.
                        HtmlNode classNameTDNode = prereqNode.PreviousSibling.PreviousSibling;
                        HtmlNode classNumberTDNode = classNameTDNode.PreviousSibling.PreviousSibling;

                        // Check if the current class number matches the previous iteration's.
                        string classNum = classNumberTDNode.InnerText;
                        if (classNum != currentClassNum)
                        {
                            // Trim the class name and units fields.
                            HtmlNode classNameNode = classNameTDNode.FirstChild.NextSibling.FirstChild;
                            string className = classNameNode.InnerText;
                            string classNameTrimmed = className.Replace("&amp;", "&");
                            HtmlNode classUnitsNode = classNameNode.ParentNode.NextSibling;
                            string classUnits = classUnitsNode.InnerText;
                            string classUnitsTrimSpaces = Regex.Replace(classUnits, @"\s+", "");
                            string classUnitsTrimUnits = classUnitsTrimSpaces.Replace("Units", "");
                            string classUnitsTrimmed = classUnitsTrimUnits.Trim(unitsTrimChars);

                            // Create new course for course list after checking for existence in db.
                            currentClassNum = classNum;
                            string courseAbbrev = department + " " + classNum;

                            // Find the course in the db or make new course object
                            if (!courseDictionary.TryGetValue(courseAbbrev, out currentCourse))
                            {
                                currentCourse = new Course
                                {
                                    CourseName = classNameTrimmed,
                                    CourseAbbreviation = courseAbbrev,
                                    Units = classUnitsTrimmed
                                };
                                courseList.Add(currentCourse);
                            }
                            else
                            {
                                // Delete all the meetings, rmp, and cape of the course which was found from the 
                                // database and later we'll repopulate it.
                                var meetingsToDelete = currentCourse.Sections.SelectMany(s => s.Meetings);
                                var rmpsToDelete = currentCourse.Sections.Select(s => s.Professor.RateMyProfessor);
                                var capesToDelete = currentCourse.Cape;
                                foreach (RateMyProfessor rmpToDelete in rmpsToDelete)
                                {
                                    if (rmpToDelete != null)
                                        _context.RateMyProfessor.Remove(rmpToDelete);
                                }                                
                                _context.Cape.RemoveRange(capesToDelete);
                                _context.Meetings.RemoveRange(meetingsToDelete);
                                _context.SaveChanges();
                            }

                        }
                    }

                    // Else check if node is a TR that contains section/meeting data.
                    // This block will scrape the lecture/dicsussion/lab part
                    else if ((nodeClasses != null && nodeClasses.Contains(ActStrings.TRSectionClass)) || foundLE == true)
                    {

                        foundLE = false;

                        /*****
                         Scraping meeting type and the code of the meetingtype, all meeting type
                         will have these information.
                         ******/

                        // Select the meeting type node to begin iterating from.
                        HtmlNode meetingTypeTDNode = node.SelectSingleNode(ActStrings.MeetingTypeNode);

                        // Create new meeting and set meeting type.
                        Meeting currentMeeting = new Meeting();
                        string meetingType = meetingTypeTDNode.InnerText;
                        currentMeeting.MeetingType = GetMeetingType(meetingType);

                        // Set it to true as default, if the meeting is unique, like final and lecture,
                        // we're changing it later.
                        currentMeeting.IsUnique = true;

                        // Set code.
                        HtmlNode codeNode = meetingTypeTDNode.NextSibling.NextSibling;
                        string code = codeNode.InnerText;
                        currentMeeting.Code = code;

                        /*****
                         Scraping Days, Time, Location, and Professor of the meeting type
                         *****/
                        
                        HtmlNode daysNode = codeNode.NextSibling.NextSibling;
                        // Check if class times are not TBA, and if class is not cancelled.
                        if (!daysNode.InnerText.Contains("TBA") && !daysNode.InnerText.Contains("Cancelled"))
                        {
                            // Set days.
                            string days = daysNode.InnerText.Trim();
                            currentMeeting.Days = GetDays(days);

                            // Set time after formatting it into DateTime.
                            HtmlNode timeNode = daysNode.NextSibling.NextSibling;
                            string unsplitTime = timeNode.InnerText;
                            string startTime = unsplitTime.Substring(0, unsplitTime.IndexOf('-')) + "m";
                            string endTime = unsplitTime.Substring(unsplitTime.IndexOf('-') + 1, unsplitTime.Length - unsplitTime.IndexOf('-') - 1) + "m";
                            currentMeeting.StartTime = DateTime.ParseExact(startTime, ActStrings.TimeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            currentMeeting.EndTime = DateTime.ParseExact(endTime, ActStrings.TimeFormat, System.Globalization.CultureInfo.InvariantCulture);

                            // Set building and classroom.
                            HtmlNode buildingNode = timeNode.NextSibling.NextSibling;
                            HtmlNode roomNode = buildingNode.NextSibling.NextSibling;
                            string buildingName = buildingNode.InnerText;
                            string roomNumber = roomNode.InnerText;

                            // Check if location exists in database.
                            Location classLocation;
                            if (!locationDictionary.TryGetValue(buildingName + roomNumber, out classLocation))
                            {
                                classLocation = new Location
                                {
                                    Building = buildingName,
                                    RoomNumber = roomNumber
                                };
                                locationDictionary.Add(buildingName + roomNumber, classLocation);
                            }
                            currentMeeting.Location = classLocation;

                            // Check if instructor is not determined yet (currently set as "Staff").
                            HtmlNode instrNode = roomNode.NextSibling.NextSibling?.SelectSingleNode(ActStrings.AnchorChildNode) ?? roomNode.NextSibling.NextSibling;

                            // Check if current meeting has an empty instructor field.
                            if (!String.IsNullOrEmpty(instrNode.InnerText.Trim()))
                            {
                                currentProf = instrNode.InnerText.Trim();
                            }
                        }
                        // Meeting exists but its day, time (not initialized in case of TBA) and location are TBA.
                        else if (daysNode.InnerText.Contains("TBA"))
                        {
                            currentMeeting.Days = GetDays("TBA");

                            // Check if TBA location exists in database.
                            Location classLocation;
                            if (!locationDictionary.TryGetValue("TBATBA", out classLocation))
                            {
                                classLocation = new Location
                                {
                                    Building = "TBA",
                                    RoomNumber = "TBA"
                                };
                                locationDictionary.Add("TBATBA", classLocation);
                            }
                            currentMeeting.Location = classLocation;

                            // Check for edge case where professor field is empty, so we use the previous professor.
                            HtmlNode instrNode = daysNode.NextSibling.NextSibling?.SelectSingleNode(ActStrings.AnchorChildNode) ?? daysNode.NextSibling.NextSibling;
                            if (!String.IsNullOrEmpty(instrNode.InnerText.Trim()))
                            {
                                currentProf = instrNode.InnerText.Trim();
                            }
                        }
                        // Class is cancelled so continue to next node.
                        else if (daysNode.InnerText.Contains("Cancelled")) continue;

                        /*****
                         Scraping the section Id (ticket) if present, and assign professor to the section. 
                         *****/

                        // Check if the section ID exists for this row.
                        HtmlNode sectionIDNode = meetingTypeTDNode.PreviousSibling.PreviousSibling;
                        if (!String.IsNullOrEmpty(sectionIDNode.InnerText.Trim()) && !sectionIDNode.InnerText.Trim().Contains("&nbsp"))
                        {
                            // Section ID exists, so create new section and add current meeting to section.
                            // Check if professor exists in database.
                            Professor prof;
                            if (!professorDictionary.TryGetValue(currentProf, out prof))
                            {
                                prof = new Professor
                                {
                                    Name = currentProf
                                };
                                professorDictionary.Add(currentProf, prof);
                            }

                            // Check if section exists in database.
                            int ticket = Int32.Parse(sectionIDNode.InnerText.Trim());
                            Section currentSection;
                            if (!sectionDictionary.TryGetValue(ticket.ToString(), out currentSection))
                            {
                                currentSection = new Section
                                {
                                    Ticket = ticket,
                                    Course = currentCourse,
                                    Professor = prof
                                };
                            }
                            currentSection.Meetings.Add(currentMeeting);
                            sectionList.Add(currentSection);
                        }
                        else
                        {
                            // Add new Meeting (to be added to every section) to the static list.
                            meetingList.Add(currentMeeting);
                        }
                    }

                    // Else check if node is a TR that contains final/midterms data.
                    else if (nodeClasses != null && nodeClasses.Contains(ActStrings.TRTestClass))
                    {
                        // Select the test type node to begin iterating from.
                        HtmlNode testTypeTDNode = node?.SelectSingleNode(ActStrings.MeetingTypeNode);

                        // Create new meeting.
                        Meeting currentMeeting = new Meeting();

                        // Edge case for when test fields contains title information.
                        if (testTypeTDNode == null) continue;

                        // Set meeting type.
                        string testType = testTypeTDNode.InnerText;
                        currentMeeting.MeetingType = GetMeetingType(testType);

                        // If we get a meeting of type non-test or review, go back to start of if statement.
                        if (testType != "FI" && testType != "MI" && testType != "RE" && testType != "PB")
                        {
                            foundLE = true;
                            goto lectureInFinal;
                        }

                        // Set date.
                        HtmlNode testDateNode = testTypeTDNode.NextSibling.NextSibling;
                        string date = testDateNode.InnerText;   // Here is the error, we get date to be A00, when scraping math 3C. 
                        currentMeeting.StartDate = DateTime.ParseExact(date, ActStrings.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                        // Set test day.
                        HtmlNode testDayNode = testDateNode.NextSibling.NextSibling;
                        // Check if test is cancelled, else continue grabbing data.
                        if (testDayNode.InnerText.Contains("Cancelled")) continue;
                        string days = testDayNode.InnerText.Trim();
                        currentMeeting.Days = GetDays(days);

                        // Set time after formatting it into DateTime.
                        HtmlNode testTimeNode = testDayNode.NextSibling.NextSibling;
                        string unsplitTime = testTimeNode.InnerText;
                        string startTime = unsplitTime.Substring(0, unsplitTime.IndexOf('-')) + "m";
                        string endTime = unsplitTime.Substring(unsplitTime.IndexOf('-') + 1, unsplitTime.Length - unsplitTime.IndexOf('-') - 1) + "m";
                        currentMeeting.StartTime = DateTime.ParseExact(startTime, ActStrings.TimeFormat, System.Globalization.CultureInfo.InvariantCulture);
                        currentMeeting.EndTime = DateTime.ParseExact(endTime, ActStrings.TimeFormat, System.Globalization.CultureInfo.InvariantCulture);

                        // Set test building and room number.
                        HtmlNode testBuildingNode = testTimeNode.NextSibling.NextSibling;
                        HtmlNode testRoomNode = testBuildingNode.NextSibling.NextSibling;

                        string testBuilding = testBuildingNode.InnerText;
                        string testRoom = testRoomNode.InnerText;

                        // Check if location exists in database.
                        Location classLocation;
                        if (!locationDictionary.TryGetValue(testBuilding + testRoom, out classLocation))
                        {
                            classLocation = new Location
                            {
                                Building = testBuilding,
                                RoomNumber = testRoom
                            };
                            locationDictionary.Add(testBuilding + testRoom, classLocation);
                        }
                        currentMeeting.Location = classLocation;

                        // Push to all sections currently in section list.
                        foreach (Section section in sectionList)
                        {
                            // Create new final meeting each time so that each section will have correct
                            // unique meetings. Static meeting needs to have sectionID = null
                            Meeting finalMeeting = new Meeting()
                            {
                                Code = currentMeeting.Code,
                                Days = currentMeeting.Days,
                                StartTime = currentMeeting.StartTime,
                                EndTime = currentMeeting.EndTime,
                                StartDate = currentMeeting.StartDate,
                                MeetingType = currentMeeting.MeetingType,
                                Location = currentMeeting.Location,
                                IsUnique = false
                            };
                            section.Meetings.Add(finalMeeting);
                        }
                    }
                }
            }

            // Push the last static meetings to each section.
            foreach (Meeting meeting in meetingList)
            {
                foreach (Section section in sectionList)
                {
                    // Create new meeting each time so that each section will have correct
                    // unique meetings. Static meeting needs to have sectionID = null
                    Meeting staticMeeting = new Meeting()
                    {
                        Code = meeting.Code,
                        Days = meeting.Days,
                        StartTime = meeting.StartTime,
                        EndTime = meeting.EndTime,
                        StartDate = meeting.StartDate,
                        MeetingType = meeting.MeetingType,
                        Location = meeting.Location,
                        IsUnique = false
                    };
                    section.Meetings.Add(staticMeeting);
                }
            }

            // Push all sections to all course instances to current course.
            foreach (Section section in sectionList)
            {
                currentCourse.Sections.Add(section);
            }

            // Add courses to db and save.
            foreach (Course course in courseList)
            {
                _context.Courses.Add(course);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the MeetingType of the meeting.
        /// </summary>
        /// <returns>The MeetingType.</returns>
        /// <param name="meetingType">String representation of the meeting type.</param>
        public static MeetingType GetMeetingType(string meetingType)
        {
            switch (meetingType)
            {
                case "LE":
                    return MeetingType.Lecture;
                case "DI":
                    return MeetingType.Discussion;
                case "LA":
                    return MeetingType.Lab;
                case "RE":
                    return MeetingType.Review;
                case "FI":
                    return MeetingType.Final;
                case "MI":
                    return MeetingType.Midterm;
                case "SE":
                    return MeetingType.Seminar;
                case "PR":
                    return MeetingType.Practicum;
                case "IN":
                    return MeetingType.IndependentStudy;
                case "ST":
                    return MeetingType.Studio;
                case "TU":
                    return MeetingType.Tutorial;
                case "PB":
                    return MeetingType.ProblemSession;
                case "FW":
                    return MeetingType.Fieldwork;
                case "SA":
                    return MeetingType.StudyAbroad;
                case "MU":
                    return MeetingType.MakeUpSession;
                default:
                    throw new FormatException(meetingType);

            }
        }

        /// <summary>
        /// Gets the enumerated days of the meeting.
        /// </summary>
        /// <returns>The enumerated days of the meeting.</returns>
        /// <param name="days">String that represents the days of the meeting.</param>
        public static Days GetDays(string days)
        {
            // Initialize the return value.
            Days daysEnumerated = 0;

            if (days == "TBA")
            {
                return Days.TBA;
            }

            for (int i = 0; i < days.Length; i++)
            {
                switch (days[i])
                {
                    case 'S':
                        // Case for Saturday (with a check first to see if Saturday is the only day listed).
                        if (i + 1 < days.Length && days[++i] != 'u')
                        {
                            daysEnumerated |= Days.Saturday;
                            --i;
                        }
                        // Case for Sunday.
                        else
                        {
                            daysEnumerated |= Days.Sunday;
                            ++i;
                        }
                        break;
                    case 'M':
                        daysEnumerated |= Days.Monday;
                        break;
                    case 'T':
                        // Case for Tuesday.
                        if (days[++i] == 'u')
                        {
                            daysEnumerated |= Days.Tuesday;
                        }
                        //Case for Thursday.
                        else
                        {
                            daysEnumerated |= Days.Thursday;
                        }
                        break;
                    case 'W':
                        daysEnumerated |= Days.Wednesday;
                        break;
                    case 'F':
                        daysEnumerated |= Days.Friday;
                        break;
                    default:
                        throw new FormatException(days);
                }
            }

            return daysEnumerated;
        }
    }
}
