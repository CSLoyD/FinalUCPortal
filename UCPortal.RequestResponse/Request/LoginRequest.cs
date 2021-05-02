using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class LoginRequest
    {
        [Required]
        public string id_number { get; set; }
        [Required]
        public string password { get; set; }
    }
}
