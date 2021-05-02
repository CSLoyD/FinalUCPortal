using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class UpdateInforRequest
    {
        public string stud_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_initial { get; set; }
        public int year_level { get; set; }
        public string dept { get; set; }
        public string course_code { get; set; }
        public string classification { get; set; }
        public int allowed_units { get; set; }
        public string active_term { get; set; }
    }
}
