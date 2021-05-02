using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class CreateAccountRequest
    {
        public string id_number { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string middle_initial { get; set; }
        public string suffix { get; set; }
        public string dept { get; set; }
        public string course { get; set; }
        public string sex { get; set; }
        public string user_type { get; set; }
        public string email { get; set; }
        public string mobile_number { get; set; }
    }
}
