using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetStudentBalancePerCategoryRequest
    {
        public string course_department { get; set; }
        public string active_term { get; set; }
    }
}
