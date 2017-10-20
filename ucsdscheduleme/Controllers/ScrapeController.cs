using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ucsdscheduleme.Models;

namespace ucsdscheduleme.Controllers
{
    public class ScrapeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ScrapeViewModel scrape = new ScrapeViewModel() { ScrapeSite = ScrapeSite.Cape };
            return View(scrape);
        }

        /// <summary>
        /// Form postback that takes in Url to scrape the Cape data from.
        /// </summary>
        /// <param name="scrape">Contains the url to scrape from.</param>
        /// <returns>The resultant data from the scrape of the single Url.</returns>
        public IActionResult ScrapeSingleCape([Bind("Url,ScrapeSite")] ScrapeViewModel scrape)
        {

            // Scrape a single cape at specified URL by calling code in repo folder

            // Add result to ScrapeViewModel, and return.
            // For now, fake data!
            scrape.ScrapeResults = new List<ScrapeResult>()
            {
                new ScrapeResult { Fake1 = "\uD83C\uDFBA \uD83C\uDFBA John Cena \uD83C\uDFBA \uD83C\uDFBA", Fake2 = "Some other data stuff", Fake3 = "And another one"}
            };

            return View(nameof(Index),scrape);
        }

        /// <summary>
        /// Scrapes Cape data from the entire Cape website and returns metadata to the view. 
        /// </summary>
        /// <returns>ViewModel with populated metadata about the scrape.</returns>
        public IActionResult ScrapeAllCapes()
        {
            // Call out to repo code and scrape entirety of cape.

            ScrapeViewModel scrape = new ScrapeViewModel();

            return View("Index", scrape);
        }
    }
}
