using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class UserLMSReportResponse
    {
        public List<User> users { get; set; }

        public class User
        {
            public string username { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string cohort1 { get; set; }
            public string cohort2 { get; set; }
            public string cohort3 { get; set; }
            public string classification { get; set; }

        }
    }
}
