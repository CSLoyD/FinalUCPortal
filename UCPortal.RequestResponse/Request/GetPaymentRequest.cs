using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class GetPaymentRequest
    {
        public string id_number { get; set; }
        public string exam_type { get; set; }
        public int status { get; set; }
        public string active_term { get; set; }
    }
}
