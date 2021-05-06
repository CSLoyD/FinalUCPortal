using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetStudentReqResponse
    {
        public string test { get; set; }
        public List<RequestedSubject> request { get; set; }
        public List<FilteredSubject> filtered { get; set; }
        public class RequestedSubject
        {
            public string subject_name { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string mdn { get; set; }
            public string days { get; set; }
            public int rtype { get; set; }
            public string m_time_start { get; set; }
            public string m_time_end { get; set; }
            public int status { get; set; }
            public string internal_code { get; set; }
            public int size {get;set;}
        }
        public class FilteredSubject
        { 
            public string internal_code { get; set; }
            public string subject_name { get; set; }
        }
    }
}
