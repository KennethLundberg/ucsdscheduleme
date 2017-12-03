using HtmlAgilitySandbox;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ucsdscheduleme.Data;

namespace ucsdscheduleme.Repo
{
    public class ScrapeRepo
    {
        ScheduleContext _context;

        // Settin up the Db Context
        public ScrapeRepo(ScheduleContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is used as a controller that will call all the other update method
        /// of the other scrape classes.
        /// </summary>
        public void Update()
        {
            // List of departments that are in UCSD
            List<string> listOfDepartments = GetAllDepartments();

            // Getting the courses for each department from the act website
            /*foreach (string department in listOfDepartments)
            {
                // Scrape act for each department for Winter 18 
                ClassScrape classScraper = new ClassScrape(_context, "WI18", department + " 1-10");
                classScraper.Update();
            }*/

            ClassScrape classScraper = new ClassScrape(_context, "WI18", "cse 1-100");
            classScraper.Update();

            // Scraping the rate my professor page for all professor in the db
            RateMyProfessorScrape scrapeRMP = new RateMyProfessorScrape(_context);
            scrapeRMP.Update();

            // Scraping the cape website for each professor and course combo in db
            CapeScrape scrapeCape = new CapeScrape(_context);
            scrapeCape.Update();
        }

        /// <summary>
        /// Get a list of all department codes(i.e. CSE or MAE) from the JSON
        /// </summary>
        /// <returns> List of strings containing code names of all departments
        /// </returns>
        private static List<string> GetAllDepartments()
        {
            List<string> departmentsList = new List<string>();

            string httpURL = @"https://act.ucsd.edu/scheduleOfClasses/department-list.json?selectedTerm=WI18";

            // Make a request and get the JSON containing the code names 
            string jsonStringOfDepartments = GetRequest(httpURL).Result;

            // Parse the JSON array to get a list of JSON objects(departments) 
            JArray responseObj = JArray.Parse(jsonStringOfDepartments);

            // Iterate through the list and get the code name 
            foreach (JObject department in responseObj)
            {
                JValue deptCode = (JValue)department["code"];
                // Add the code name to the list of department code names
                departmentsList.Add(deptCode.ToString());
            }

            return departmentsList;
        }

        /// <summary>
        /// Make a Get request to get list of all departments
        /// </summary>
        /// <param name="URL"></param>
        /// <returns> JSON array of all department names</returns>
        async static Task<string> GetRequest(string URL)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(URL);

            HttpContent content = response.Content;

            string myContent = await content.ReadAsStringAsync();

            return myContent;
        }

        // Get a list of all departments code names 
        List<string> listOfDepartments = GetAllDepartments();

    }
}
