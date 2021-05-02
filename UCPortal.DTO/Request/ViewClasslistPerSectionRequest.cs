using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ViewClasslistPerSectionRequest
    {
        public string course_code { get; set; }
        public string section { get; set; }
        public string active_term { get; set; }
    }
}
