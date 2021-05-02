using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UCPortal.DTO.Request
{
    public class UploadImageRequest
    {
        public List<IFormFile> formFiles { get; set; }
    }
}
