using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetStudentListResponse
    {
        
        public List<Students> students { get; set; }

        public class Students
        {
            public string id_number { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string course { get; set; }
            public int year { get; set; }
            public string contact { get; set; }
            public string fb { get; set; }
            public string email { get; set; }
        }
    }
}
