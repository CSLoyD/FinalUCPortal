using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetDepartmentRequest
    {
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
