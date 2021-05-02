using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class SaveEnrollmentRequest
    {
        [Required]
        public string id_number { get; set; }
        [Required]
        public List<string> schedules { get; set; }
        [Required]
        public int total_units { get; set; }
        [Required]
        public int year_level { get; set; }
        [Required]
        public string classification { get; set; }
        public int accept_section { get; set; }
        public string active_term { get; set; }
    }

    
}
