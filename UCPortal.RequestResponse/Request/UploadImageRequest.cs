using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UCPortal.RequestResponse.Request
{
    public class UploadImageRequest
    {
        [Required]
        public List<IFormFile> formFiles { get; set; }
    }
}
