using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class StudentSubjectRequest
    {
        public string internal_code { get; set; }
        public string time_start { get; set; }
        public string time_end { get; set; }
        public string m_time_start { get; set; }
        public string m_time_end { get; set; }
        public string mdn { get; set; }
        public string days { get; set; }
        public int rtype { get; set; }
        public string id_number { get; set; }
    }
}
