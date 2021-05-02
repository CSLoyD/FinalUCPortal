using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ApplyReqRequest
    {
        public string stud_id { get; set; }
        public int request_deblock { get; set; }
        public int request_overload { get; set; }
        public int approved_promissory { get; set; }
    }
}
