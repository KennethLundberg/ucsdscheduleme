using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;
using HtmlAgilityPack;

namespace ucsdscheduleme.Repo
{
    public class ScrapeRepo
    {
        #region Cape

        /// <summary>
        /// Contains XPaths used in scraping data from Cape
        /// </summary>
        struct CapeXPaths
        {
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
        }

        /// <summary>
        /// Scrapes a single Cape page specified by the URL input parameter.
        /// Note that this works only for FA12 term to the present
        /// </summary>
        /// <returns>A List of ScrapeResult objects containing the data</returns>
        /// <param name="Url">URL of single Cape page to scrape</param>
        public List<ScrapeResult> ScrapeCape(string Url)
        {
            // Check for null or empty URL
            if (String.IsNullOrEmpty(Url))
            {
                throw new ArgumentNullException(Url);
            }

            // HTML read from url and assign into var htmlDoc
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(Url);

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
                avgGradeExpected = "N/A";
            }
            else
            {
                avgGradeExpected = (avgGradeExpectedCheck.Substring(0, avgGradeExpectedCheck.IndexOf(' ')));
            }

            // Gather average grade received, if valid only grabs letter grade portion of string
            string avgGradeReceived = "";
            string avgGradeReceivedCheck = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.AvgGradeReceivedNode)?.InnerText;
            if (String.IsNullOrEmpty(avgGradeReceivedCheck))
            {
                avgGradeReceived = ("N/A");
            }
            else
            {
                avgGradeReceived = (avgGradeReceivedCheck.Substring(0, avgGradeExpectedCheck.IndexOf(' ')));
            }

            // Insert the results into their proper member and return
            List<ScrapeResult> retList = new List<ScrapeResult>()
            {
                new ScrapeResult
                    { InstructorName = instructorName,
                      Term = term,
                      StudentsEnrolled = Convert.ToInt32(enrollment),
                      NumberOfEvaluation = Convert.ToInt32(evalsSubmitted),
                      RecommendedClass = Convert.ToDecimal(recCourseMean),
                      RecommendedProfessor = Convert.ToDecimal(recProfMean),
                      StudyHoursPerWeek = Convert.ToDecimal(studyHoursMean),
                      AverageGradeExpected = avgGradeExpected,
                      AverageGradeReceived = avgGradeReceived }
            };
            return retList;
        }

        #endregion

        #region Ratemyprofessors

        /// <summary>
        /// Contains XPaths used in scraping data from RateMyProfessors
        /// </summary>
        struct RateMyProfXPaths
        {
            public static readonly string ProfessorFirstNamePath = "//div[@class='result-name']//span[@class='pfname'][1]";
            public static readonly string ProfessorMidNamePath = "//div[@class='result-name']//span[@class='pfname'][2]";
            public static readonly string ProfessorLastNamePath = "//div[@class='result-name']//span[@class='plname']";
            public static readonly string ProfessorNotRatedNamePath = "//div[@class='name']";

            public static readonly string OverallQualityPath = "//div[contains(.,'Overall Quality')]/div[@class='grade']";
            public static readonly string WouldTakeAgainPath = "//div[contains(.,'Would Take Again')]/div[@class='grade']";
            public static readonly string DifficultyLevelPath = "//div[contains(.,'Level of Difficulty')]/div[@class='grade']";

        }

        /// <summary>
        /// Scrapes a single RateMyProf page specified by the URL input parameter.
        /// </summary>
        /// <returns>A List of ScrapeResultRateMyProf objects containing the data</returns>
        /// <param name="Url">URL of single RateMyProf page to scrape</param>
        public List<ScrapeResult> ScrapeRateMyProf(string Url)
        {
            // Check for null or empty URL
            if (String.IsNullOrEmpty(Url))
            {
                throw new ArgumentNullException(Url);
            }

            // HTML read from url and assign into var htmlDoc
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(Url);

            // Rating Availabilty Indicator
            bool professorIsRated = true;

            // Get Overall Quality Info
            HtmlNode quality = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.OverallQualityPath);
            string qualityRate = (quality != null) ? quality.InnerText : "N/A";

            // Get Would Take Again Info
            HtmlNode wouldTakeAgain = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.WouldTakeAgainPath);
            string wouldTakeAgainRate = (wouldTakeAgain != null) ? wouldTakeAgain.InnerText.Trim() : "N/A";

            // Get Difficulty Level Info
            HtmlNode difficultyLevel = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.DifficultyLevelPath);
            string difficultyLevelRate = (difficultyLevel != null) ? difficultyLevel.InnerText.Trim() : "N/A";


            // Check if professor was rated at all
            if (quality == null && wouldTakeAgain == null && difficultyLevel == null)
            {
                professorIsRated = false;
            }

            // Professor's Full Name
            string professorFullName;
            if (professorIsRated)
            {
                HtmlNode professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorFirstNamePath);
                string professorFirstName = (professorNode != null) ? professorNode.InnerText.Trim() : "N/A";
                professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorMidNamePath);
                string professorMiddleName = (professorNode != null) ? professorNode.InnerText.Trim() : "N/A";
                professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorLastNamePath);
                string professorLastName = (professorNode != null) ? professorNode.InnerText.Trim() : "N/A";

                if (string.IsNullOrEmpty(professorMiddleName))
                {
                    professorFullName = professorFirstName + " " + professorLastName;
                }
                else
                {
                    professorFullName = professorFirstName + " " + professorMiddleName + " " + professorLastName;
                }

            }
            else
            {
                HtmlNode professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorNotRatedNamePath);
                professorFullName = (professorNode != null) ? professorNode.InnerText : "Professor Not Found";
            }

            // Insert the results into their proper member and return
            List<ScrapeResult> rateMyProfessorResult = new List<ScrapeResult>()
            {
                new ScrapeResult
                {
                    InstructorName = professorFullName,
                    OverallQuality = qualityRate,
                    WouldTakeAgain = wouldTakeAgainRate,
                    LevelOfDifficulty = difficultyLevelRate
                }
            };
            return rateMyProfessorResult;
        }

        #endregion
    }
}
