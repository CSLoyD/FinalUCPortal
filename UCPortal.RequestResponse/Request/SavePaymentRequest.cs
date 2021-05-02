using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class SavePaymentRequest
    {
        public string id_number { get; set; }
        public List<Attachmentss> attachments { get; set; }
        public string active_term { get; set; }

        public class Attachmentss
        {
            public string email { get; set; }
            public string filename { get; set; }
        }
    }
}
