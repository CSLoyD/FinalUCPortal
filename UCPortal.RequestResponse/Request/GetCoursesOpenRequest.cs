using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetCoursesOpenRequest
    {
        public string course_department { get; set; }
        public string department_abbr { get; set; }
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
