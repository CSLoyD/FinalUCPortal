using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetEnrollmentStatusRequest
    {
        public DateTime dte { get; set; }
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
