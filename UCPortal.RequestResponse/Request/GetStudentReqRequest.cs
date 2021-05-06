using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetStudentReqRequest
    {
        public string id_number { get; set; }
        public string term { get; set; }
    }
}
