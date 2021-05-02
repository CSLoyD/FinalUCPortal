using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class UpdateStudentStatusRequest
    {
        public string id_number { get; set; }
        public int new_status { get; set; }
        public string active_term { get; set; }
    }
}
