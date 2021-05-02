using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ChangeSchedStatusRequest
    {
        public string course_code { get; set; }
        public string section { get; set; }
        public List<string> edp_code { get; set; }
        public int status { get; set; }
        public string active_term { get; set; }
    }
}
