using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetCoursesResponse
    {
        public List<college> colleges { get; set; }
        public class college
        {
            public int college_id { get; set; }
            public string college_name { get; set; }
            public string college_code { get; set; }
            public int year_limit { get; set; }
            public string department { get; set; }
        }
    }
    
}
