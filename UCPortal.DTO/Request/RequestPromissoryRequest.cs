using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class RequestPromissoryRequest
    {
        public string stud_id { get; set; }
        public string message { get; set; }
        public int promise_pay { get; set; }
        public string active_term { get; set; }
    }
}
