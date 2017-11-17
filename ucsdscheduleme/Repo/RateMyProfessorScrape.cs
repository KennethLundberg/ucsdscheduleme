using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ucsdscheduleme.Data;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Repo
{
    public class RateMyProfessorScrape
    {
        private readonly ScheduleContext _context;

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
        /// Gets all the professor from the Database, and using the name of the professor
        /// stores the review of the professor from the ratemyprofessors.
        /// </summary>
        public List<ScrapeResult> Update()
        {
            // Get professors list from database
            var professors = _context.Professor.ToList();
            
            // Create empty list of ScrapeResult
            List<ScrapeResult> scrapeRMP = new List<ScrapeResult>();

            // For each professor in the list from database
            foreach (var professor in professors)
            {
                // Check if the professor has RateMyProfessor object
                if (professor.RateMyProfessor != null)
                {
                    professor.RateMyProfessor = new RateMyProfessor();
                }

                // Get the URL needed to scrape that Professors page at RMP 
                string rateMyProfURL = GetTidFromProfessorName(professor.Name);

                // Scrape the page and add the resultant to the ScrapeResult list
                scrapeRMP.Add(InsertDataFromHtmlPage(rateMyProfURL));


            }

            return scrapeRMP;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProfName"></param>
        /// <returns></returns>
        private string GetTidFromProfessorName(string ProfName)
        {
            string[] names = ProfName.Split(",");

            // Get the first word of first name and last name
            string[] firstName = names[1].Split(" ");
            string[] lastName = names[0].Split(" ");

            // Generate the URL with the first and last name
            string urlString = @"http://search.mtvnservices.com/typeahead/suggest/?solrformat=true&rows=20&callback=noCB&prefix=" +
                firstName[0].ToLower() + "+" + lastName[0].ToLower() + "&qf=teacherfirstname_t%5E2000+teacherlastname_t%5E2000+teacher" +
                "fullname_t%5E2000+teacherfullname_autosuggest&bf=pow(total_number_of_ratings_i%2C2.1)&sort=score+desc&defType=edismax&" +
                "siteName=rmp&rows=20&group=off&group.field=content_type_s&group.limit=20&fq=content_type_s%3ATEACHER&fq=schoolname_t%3A%" +
                "22University+of+California+San+Diego%22";

            // Use urlString to extract the pk_id
            string pk_id = (GetRequest(urlString)).Result;

            // Return the final url containing pk_id
            return ("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=" + pk_id);
        }

        async static Task<string> GetRequest(string Url)
        {
            HttpClient client = new HttpClient();

            // Make a request using the given Url
            HttpResponseMessage response = await client.GetAsync(Url);
            // Get the content returned by the request
            HttpContent content = response.Content; 
            // Get as string the content of the request made
            string myContent = await content.ReadAsStringAsync();

            // Get rid of extra chars in the JSON returned from the request
            myContent = myContent.Replace("noCB(", "");
            myContent = myContent.Replace(")", "");

            // Parse the JSON to get pk_id of the professor 
            JObject responseObj = JObject.Parse(myContent);
            JObject respondNode = (JObject)responseObj["response"];
            JArray docsNode = (JArray)respondNode["docs"];
            JValue pk_id = (JValue)docsNode[0]["pk_id"];

            string id = pk_id.ToString();
  
            return (id);

        }

        /// <summary>
        /// Scrapes a single RateMyProf page specified by the URL input parameter.
        /// </summary>
        /// <returns>A List of ScrapeResultRateMyProf objects containing the data</returns>
        /// <param name="Url">URL of single RateMyProf page to scrape</param>
        private ScrapeResult InsertDataFromHtmlPage(string Url)
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

