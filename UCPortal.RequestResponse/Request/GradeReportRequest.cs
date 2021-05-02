using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GradeReportRequest
    {
        public string department { get; set; }
        public string id_number { get; set; }
        public int exam { get; set; }
        public string active_term { get; set; }
    }
}
