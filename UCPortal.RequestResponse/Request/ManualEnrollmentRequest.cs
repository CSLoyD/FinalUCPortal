using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ManualEnrollmentRequest
    {
        public string id_number { get; set; }
        public string edp_codes { get; set; }
        public string active_term { get; set; }
    }
}
