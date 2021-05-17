using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetEquivalenceRequest
    {
        public string description { get; set; }
        public int units { get; set; }
        public string course_code { get; set; }
        public int curr_year { get; set; }
        public string subject_name { get; set; }
        public string internal_code { get; set; }
    }

}
