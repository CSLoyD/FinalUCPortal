using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class SaveAdjustmentRequest
    {
        public string id_number { get; set; }
        public string[] addEdpCodes { get; set; }
        public string[] deleteEdpCodes { get; set; }
        public string active_term { get; set; }
    }
}
