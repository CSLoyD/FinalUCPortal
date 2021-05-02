using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class VerificationRequest
    {
        [Required]
        public string email;
        [Required]
        public string token;
    }
}
