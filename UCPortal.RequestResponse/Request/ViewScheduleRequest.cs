using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ViewScheduleRequest
    {
        public string course_code { get; set; }
        public int year_level { get; set; }
        public List<string> edp_codes { get; set; }
        public string subject_name { get; set; }
        public string section { get; set; }
        public int status { get; set; }
        public int sort { get; set; }        
        public string? gen_ed { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public int no_nstp { get; set; }
        public int no_pe { get; set;  }
        public string department_abbr { get; set; }
        public string active_term { get; set; }
    }
}
