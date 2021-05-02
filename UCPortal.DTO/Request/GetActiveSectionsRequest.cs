using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetActiveSectionsRequest
    {
        public int year_level { get; set; }
        public string course_code { get; set; }
        public string active_term { get; set; }
    }
}
