using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class ViewClasslistPerSectionResponse
    {
        public string course { get; set; }
        public string section { get; set; }
        public string section_size { get; set; }
        public List<Enrolled> assigned_section { get; set; }
        public class Enrolled
        {
            public string id_number { get; set; }
            public string last_name { get; set; }
            public string firstname { get; set; }
            public string course_year { get; set; }
            public string mobile_number { get; set; }
            public string email { get; set; }
            public int status { get; set; }
            public string gender { get; set; }
        }
    }
}

