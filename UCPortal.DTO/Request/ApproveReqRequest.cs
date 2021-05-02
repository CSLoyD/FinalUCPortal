using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ApproveReqRequest
    {
        public string id_number { get; set; }
        public int approved_deblock { get; set; }
        public int approved_overload { get; set; }
        public int approved_promissory { get; set; }
        public int? approved_prelim_promissory { get; set; }
        public int? approved_midterm_promissory { get; set; }
        public int? approved_semi_promissory { get; set; }
        public int? approved_final_promissory { get; set; }
        public int max_units { get; set; }
        public int promise_pay { get; set; }
        public string message { get; set; }
        public string active_term { get; set; }
    }
}
