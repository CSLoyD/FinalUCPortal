using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ViewOldStudentInfoRequest
    {
        public string id_number { get; set; }
        public int? payment { get; set; }
        public string active_term { get; set; }
    }
}
