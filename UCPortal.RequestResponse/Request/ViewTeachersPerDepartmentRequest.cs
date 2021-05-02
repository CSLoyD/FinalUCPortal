using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ViewTeachersPerDepartmentRequest
    {
        public string dept { get; set; }
        public string active_term { get; set; }
    }
}
