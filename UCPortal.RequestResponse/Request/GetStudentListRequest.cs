using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetStudentListRequest
    {
        public string internal_code { get; set; }
        public string term { get; set;}
    }
}
