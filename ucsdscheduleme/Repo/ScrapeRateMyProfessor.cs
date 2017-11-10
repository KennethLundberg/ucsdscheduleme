using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo
{
    public class ScrapeRateMyProfessor
    {
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
        public ScrapeResult ScrapeRateMyProf(string Url)
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

            // Insert the results into their proper fields and return ScrapeResult obj
            ScrapeResult rateMyProfessorResult = new ScrapeResult()
            {
                InstructorName = professorFullName,
                OverallQuality = qualityRate,
                WouldTakeAgain = wouldTakeAgainRate,
                LevelOfDifficulty = difficultyLevelRate
            
            };
            return rateMyProfessorResult;
        }

    }
}

