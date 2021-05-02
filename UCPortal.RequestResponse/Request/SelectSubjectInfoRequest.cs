using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class SelectSubjectInfoRequest
    {
        public string edp_code { get; set; }
        public string active_term { get; set; }
    }
}
