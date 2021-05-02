using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class AddNotificationRequest
    {
        public string stud_id { get; set; }
        public string from_sender { get; set; }
        public string message { get; set; }
        public string active_term { get; set; }
    }
}
