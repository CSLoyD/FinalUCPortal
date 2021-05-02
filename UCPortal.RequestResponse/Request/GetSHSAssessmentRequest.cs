using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetSHSAssessmentRequest
    {
        public string id_number { get; set; }
        public string exam { get; set; }
        public string active_term { get; set; }
    }
}
