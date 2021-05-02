using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class AcknowledgePaymentRequest
    {
        public string filename { get; set; }
        public int? duplicate { get; set; }
        public string active_term { get; set; }
    }
}
