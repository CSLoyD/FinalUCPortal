using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class GetDepartmentResponse
    {
        public List<department> departments { get; set; }
        public class department
        {
            public string dept_name { get; set; }
            public string dept_abbr { get; set; }
        }
    }
}
