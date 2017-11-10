using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HtmlAgilitySandbox
{
    class ActScrapeRepo
    {
        // C# standard is that you only create one of these.
        private static HttpClient client = new HttpClient();
        // This is the Url to make the form post to.
        private static readonly string _baseRequestUrl = @"https://act.ucsd.edu/scheduleOfClasses/scheduleOfClassesStudent.htm";
        // This is the Url that is used to build the request for an individual response.
        private static readonly string _baseIndividualResponseUrl = @"https://act.ucsd.edu/scheduleOfClasses/scheduleOfClassesStudentResult.htm";

        private ActFormRequest _actFormRequest;

        /// <summary>
        /// Constructor that builds a form request to ucsd act to grab all html pages for a given term and 
        /// course request.
        /// </summary>
        /// <param name="selectedTerm">Usually a four character code that specifies the term you want 
        /// to search from. Ex: "FA17" would return courses from the Fall 2017 quarter.</param>
        /// <param name="courses">The comma seperated course search parameter. See the ucsd act class 
        /// search for full information on how to use this. Ex: "cse 1-199" returns all CSE courses between 
        /// 1 and 199</param>
        public ActScrapeRepo(string selectedTerm, string courses)
        {
            _actFormRequest = new ActFormRequest(selectedTerm, courses);
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
            message.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");
            message.Content = request;

            var formResponse = client.SendAsync(message).Result;

            var responseAsString = formResponse.Content.ReadAsStringAsync().Result;

            // Now we have the resutlts for the first page of classes
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseAsString);

            // Get the table that contains the number of pages we'll need to get results for.
            var nodes = htmlDoc.DocumentNode.SelectNodes("//table");
            // The navigation table is ALWAYS the third table.
            var navigationTable = nodes[2];
            var navigationText = navigationTable.SelectSingleNode("//td[@align='right']").InnerText;

            // This will check if there is more than one page. If there is only one, we're done
            // Otherwise we gotta make a request for each page.
            if(navigationText.Contains("Last"))
            {
                // Get the a tag with the last page value
                var lastPageATag = navigationTable.SelectSingleNode("//a[text() = 'Last']");
                // The actual last page is stored as baseUrl?page=[lastPageNumber]
                string lastPageHref = lastPageATag.GetAttributeValue("href","");
                // Get the part of the href which is the page, ie the part after the "="
                var lastPageAsString = lastPageHref.Substring(lastPageHref.LastIndexOf('=') + 1);

                // If we got here, we know there is a last page, and we know the format only has one = sign,
                // so this should never fail, BUT, just in case, lets verfiy things are going as well as they should.
                if (Int32.TryParse(lastPageAsString,out int lastPage))
                {
                    // Start at page 2 because we already grabbed page 1.
                    for(int pageNum = 2; pageNum <= lastPage; pageNum++)
                    {
                        Uri thisPageUri = new Uri(_baseIndividualResponseUrl + "?page=" + pageNum);
                        HttpRequestMessage thisMessage = new HttpRequestMessage(HttpMethod.Get, thisPageUri);
                        // Tell ucsd that we're always coming from the first response page.
                        thisMessage.Headers.Referrer = new Uri(_baseIndividualResponseUrl);
                        // Pretend we're a browser.
                        thisMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");

                        var pageResponse = client.SendAsync(thisMessage).Result;

                        var pageResponseAsString = pageResponse.Content.ReadAsStringAsync().Result;

                        // Now we have the results for this page of classes
                        var thisHtmlDoc = new HtmlDocument();
                        thisHtmlDoc.LoadHtml(responseAsString);

                        // Add it to the final return
                        allDocumentsForQuerry.Add(thisHtmlDoc);
                    }
                }
            }
            // If we got here there should be only one page, the one we already got.
            else
            {
                allDocumentsForQuerry.Add(htmlDoc);
            }

            return allDocumentsForQuerry;
        }
    }
}
