using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetCoursesRequest
    {
        public string course_department { get; set; }
        public string department_abbr { get; set; }
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
