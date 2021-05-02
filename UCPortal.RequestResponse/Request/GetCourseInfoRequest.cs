using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetCourseInfoRequest
    {
        public int curr_year { get; set; }
        public string department { get; set; }
    }
}
