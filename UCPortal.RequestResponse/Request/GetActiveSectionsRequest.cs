using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetActiveSectionsRequest
    {
        [Required]
        public int year_level { get; set; }
        [Required]
        public string course_code { get; set; }
        public string active_term { get; set; }
    }
}
