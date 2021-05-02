using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string user_id { get; set; }
        [Required]
        public string new_password { get; set; }
    }
}
