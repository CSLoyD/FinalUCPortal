using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetCourseInfoResponse
    {
        public List<Courses> courses { get; set; }
        public class Courses
        { 
            public string course_code { get; set; }
            public string course_description { get; set; }
            public string course_abbr { get; set; }
            public int year_limit { get; set; }
            public string course_department { get; set; }
            public string course_department_abbr { get; set; }
            public string department { get; set; }
        }
       
    }
}
