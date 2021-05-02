using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class UpdateStudentInfoRequest
    {
        public string id_number { get; set; }
        public string dept { get; set; }
        public string course_code { get; set; }
        public int year { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string classification { get; set; }
        public string mdn { get; set; }
        public string active_term { get; set; }
    }
}
