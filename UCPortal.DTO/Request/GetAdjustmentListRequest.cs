using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetAdjustmentListRequest
    {
        public string course_department { get; set; }
        public string course_code { get; set; }
        public int year { get; set; }
        public string id_number { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string active_term { get; set; }
    }
}
