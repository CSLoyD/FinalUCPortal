using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetSubjectReqResponse
    {
        public List<RequestedSubject> request { get; set; }
        public class RequestedSubject
        {
            public string subject_name { get; set; }

            public string desc_1 { get; set; }
            public string desc_2 { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string mdn { get; set; }
            public string days { get; set; }
            public int rtype { get; set; }
            public string m_time_start { get; set; }
            public string m_time_end { get; set; }
            public int status { get; set; }
            public string internal_code { get; set; }
        }
    }
}
