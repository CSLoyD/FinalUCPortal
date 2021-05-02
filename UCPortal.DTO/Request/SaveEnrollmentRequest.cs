using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class SaveEnrollmentRequest
    {
        public string id_number { get; set; }
        public List<string> schedules { get; set; }
        public int total_units { get; set; }
        public int year_level { get; set; }
        public string classification { get; set; }
        public int accept_section { get; set; } 
        public string active_term { get; set; }
    }
}
