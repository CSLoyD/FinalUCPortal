using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ApproveAdjustmentRequest
    {
        public string id_number { get; set; }
        public int approve { get; set; }
        public string active_term { get; set; }
    }
}
