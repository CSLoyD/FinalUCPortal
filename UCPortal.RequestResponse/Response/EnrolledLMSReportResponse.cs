using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class EnrolledLMSReportResponse
    {
        public List<enroll> enrolled { get; set; }

        public class enroll
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
        }
    }
}
