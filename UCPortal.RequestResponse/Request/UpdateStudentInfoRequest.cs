using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class UpdateStudentInfoRequest
    {
        [Required]
        public string id_number { get; set; }
        [Required]
        public string dept { get; set; }
        [Required]
        public string course_code { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public string email { get; set; }
        public string facebook { get; set; }
        [Required]
        public string classification { get; set; }
        public string mdn { get; set; }
        public string active_term { get; set; }
    }
}
