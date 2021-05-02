using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class NotificationResponse
    {       
        public List<Notification> notifications { get; set; }
        public class Notification
        {
            public int id { get; set; }
            public int read { get; set; }
            public string message { get; set; }
            public string dte { get; set; }
        }
    }
}
