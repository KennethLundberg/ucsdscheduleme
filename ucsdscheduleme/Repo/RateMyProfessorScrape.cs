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

        // Sending in context to files in repo.
        public RateMyProfessorScrape(ScheduleContext context)
        {
            _context = context;
        }

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
        public void Update()
        {
            // Get professors list from database
            var professors = _context.Professor.ToList();

            // List of professor whose RMP page couldn't be found
            List<string> profRMPNotFound = new List<string>();
            
            // Keep track of number of professors we scrape RMP for
            int delay = 0;
            
            // For each professor in the list from database
            foreach (var professor in professors)
            {    
                // Check if the professor has RateMyProfessor object, if no make an object
                if (professor.RateMyProfessor == null)
                {
                    professor.RateMyProfessor = new RateMyProfessor();
                }

                // Quick fix for the only weird professor : William Arctander O'Brien
                // character " ' " results in weird characters in the last name
                if (professor.Name.Contains("O&#039;Brien")) // TODO: Change name to O'Brien
                    continue;

                // Get the URL needed to scrape that Professors page at RMP 
                string rateMyProfURL = GetTidFromProfessorName(professor.Name);
                if (rateMyProfURL.Length != 0)
                {
                    // Scrape the page to get all the information about the professor
                    ScrapeResult scrapedProfessor = InsertDataFromHtmlPage(rateMyProfURL);

                    // Updating/Adding information of the professor in database
                    professor.RateMyProfessor.LevelOfDifficulty = scrapedProfessor.LevelOfDifficulty;
                    professor.RateMyProfessor.OverallQuality = scrapedProfessor.OverallQuality;
                    professor.RateMyProfessor.WouldTakeAgain = scrapedProfessor.WouldTakeAgain;
                    professor.RateMyProfessor.URL = rateMyProfURL;
                }
                else
                {
                    profRMPNotFound.Add(professor.Name);
                }
                
                // Every time we scrape 100 professors, wait for 10 seconds
                delay++;
                if (delay % 100 == 0 )
                {
                    System.Threading.Thread.Sleep(10000);
                    //_context.SaveChanges();
                }
            }

            // Save changes made in the database
           _context.SaveChanges();

        }

        /// <summary>
        /// Generating the RMP page for professor sent as parameter.
        /// </summary>
        /// <param name="ProfName"> Name of the professor whose RMP page we're generating </param>
        /// <returns>The RMP page's URL</returns>
        private string GetTidFromProfessorName(string ProfName)
        {
            // Split the name to get first and last name
            string[] names = ProfName.Split(",");

            // If the professor's name is staff or empty, then there is no RMP page!
            if (names[0] == "Staff" || names[0]=="")
            {
                return ("");
            }
            
            // Get the first word of first name and last name
            string[] firstName = names[1].Split(" ");
            string[] lastName = names[0].Split(" ");



            // Need to add edge cases; when there are muntiple first and last names in the act and RMP.

            // Generate the URL with the first and last name
            string urlString = ReturnHttpUrl(firstName[1], lastName[0]);

            // Use urlString to extract the pk_id
            string pk_id = (GetRequest(urlString)).Result;

            // If no pk_id was returned, check for the unique cases
            if (pk_id.Length == 0)
            {
                pk_id = CheckUniqueProfessor(firstName, lastName);

                // If can't find the professor's RMP page even in unique case return null
                if (pk_id.Length == 0)
                    return ("");
            }

            // Return the final url containing pk_id
            return ("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=" + pk_id);
        }

        /// <summary>
        /// This method is for the professor who have different name in the RMP page and in
        /// act class schedules that was scraped into the database
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        private string CheckUniqueProfessor(string[] firstName, string[] lastName)
        {
            if (lastName[0] == "Ord" && firstName[1] == "Richard")
            {
                return (GetRequest(ReturnHttpUrl("Rick", "Ord"))).Result;
            }
            else if (lastName[0] == "Porter" && firstName[1] == "Leonard")
            {
                return (GetRequest(ReturnHttpUrl("Leo", "Porter"))).Result;
            }
            else if (lastName[0] == "Cottrell" && firstName[1] == "Garrison")
            {
                return (GetRequest(ReturnHttpUrl("Gary", "Cottrell"))).Result;
            }
            else if (lastName[0] == "Minnes" && firstName[1] == "Mor")
            {
                return (GetRequest(ReturnHttpUrl("Mia", "Minnes-Kemp"))).Result;
            }
            else if (lastName[0] == "Howden" && firstName[1] == "William")
            {   // Need to make sure they're the same professor though
                return (GetRequest(ReturnHttpUrl("Bill", "Howden"))).Result;
            }
            else if (lastName[0] == "Popescu" && firstName[1] == "Cristian")
            {   // Need to make sure they're the same professor though
                return (GetRequest(ReturnHttpUrl("Christian", "Popescu"))).Result;
            }
            else if (lastName[0] == "Knop" && firstName[1] == "Aleksandr")
            {   // Need to make sure they're the same professor though
                return (GetRequest(ReturnHttpUrl("Alexander", "Knop"))).Result;
            }
            else if (lastName[0] == "Aghajani" && firstName[1] == "Mohammadreza")
            {   // Need to make sure they're the same professor though
                return (GetRequest(ReturnHttpUrl("Reza", "Aghajani"))).Result;
            }
            /* Can't Find in the RMP page. Just for CSE 1-190
             * Schulze, Jurgen
             * Ochoa, Benjamin Lawrence
             * Nakashole, Ndapandula
             * Weibel, Nadir
             * Kumar, Rakesh
             * Gao, Sicun
             * Eldon, John
             * Zhao, Jishen
             */
            return "";
        }

        /// <summary>
        /// This method returns httpGet Url to receive the Json from the call.
        /// </summary>
        /// <param name="firstName">The first name of the professor</param>
        /// <param name="lastName">The last name of the professor</param>
        /// <returns>The actual http get URL</returns>
        private string ReturnHttpUrl(string firstName, string lastName)
        {
            return (@"http://search.mtvnservices.com/typeahead/suggest/?solrformat=true&rows=20&callback=noCB&prefix=" +
                firstName.ToLower() + "+" + lastName.ToLower() + "&qf=teacherfirstname_t%5E2000+teacherlastname_t%5E2000+teacher" +
                "fullname_t%5E2000+teacherfullname_autosuggest&bf=pow(total_number_of_ratings_i%2C2.1)&sort=score+desc&defType=edismax&" +
                 "siteName=rmp&rows=20&group=off&group.field=content_type_s&group.limit=20&fq=content_type_s%3ATEACHER&fq=schoolname_t%3A%" +
                 "22University+of+California+San+Diego%22");

        }

        /// <summary>
        /// From the http get method, we extract and return a unique professor id, pk_id.
        /// </summary>
        /// <param name="Url"> The http URL that we'll be used to extract unique 
        /// professor id. </param>
        /// <returns> Unique professor id </returns>
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
            myContent = myContent.Replace(");", "");

            // Parse the JSON to get pk_id of the professor 
            JObject responseObj = JObject.Parse(myContent);
            JObject respondNode = (JObject)responseObj["response"];
            JArray docsNode = (JArray)respondNode["docs"];

            // Check the size of the array to check if we received pk_id;
            if (docsNode.Count > 0)
            {
                JValue pk_id = (JValue)docsNode[0]["pk_id"];
                // Return the unique professor ID
                return (pk_id.ToString());
            }

            // Return empty string if no pk_id found
            return ("");
        }

        /// <summary>
        /// Scrapes a single RateMyProf page specified by the URL input parameter.
        /// </summary>
        /// <returns> ScrapeResult object containing the data </returns>
        /// <param name="Url"> URL of single RateMyProf page to scrape </param>
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

            // Converting the string review to decimal for inserting to db
            decimal qualityRateDecimal;
            bool checkError = Decimal.TryParse(qualityRate, out qualityRateDecimal);
            if (checkError)
                Console.Error.WriteLine("Error while converting qualityRateDecimal");

            // Get Would Take Again Info
            HtmlNode wouldTakeAgain = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.WouldTakeAgainPath);
            string wouldTakeAgainRate = (wouldTakeAgain != null) ? wouldTakeAgain.InnerText.Trim() : "N/A";

            // Converting the string review to decimal for inserting to db
            wouldTakeAgainRate = wouldTakeAgainRate.Replace("%", "");
            decimal wouldTakeAgainDecimal;
            checkError = Decimal.TryParse(wouldTakeAgainRate, out wouldTakeAgainDecimal);
            if (checkError)
                Console.Error.WriteLine("Error while converting wouldTakeAgainDecimal");

            // Get Difficulty Level Info
            HtmlNode difficultyLevel = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.DifficultyLevelPath);
            string difficultyLevelRate = (difficultyLevel != null) ? difficultyLevel.InnerText.Trim() : "N/A";

            // Converting the string review to decimal for inserting to db
            decimal difficultyLevelDecimal;
            checkError = Decimal.TryParse(difficultyLevelRate, out difficultyLevelDecimal);
            if (checkError)
                Console.Error.WriteLine("Error while converting difficultyLevelRateDecimal");

            // Check if professor was rated at all
            if (quality == null && wouldTakeAgain == null && difficultyLevel == null)
            {
                professorIsRated = false;
            }

            // Professor's Full Name
            string professorFullName;

            // Check to see if this professor has any info available on the page
            if (professorIsRated)
            {
                // Scrape the professor's full name(if not, leave "N/A" )
                HtmlNode professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorFirstNamePath);
                string professorFirstName = (professorNode != null) ? professorNode.InnerText.Trim() : "N/A";
                professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorMidNamePath);
                string professorMiddleName = (professorNode != null) ? professorNode.InnerText.Trim() : "N/A";
                professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorLastNamePath);
                string professorLastName = (professorNode != null) ? professorNode.InnerText.Trim() : "N/A";

                // Handle the case when the professor has a middle name
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
                // If the professor has no info on the RMP page
                HtmlNode professorNode = htmlDoc.DocumentNode.SelectSingleNode(RateMyProfXPaths.ProfessorNotRatedNamePath);
                professorFullName = (professorNode != null) ? professorNode.InnerText : "Professor Not Found";
            }

            // Insert the results into their proper fields and return ScrapeResult obj
            ScrapeResult rateMyProfessorResult = new ScrapeResult()
            {
                InstructorName = professorFullName,
                OverallQuality = qualityRateDecimal,
                WouldTakeAgain = wouldTakeAgainDecimal,
                LevelOfDifficulty = difficultyLevelDecimal

            };

            return rateMyProfessorResult;
        }

    }
}

