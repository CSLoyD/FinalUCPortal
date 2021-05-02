using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class LoginResponse
    {
        public int success { get; set;  }
        public StudentInfo student_info { get; set; }
        public string user_type { get; set; }
        public string email { get; set; }
        public string id_number { get; set; }
        public int is_verified { get; set; }
        public string classification { get; set; }
        public string token { get; set; }
        public string profile { get; set; }
        public int has_load { get; set; }
        public string current_exam { get; set; }
        public string active_terms { get; set; }
        public string has_active_term { get; set; }
        public int curr_year { get; set; }
        public int allowed_units { get; set; }
        public class StudentInfo
        {
            public string fullname { get; set; }
            public string course { get; set; }
            public string course_code { get; set; }
            public string department_abbr { get; set; }
            public int pending_exam_promi { get; set; }
            public int approved_promi_amount { get; set; }
        }

    }


}
