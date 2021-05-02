using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class CourseLMSReportResponse
    {
        public List<course> courses { get; set; }
        public class course
        { 
            public string shortname { get; set; }
            public string fullname { get; set; }
            public string category { get; set; }
            public string idnumber { get; set; }
        }
    }
}
