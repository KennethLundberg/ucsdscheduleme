using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilitySandbox;
using ucsdscheduleme.Models;
using ucsdscheduleme.Repo;
using Microsoft.EntityFrameworkCore;

namespace ucsdscheduleme.Data
{
    public class DbInitializer
    {
        public static void Initialize(ScheduleContext context)
        {
            // TODO put back before commiting
            context.Database.EnsureCreated();
            if(context.Courses.Any())
            {
                return;
            }

            // Calling the Scraping Controller
            ScrapeRepo scrapeAll = new ScrapeRepo(context);
            scrapeAll.Update();

            //context.Database.ExecuteSqlCommandAsync();

            Console.Write("Code has been completed");
        }
    }
}
