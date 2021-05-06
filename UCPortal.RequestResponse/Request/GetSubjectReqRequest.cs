using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetSubjectReqRequest
    {
        public string course_code { get; set; }
        public string department { get; set; }
        public string term { get; set; }
    }
}
