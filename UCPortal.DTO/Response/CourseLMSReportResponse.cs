using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
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
            public int status { get; set; }
            public int instructor { get; set; }
        }
    }
}
