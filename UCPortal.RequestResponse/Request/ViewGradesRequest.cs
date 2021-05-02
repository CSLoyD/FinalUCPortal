using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ViewGradesRequest
    {
        public string edp_code { get; set; }
        public string active_term { get; set; }
    }
}
