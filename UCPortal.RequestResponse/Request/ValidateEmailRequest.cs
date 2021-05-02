using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class ValidateEmailRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string token { get; set; }
    }
}
