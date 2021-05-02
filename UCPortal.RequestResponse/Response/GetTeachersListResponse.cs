using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetTeachersListResponse
    {
        public List<Teachers> teacherList;
        public class Teachers
        {
            public string id_number { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
        }
    }
}
