using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class RemoveEquivalenceRequest
    {
        public string internal_code { get; set; }
        public string equivalence_code { get; set; }
    }
}
