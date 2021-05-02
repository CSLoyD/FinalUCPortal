using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetExamPromisorryListRequest
    { 
        public string course_department { get; set; }
        public int status { get; set; }
        public string department { get; set; }
        public string exam { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string active_Term { get; set; }      
    }
}
