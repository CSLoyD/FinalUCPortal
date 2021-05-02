using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class UpdateEmailRequest
    {
        public string id_number { get; set; }
        public string email { get; set; }
    }
}
