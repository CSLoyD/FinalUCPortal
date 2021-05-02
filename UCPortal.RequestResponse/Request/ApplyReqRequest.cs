using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ApplyReqRequest
    {
        [Required]
        public string stud_id { get; set; }
        public int request_deblock { get; set; }
        public int request_overload { get; set; }

    }
}
