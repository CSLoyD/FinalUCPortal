using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Response
{
    public class ApproveAdjustmentResponse
    {
        public int success { get; set; }
        public List<string> edp_code { get; set; }
    }
}
