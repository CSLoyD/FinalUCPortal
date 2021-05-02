using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetBEAssessmentRequest
    {
        public string id_number { get; set; }
        public string exam { get; set; }
        public string active_term { get; set; }
    }
}
