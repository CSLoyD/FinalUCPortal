using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ViewStudentPerStatusRequest
    {
        public int status { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string id_number { get; set; }
        public string course_department { get; set; }
        public string course { get; set; }
        public int year_level { get; set; }
        public string classification { get; set; }
        public int? is_cashier { get; set; }
        public string active_term { get; set; }
    }
}
