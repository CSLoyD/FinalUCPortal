using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class RequestExamPromiRequest
    {
        public string stud_id { get; set; }
        public string message { get; set; }
        public int promise_pay { get; set; }
        public string exam { get; set; }
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
