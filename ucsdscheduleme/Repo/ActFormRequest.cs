using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace HtmlAgilitySandbox
{
    public class ActFormRequest
    {
        public List<KeyValuePair<string,string>> FullFormValues { get; private set; }

        public ActFormRequest(string selectedTerm, string courses)
        {
            var courseQuerry = new KeyValuePair<string,string>("courses",courses);
            var selectedTermQuerry = new KeyValuePair<string, string>("selectedTerm", selectedTerm);

            FullFormValues = FormValues.ToList();
            FullFormValues.Add(courseQuerry);
            FullFormValues.Add(selectedTermQuerry);
        }

        private static KeyValuePair<string, string>[] FormValues = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string,string>("xsoc_term",""),
            new KeyValuePair<string,string>("loggedIn","false"),
            new KeyValuePair<string,string>("tabNum","tabs-crs"),
            new KeyValuePair<string,string>("_selectedSubjects","1"),
            new KeyValuePair<string,string>("schedOption1","true"),
            new KeyValuePair<string,string>("_schedOption1","on"),
            new KeyValuePair<string,string>("_schedOption11","on"),
            new KeyValuePair<string,string>("_schedOption12","on"),
            new KeyValuePair<string,string>("schedOption2","true"),
            new KeyValuePair<string,string>("_schedOption2","on"),
            new KeyValuePair<string,string>("_schedOption4","on"),
            new KeyValuePair<string,string>("_schedOption5","on"),
            new KeyValuePair<string,string>("_schedOption3","on"),
            new KeyValuePair<string,string>("_schedOption7","on"),
            new KeyValuePair<string,string>("_schedOption8","on"),
            new KeyValuePair<string,string>("_schedOption13","on"),
            new KeyValuePair<string,string>("_schedOption10","on"),
            new KeyValuePair<string,string>("_schedOption9","on"),
            new KeyValuePair<string,string>("schDay","M"),
            new KeyValuePair<string,string>("_schDay","on"),
            new KeyValuePair<string,string>("schDay","T"),
            new KeyValuePair<string,string>("_schDay","on"),
            new KeyValuePair<string,string>("schDay","W"),
            new KeyValuePair<string,string>("_schDay","on"),
            new KeyValuePair<string,string>("schDay","R"),
            new KeyValuePair<string,string>("_schDay","on"),
            new KeyValuePair<string,string>("schDay","F"),
            new KeyValuePair<string,string>("_schDay","on"),
            new KeyValuePair<string,string>("schDay","S"),
            new KeyValuePair<string,string>("_schDay","on"),
            new KeyValuePair<string,string>("schStartTime","12:00"),
            new KeyValuePair<string,string>("schStartAmPm","0"),
            new KeyValuePair<string,string>("schEndTime","12:00"),
            new KeyValuePair<string,string>("schEndAmPm","0"),
            new KeyValuePair<string,string>("_selectedDepartments","1"),
            new KeyValuePair<string,string>("schedOption1Dept","true"),
            new KeyValuePair<string,string>("_schedOption1Dept","on"),
            new KeyValuePair<string,string>("_schedOption11Dept","on"),
            new KeyValuePair<string,string>("_schedOption12Dept","on"),
            new KeyValuePair<string,string>("schedOption2Dept","true"),
            new KeyValuePair<string,string>("_schedOption2Dept","on"),
            new KeyValuePair<string,string>("_schedOption4Dept","on"),
            new KeyValuePair<string,string>("_schedOption5Dept","on"),
            new KeyValuePair<string,string>("_schedOption3Dept","on"),
            new KeyValuePair<string,string>("_schedOption7Dept","on"),
            new KeyValuePair<string,string>("_schedOption8Dept","on"),
            new KeyValuePair<string,string>("_schedOption13Dept","on"),
            new KeyValuePair<string,string>("_schedOption10Dept","on"),
            new KeyValuePair<string,string>("_schedOption9Dept","on"),
            new KeyValuePair<string,string>("schDayDept","M"),
            new KeyValuePair<string,string>("_schDayDept","on"),
            new KeyValuePair<string,string>("schDayDept","T"),
            new KeyValuePair<string,string>("_schDayDept","on"),
            new KeyValuePair<string,string>("schDayDept","W"),
            new KeyValuePair<string,string>("_schDayDept","on"),
            new KeyValuePair<string,string>("schDayDept","R"),
            new KeyValuePair<string,string>("_schDayDept","on"),
            new KeyValuePair<string,string>("schDayDept","F"),
            new KeyValuePair<string,string>("_schDayDept","on"),
            new KeyValuePair<string,string>("schDayDept","S"),
            new KeyValuePair<string,string>("_schDayDept","on"),
            new KeyValuePair<string,string>("schStartTimeDept","12:00"),
            new KeyValuePair<string,string>("schStartAmPmDept","0"),
            new KeyValuePair<string,string>("schEndTimeDept","12:00"),
            new KeyValuePair<string,string>("schEndAmPmDept","0"),
            new KeyValuePair<string,string>("sections",""),
            new KeyValuePair<string,string>("instructorType","begin"),
            new KeyValuePair<string,string>("instructor",""),
            new KeyValuePair<string,string>("titleType","contain"),
            new KeyValuePair<string,string>("title",""),
            new KeyValuePair<string,string>("_hideFullSec","on"),
            new KeyValuePair<string,string>("_showPopup","on")
        };

        public FormUrlEncodedContent WebregRequestFormEncoded
        {
            get
            {
                var request = new FormUrlEncodedContent(FullFormValues);
                return request;
            }
        }
    }
}
