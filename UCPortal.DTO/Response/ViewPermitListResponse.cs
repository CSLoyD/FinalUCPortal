using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class ViewPermitListResponse
    {
        public string edp_code { get; set; }
        public string subject_name { get; set; }
        public string time_info { get; set; }
        public string units { get; set; }
        public int subject_size { get; set; }
        public string exam_type { get; set; }
        public List<Enrolled> enrolled { get; set; }
        public class Enrolled
        {
            public string id_number { get; set; }
            public string last_name { get; set; }
            public string firstname { get; set; }
            public string course_year { get; set; }
            public string mobile_number { get; set; }
            public string email { get; set; }
            public int has_permit { get; set; }
            public int applied_promi { get; set; }
        }
    }
}
