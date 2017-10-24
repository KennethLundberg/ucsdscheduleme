using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public enum ScrapeSite
    {
        Cape,
        Webreg,
        RateMyProfessor
    }

    public class ScrapeViewModel
    {
        public string Url { get; set; }
        public ScrapeSite ScrapeSite { get; set; }
        public List<ScrapeResult> ScrapeResults { get; set; } = new List<ScrapeResult>();
    }
}
