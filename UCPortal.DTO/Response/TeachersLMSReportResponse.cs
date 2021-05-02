using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class TeachersLMSReportResponse
    {
        public List<teacher> teachers { get; set; }
        public class teacher
        {
            public string username { get; set; }
            public string course1 { get; set; }
            public string role1 { get; set; }
        }
    }
}
