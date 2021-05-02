using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ViewAdviserRequest
    {
        public string section { get; set; }
        public string department { get; set; }
        public string active_term { get; set; }
    }
}
