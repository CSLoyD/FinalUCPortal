using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class ViewOldStudentInfoResponse
    {
        public string stud_id { get; set; }
        public int allowed_units { get; set; }
        public string college { get; set; }
        public string course { get; set; }
        public string course_code { get; set; }
        public string assigned_section { get; set; }
        public int year_level { get; set; }
        public string mdn { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string suffix { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public int start_term { get; set; }
        public int is_verified { get; set; }
        public string classification { get; set; }
        public string dept { get; set; }
        public string section { get; set; }
        public string mobile { get; set; }
        public string landline { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public int request_deblock { get; set; }
        public int request_overload { get; set; }
        public List<attachment> attachments { get; set; }

        public class attachment
        {
            public int attachment_id { get; set; }
            public string id_number { get; set; }
            public string email { get; set; }
            public string type { get; set; }
            public string filename { get; set; }
        }
    }
}
