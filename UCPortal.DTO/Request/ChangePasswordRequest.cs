using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class ChangePasswordRequest
    {
        public string user_id { get; set; }
        public string new_password { get; set; }
    }
}
