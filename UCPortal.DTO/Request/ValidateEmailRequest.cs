using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ValidateEmailRequest
    {
        public string email { get; set; }
        public string token { get; set; }
    }
}
