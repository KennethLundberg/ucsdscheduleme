using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;
using HtmlAgilityPack;
using ucsdscheduleme.Data;
using Microsoft.EntityFrameworkCore;

namespace ucsdscheduleme.Repo
{
    public class CapeScrape
    {

        // The current class we're scraping
        private static string CourseName;

        private readonly ScheduleContext _context;
        // Sending in context to files in /repo.
        public CapeScrape(ScheduleContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Contains XPaths used in scraping data from Cape
        /// </summary>
        struct CapeXPaths
        {
            public static readonly string CourseNamePath = "//*[@id='ctl00_ContentPlaceHolder1_lblCourseDescription']";
            public static readonly string InstrNamePath = "//span[contains(@id, 'ctl00_ContentPlaceHolder1_lblInstructorName')]";
            public static readonly string TermPath = "//span[contains(@id, 'ctl00_ContentPlaceHolder1_lblTermCode')]";
            public static readonly string EnrollmentPath = "//span[contains(@id, 'ctl00_ContentPlaceHolder1_lblEnrollment')]";
            public static readonly string EvalsSubmittedPath = "//span[contains(@id, 'ctl00_ContentPlaceHolder1_lblEvaluationsSubmitted')]";
            public static readonly string RecommendClassTblRow = "//span[contains(., 'Do you recommend this course overall?')]/../.. " +
                                                     "| //span[contains(., 'Do you recommend this seminar overall?')]/../.." +
                                                     "| //span[contains(., 'Do you recommend this lab overall?')]/../..";
            public static readonly string RecommendProfTblRow = "//span[contains(., 'Do you recommend this professor overall?')]/../..";
            public static readonly string HoursPerWeekTblRow = "//span[contains(., 'How many hours a week do you spend studying outside of class on average?')]/../..";
            public static readonly string TblRowToMean = ".//span[contains(@id, 'Mean')]";
            public static readonly string AvgGradeExpectedNode = "//span[@id = 'ctl00_ContentPlaceHolder1_lblAverageGradeExpected']";
            public static readonly string AvgGradeReceivedNode = "//span[@id = 'ctl00_ContentPlaceHolder1_lblAverageGradeReceived']";

            public static readonly string SectionSearchList = "//a[@id='ctl00_ContentPlaceHolder1_gvCAPEs_ctl02_hlViewReport']";

        }

        /// <summary>
        /// Gets all the professor and the courses from the database, and using
        /// populates the database using the name of the course and professor
        /// </summary>
        public void Update()
        {
            // Getting the list of courses from the database
            //var courses = _context.Courses.ToList();
            var courses = _context?.Courses.Include(c => c.Sections)
                                   .ThenInclude(s => s.Professor)
                                   .Include(c => c.Sections)
                                        .ToList();

            // List of professor and courses that do not have cape review
            List<Tuple<string, string>> profcourseNotFound = new List<Tuple<string, string>>();

            // Keep track of number of course we scrape 
            int delay = 0;

            // Iterate over all the courses we recieved from the database
            foreach (var course in courses)
            {
                // for each course list out all the sections that are present
                var sections = course.Sections.ToList();

                // Variable to store all the professors that are teaching one course
                List<string> listOfProfessors = new List<string>();

                // Iterate over all the sections for each course
                foreach (var section in sections)
                {
                    Professor currProfessor = section.Professor;
                    // Update the Cape review for each professor only one time
                    if (!listOfProfessors.Contains(currProfessor.Name))
                    {
                        listOfProfessors.Add(currProfessor.Name);

                        // Note: Cape has already been initialized in the database model
                        // as a empty list.

                        // Gets the CAPE page for the specific professor
                        string capePageURL = GenerateURL(currProfessor.Name, course.CourseAbbreviation);

                        // If there is cape review page for the specific professor and specific course
                        if (capePageURL.Length != 0)
                        {
                            ScrapeResult scrapedCapePage = InsertDataFromHtmlPage(capePageURL);
                            Cape newCapeReview = null;
                            // Boolean to keep track if we need to add the cape review to the DB or
                            // just update
                            bool addToDb = true;

                            // Check if we already have the cape for specific professor and course
                            foreach (Cape tempCape in course.Cape)
                            {
                                if (tempCape.Professor == currProfessor)
                                {
                                    newCapeReview = tempCape;
                                    addToDb = false;
                                }
                            }

                            // If no cape found then make a new one
                            if (newCapeReview == null)
                            {
                                newCapeReview = new Cape();
                            }

                            if (scrapedCapePage != null)
                            {
                                // Updating the cape review result for professor object
                                newCapeReview.Term = scrapedCapePage.Term;
                                newCapeReview.StudentsEnrolled = scrapedCapePage.StudentsEnrolled;
                                newCapeReview.NumberOfEvaluation = scrapedCapePage.NumberOfEvaluation;
                                newCapeReview.RecommendedClass = scrapedCapePage.RecommendedClass;
                                newCapeReview.RecommendedProfessor = scrapedCapePage.RecommendedProfessor;
                                newCapeReview.StudyHoursPerWeek = scrapedCapePage.StudyHoursPerWeek;
                                newCapeReview.AverageGradeExpected = scrapedCapePage.AverageGradeExpected;
                                newCapeReview.AverageGradeReceived = scrapedCapePage.AverageGradeReceived;
                                newCapeReview.URL = capePageURL;

                                // Adding cape review to professor and cource object
                                if (addToDb)
                                {
                                    currProfessor.Cape.Add(newCapeReview);
                                    course.Cape.Add(newCapeReview);
                                }
                            }

                        }
                        else
                        {
                            // Used to check when and for which tuples did we not find cape reviews.
                            profcourseNotFound.Add(Tuple.Create(currProfessor.Name, course.CourseAbbreviation));
                        }

                    }
                }

                // After every 100 courses scraped, wait for 10 seconds 
                delay++;
                if ( delay % 100 == 0)
                {
                    System.Threading.Thread.Sleep(10000);
                }
            }

            // Save Changes in the database
            _context.SaveChanges();

        }

        /// <summary>
        /// Generate the url of a specific Cape page from professor's name and course name
        /// </summary>
        /// <param name="professorName"> Name of the professor </param>
        /// <param name="courseName"> Name of the course </param>
        /// <returns> The URL to the page that has the review of the class and the professor </returns>
        public static string GenerateURL(string professorName, string courseName)
        {
            // Encode the professor's name and course name
            string encodedName = System.Net.WebUtility.UrlEncode(professorName);
            string encodedCourse = System.Net.WebUtility.UrlEncode(courseName);

            // Generate url to access search results 
            string searchResultsUrl = @"http://cape.ucsd.edu/responses/Results.aspx?Name=" + professorName
                        + "&CourseNumber=" + courseName;

            CapeScrape.CourseName = courseName;

            // Load the search result page
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(searchResultsUrl);

            // Get the first course listed for the professor and the course name
            HtmlNode searchResult = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.SectionSearchList);
            if (searchResult != null && searchResult.Attributes["href"] != null)
            {
                // Return the url generated by the searchResult's section ID
                return (@"http://cape.ucsd.edu/responses/" + searchResult.Attributes["href"].Value);
            }

            // If no search result was found return empty string
            return "";
        }

        /// <summary>
        /// Scrapes a single Cape page specified by the URL input parameter.
        /// Note that this works only for FA12 term to the present
        /// </summary>
        /// <returns>ScrapeResult objects containing the data</returns>
        /// <param name="Url">URL of single Cape page to scrape</param>
        public ScrapeResult InsertDataFromHtmlPage(string Url)
        {
            // Check for null or empty URL
            if (String.IsNullOrEmpty(Url))
            {
                throw new ArgumentNullException(Url);
            }

            // HTML read from url and assign into var htmlDoc
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(Url);

            // Get the course name 
            HtmlNode crseNameNode = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.CourseNamePath);
            string fullScrapedCourse = (crseNameNode != null) ? crseNameNode.InnerText : "N/A";
            string[] splitCourse = fullScrapedCourse.Split(" ");

            // Cape is too old to scrape
            if (splitCourse.Length < 2)
                return null;

            string courseName = splitCourse[0] + " " + splitCourse[1];
            // Check if we're looking at the right course scrape
            if (CourseName != courseName)
                return null;

            // Gather professor and check for null return
            HtmlNode instrNameNode = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.InstrNamePath);
            string instructorName = (instrNameNode != null) ? instrNameNode.InnerText : "N/A";

            // Gather term and check for null return
            HtmlNode termNode = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.TermPath);
            string term = (termNode != null) ? termNode.InnerText : "N/A";

            // Gather enrollment and check for null return
            HtmlNode enrollmentNode = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.EnrollmentPath);
            string enrollment = (enrollmentNode != null) ? enrollmentNode.InnerText : "0";

            // Gather number of evalulations and check for null return
            HtmlNode evalsSubmittedNode = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.EvalsSubmittedPath);
            string evalsSubmitted = (enrollmentNode != null) ? evalsSubmittedNode.InnerText : "0";

            // Gather percentage of enrolled students that recommend course and check for null return
            HtmlNode recCourseRow = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.RecommendClassTblRow);
            if (recCourseRow == null)
            {
                return null;
            }
            HtmlNode recCourseNode = recCourseRow.SelectSingleNode(CapeXPaths.TblRowToMean);
            string recCourseMean = (recCourseNode != null) ? recCourseNode.InnerText : "0";

            // Gather percentage of enrolled students that recommend professor and check for null return
            HtmlNode recProfRow = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.RecommendProfTblRow);
            HtmlNode recProfNode = recProfRow.SelectSingleNode(CapeXPaths.TblRowToMean);
            string recProfMean = (recProfNode != null) ? recProfNode.InnerText : "0";

            // Gather average hours of studying per week and check for null return
            HtmlNode studyHoursRow = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.HoursPerWeekTblRow);
            HtmlNode studyHoursNode = studyHoursRow.SelectSingleNode(CapeXPaths.TblRowToMean);
            string studyHoursMean = (studyHoursNode != null) ? studyHoursNode.InnerText : "0";

            // Gather average grade expected, if valid only grabs letter grade portion of string
            string avgGradeExpected = "";
            string avgGradeExpectedCheck = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.AvgGradeExpectedNode)?.InnerText;
            if (String.IsNullOrEmpty(avgGradeExpectedCheck))
            {
                // When cape doesn't have grade expected value
                avgGradeExpected = "0";
            }
            else
            {
                avgGradeExpected = (avgGradeExpectedCheck.Substring(avgGradeExpectedCheck.IndexOf("(")));
                avgGradeExpected = avgGradeExpected.Replace("(", "");
                avgGradeExpected = avgGradeExpected.Replace(")", "");
            }

            // Gather average grade received, if valid only grabs letter grade portion of string
            string avgGradeReceived = "";
            string avgGradeReceivedCheck = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.AvgGradeReceivedNode)?.InnerText;
            if (String.IsNullOrEmpty(avgGradeReceivedCheck))
            {
                // When cape doesn't have grade recieved value
                avgGradeReceived = ("0");
            }
            else
            {
                avgGradeReceived = (avgGradeReceivedCheck.Substring(avgGradeReceivedCheck.IndexOf('(')));
                avgGradeReceived = avgGradeReceived.Replace("(", "");
                avgGradeReceived = avgGradeReceived.Replace(")", "");
            }

            // Make a scrapeResult object to store the result
            ScrapeResult result = new ScrapeResult()
            {
                InstructorName = instructorName,
                Term = term,
                StudentsEnrolled = Convert.ToInt32(enrollment),
                NumberOfEvaluation = Convert.ToInt32(evalsSubmitted),
                RecommendedClass = Convert.ToDecimal(recCourseMean),
                RecommendedProfessor = Convert.ToDecimal(recProfMean),
                StudyHoursPerWeek = Convert.ToDecimal(studyHoursMean),
                AverageGradeExpected = Convert.ToDecimal(avgGradeExpected),
                AverageGradeReceived = Convert.ToDecimal(avgGradeReceived)
            };
            return result;
        }

    }
}
