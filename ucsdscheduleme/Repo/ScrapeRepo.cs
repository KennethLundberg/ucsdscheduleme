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
        /// <summary>
        /// Contains XPaths used in scraping data from Cape
        /// </summary>
        struct CapeXPaths
        {
            public static string InstrTermEnrollEvals = "//div[contains(@class, 'col_1_of_2 col')]";
            public static string RecommendClassTblRow = "//span[contains(., 'Do you recommend this course overall?')]/../.. " +
                                                     "| //span[contains(., 'Do you recommend this seminar overall?')]/../.." +
                                                     "| //span[contains(., 'Do you recommend this lab overall?')]/../..";
            public static string RecommendProfTblRow = "//span[contains(., 'Do you recommend this professor overall?')]/../..";
            public static string HoursPerWeekTblRow = "//span[contains(., 'How many hours a week do you spend studying outside of class on average?')]/../..";
            public static string TblRowToMean = ".//span[contains(@id, 'Mean')]";
            public static string AvgGradeExpectedNode = "//span[@id = 'ctl00_ContentPlaceHolder1_lblAverageGradeExpected']";
            public static string AvgGradeReceivedNode = "//span[@id = 'ctl00_ContentPlaceHolder1_lblAverageGradeReceived']";
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

            // List to hold results as strings before converting datatypes
            Queue<string> scrapeResultsQ = new Queue<string>();

            // Gather prof name, term, students enrolled, num of evals. First 3 nodes must be skipped (trust me it's cleaner)
            var genInfoNodes = htmlDoc.DocumentNode.SelectNodes(CapeXPaths.InstrTermEnrollEvals).Descendants("span").Skip(3);
            foreach (HtmlNode node in genInfoNodes)
            {
                scrapeResultsQ.Enqueue(node.InnerText);
            }

            // Gather percentage of enrolled students that recommend course
            HtmlNode recCourseRow = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.RecommendClassTblRow);
            string recCourseMean = recCourseRow.SelectSingleNode(CapeXPaths.TblRowToMean).InnerText;
            scrapeResultsQ.Enqueue(recCourseMean);

            // Gather percentage of enrolled students that recommend professor
            HtmlNode recProfRow = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.RecommendProfTblRow);
            string recProfMean = recProfRow.SelectSingleNode(CapeXPaths.TblRowToMean).InnerText;
            scrapeResultsQ.Enqueue(recProfMean);

            // Gather average hours of studying per week
            HtmlNode avgStudyHoursRow = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.HoursPerWeekTblRow);
            string avgStudyHoursMean = avgStudyHoursRow.SelectSingleNode(CapeXPaths.TblRowToMean).InnerText;
            scrapeResultsQ.Enqueue(avgStudyHoursMean);

            // Gather average grade expected and push to queue after error checking
            string avgGradeExpected = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.AvgGradeExpectedNode)?.InnerText;
            if (String.IsNullOrEmpty(avgGradeExpected))
            {
                scrapeResultsQ.Enqueue("N/A");
            }
            else
            {
                scrapeResultsQ.Enqueue(avgGradeExpected.Substring(0, avgGradeExpected.IndexOf(' ')));
            }

            // Gather average grade received and push to queue after error checking
            string avgGradeReceived = htmlDoc.DocumentNode.SelectSingleNode(CapeXPaths.AvgGradeReceivedNode)?.InnerText;
            if (String.IsNullOrEmpty(avgGradeReceived))
            {
                scrapeResultsQ.Enqueue("N/A");
            }
            else
            {
                scrapeResultsQ.Enqueue(avgGradeReceived.Substring(0, avgGradeExpected.IndexOf(' ')));
            }

            // Convert the results in the queue to their proper values and return
            List<ScrapeResult> retList = new List<ScrapeResult>()
            {
                new ScrapeResult
                    { InstructorName = scrapeResultsQ.Dequeue(),
                      Term = scrapeResultsQ.Dequeue(),
                      StudentsEnrolled = Convert.ToInt32(scrapeResultsQ.Dequeue()),
                      NumberOfEvaluation = Convert.ToInt32(scrapeResultsQ.Dequeue()),
                      RecommendedClass = Convert.ToDecimal(scrapeResultsQ.Dequeue()),
                      RecommendedProfessor = Convert.ToDecimal(scrapeResultsQ.Dequeue()),
                      StudyHoursPerWeek = Convert.ToDecimal(scrapeResultsQ.Dequeue()),
                      AverageGradeExpected = scrapeResultsQ.Dequeue(),
                      AverageGradeReceived = scrapeResultsQ.Dequeue() }
            };
            return retList;
        }
    }
}
