using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Response
{
    public class GetSectionResponse
    {
        public List<sections> section;
        public class sections
        {
            public string course_code { get; set; }
            public string section { get; set; }
        }
    }
}
