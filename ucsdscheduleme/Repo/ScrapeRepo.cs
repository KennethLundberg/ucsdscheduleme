using HtmlAgilitySandbox;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Update()
        {
            // Getting the cource from the act website
            ClassScrape classScraper = new ClassScrape(_context, "WI18", "cse 1-190");
            classScraper.Update();

            // TODO Now need add the other classes to the DB just like cse 1-190.

            // Scraping the rate my professor page for all professor in the db
            RateMyProfessorScrape scrapeRMP = new RateMyProfessorScrape(_context);
            scrapeRMP.Update();

            // Scraping the cape website for each professor and course combo in db
            CapeScrape scrapeCape = new CapeScrape(_context);
            scrapeCape.Update();
        }
    }
}
