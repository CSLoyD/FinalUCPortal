using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetStudyLoadRequest
    {
        public string id_number { get; set; }
        public string active_term { get; set; }
    }
}
