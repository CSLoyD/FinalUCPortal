using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class GetCurriculumRequest
    {
        public string id_number { get; set; }
        public int year { get; set; }
    }
}
