using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class EmailCheckRequest
    {
        public string email { get; set; }
        public string fullname { get; set; }
    }
}
