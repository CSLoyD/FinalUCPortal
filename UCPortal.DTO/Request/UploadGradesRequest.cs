using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class UploadGradesRequest
    {
        public string edp_code { get; set; }
        public int exam { get; set; }
        public List<grades> stud_grades { get; set; }
        public string active_term { get; set; }
        public class grades
        {
            public string stud_id { get; set; }
            public string grade { get; set; }
        }
    }
}
