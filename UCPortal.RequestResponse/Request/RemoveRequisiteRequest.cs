using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class RemoveRequisiteRequest
    {
        public string internal_code { get; set; }
        public string requisite { get; set; }
        public string requisite_type { get; set; }
    }
}
