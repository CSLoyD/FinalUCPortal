using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class VerifyEmailRequest
    {
        public string id_number { get; set; }
        public string email { get; set; }
    }
}
