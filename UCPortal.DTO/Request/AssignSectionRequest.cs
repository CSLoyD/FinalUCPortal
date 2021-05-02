using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class AssignSectionRequest
    {
        public string section { get; set; }
        public string instructor_id { get; set; }
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
